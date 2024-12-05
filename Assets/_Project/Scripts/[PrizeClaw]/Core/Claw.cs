using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PrizeClaw
{
    public class Claw : MonoBehaviour
    {
        private enum State { Ready, PullDown, PullUp, Grabbed, Released }


        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _pullForce;
        [SerializeField] private float _rotateAngle;
        [SerializeField] private float _grabAngle;
        [SerializeField] private float _pullUpSpeed;
        [SerializeField] private float _freezeRadius;

        [SerializeField] private List<Collider2D> _colliders;

        [SerializeField] private Rigidbody2D _baseRigidbody; 
        private Transform BaseTransform => _baseRigidbody.transform;


        [SerializeField] private Rigidbody2D _clawRigidbody;
        private Transform ClawTransform => _clawRigidbody.transform;


        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _freezePoint;

        private Tween _swingTween;
        private Tween _grabTween;
        private State _state = State.Ready;
        private Vector2 _clawStartLocalPosition;
        private List<Collider2D> _grabbedColliders = new List<Collider2D>();


        private const float rotateDuration = 2f;
        private const float grabDuration = 0.5f;
        private const float pullUpDuration = 2f;
        private const float returnDuration = 0.3f;



        private void Start()
        {
            _clawStartLocalPosition = ClawTransform.localPosition;
            RunSwingTween();
        }

        private void RunSwingTween()
        {
            BaseTransform.DOLocalRotate(-Vector3.forward * _rotateAngle, rotateDuration / 2f)
                .SetEase(Ease.InOutQuad)
                .onComplete += () =>
                {
                    _swingTween?.Kill();
                    _swingTween = DOTween.Sequence()
                        .Append(BaseTransform.DORotate(Vector3.forward * _rotateAngle, rotateDuration / 2f)
                            .SetEase(Ease.InOutQuad))
                        .Append(BaseTransform.DORotate(-Vector3.forward * _rotateAngle, rotateDuration / 2f)
                            .SetEase(Ease.InOutQuad))
                        .SetLoops(-1);
                };
            
        }


        public void Move(float movement)
        {
            _baseRigidbody.transform.Translate
                (Vector2.right * movement * Time.deltaTime * _moveSpeed, Space.World);
        }


        public void Interact()
        {
            switch (_state)
            {
                case State.Ready: PullDown();
                    break;

                case State.Grabbed: Release();
                    break;
            }
        }


        private void PullDown()
        {
            _state = State.PullDown;

            _swingTween.Kill();

            ClawTransform.SetParent(BaseTransform.parent);
            _clawRigidbody.isKinematic = false;
            _clawRigidbody.AddForce(_pullForce * -BaseTransform.up.normalized);

            _swingTween = BaseTransform.DORotate(Vector3.zero, rotateDuration / 2f)
                .SetEase(Ease.InOutQuad);

            _grabTween = DOTween.Sequence()
                .AppendInterval(2f)
                .Append(_leftHand.DOLocalRotate(Vector3.forward * _grabAngle, grabDuration))
                .Join(_rightHand.DOLocalRotate(-Vector3.forward * _grabAngle, grabDuration))
                .AppendCallback(PullUp);
        }


        private void PullUp()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_freezePoint.position, _freezeRadius);
            foreach (var collider in hitColliders)
            {
                if (_colliders.Exists((coll) => coll == collider) == false)
                {
                    collider.enabled = false;
                    collider.attachedRigidbody.isKinematic = true;
                    collider.attachedRigidbody.velocity = Vector3.zero;
                    collider.attachedRigidbody.angularVelocity = 0f;
                    collider.attachedRigidbody.transform.SetParent(ClawTransform);
                    _grabbedColliders.Add(collider);
                }

            }

            foreach (var collider in _colliders)
                collider.enabled = false;

            _clawRigidbody.isKinematic = true;
            _clawRigidbody.velocity = Vector3.zero;
            _clawRigidbody.angularVelocity = 0f;

            ClawTransform.SetParent(BaseTransform);
            _state = State.PullUp;
            ClawTransform.DOLocalMove(_clawStartLocalPosition, pullUpDuration);
            ClawTransform.DORotate(Vector3.zero, pullUpDuration)
                .onComplete += () =>
                {
                    
                    _state = State.Grabbed;

                };
        }


        private void Release()
        {
            _state = State.Released;
            
            _grabTween?.Kill();
            _grabTween = DOTween.Sequence()
                .Append(_leftHand.DOLocalRotate(Vector3.zero, grabDuration))
                .Join(_rightHand.DOLocalRotate(Vector3.zero, grabDuration))
                .AppendCallback(ReturnToOrigin);

            foreach (var collider in _grabbedColliders)
            {
                collider.enabled = true;
                collider.attachedRigidbody.isKinematic = false;
                collider.attachedRigidbody.transform.SetParent(null);
            }
            _grabbedColliders.Clear();

        }

        private void ReturnToOrigin()
        {
            _clawRigidbody.isKinematic = true;
            foreach (var collider in _colliders)
                collider.enabled = true;


            _clawRigidbody.velocity = Vector3.zero;
            _clawRigidbody.angularVelocity = 0f;
            ClawTransform.SetParent(BaseTransform);
            _leftHand.localRotation = Quaternion.Euler(Vector3.zero);
            _rightHand.localRotation = Quaternion.Euler(Vector3.zero);
            DOTween.Sequence()
                .Append(ClawTransform.DOLocalRotate(Vector3.zero, returnDuration))
                .Join(ClawTransform.DOLocalMove(_clawStartLocalPosition, returnDuration))
                .AppendCallback(() =>
                {
                    _state = State.Ready;
                    RunSwingTween();
                });

        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_freezePoint.position, _freezeRadius);
        }


    }
}


