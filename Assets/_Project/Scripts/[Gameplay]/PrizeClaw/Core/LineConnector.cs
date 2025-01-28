using UnityEngine;

namespace PrizeClaw
{
    public class LineConnector : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _fromTransform;
        [SerializeField] private Transform _toTransform;


        private void Update()
        {
            _lineRenderer.SetPosition(0, _fromTransform.position);
            _lineRenderer.SetPosition(1, _toTransform.position);
        }

    }
}


