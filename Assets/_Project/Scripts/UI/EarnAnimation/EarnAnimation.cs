using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;

public class EarnAnimation
{
    private const float IMAGE_SIZE = 80f;
    private const float PUSH_FORCE = 150f;
    private const float PUSH_DURATION = 0.5f;
    private readonly Vector2 MOVE_DURATION_FROM_TO = new Vector2(0.5f, 0.8f);
    private const float END_PARTICLE_SIZE = 0.7f;

    private const float EARN_PULCE_SIZE = 1.2f;
    private const float EARN_PULCE_DURATION = 0.15f;

    private Sequence _pulceSequence;


    public EarnAnimation(Vector2 from, RectTransform toRect, Sprite sprite, int imagesAmount)
    {
        for (int i = 0; i < imagesAmount; i++)
        {
            var imagePrefab = new GameObject();
            var imageObject = Object.Instantiate(imagePrefab, UI.Canvas);
            var image = imageObject.AddComponent<Image>();
            image.sprite = sprite;
            image.rectTransform.sizeDelta = Vector2.one * IMAGE_SIZE;
            image.preserveAspect = true;
            image.transform.position = from;

            Vector2 randomDirection = Random.insideUnitCircle * PUSH_FORCE;
            float randomMoveDuration = Random.Range(MOVE_DURATION_FROM_TO.x, MOVE_DURATION_FROM_TO.y);

            DOTween.Sequence()
                .Append(image.rectTransform.DOMove(from + randomDirection, PUSH_DURATION).SetEase(Ease.OutFlash))
                .Append(image.rectTransform.DOMove(toRect.position, randomMoveDuration).SetEase(Ease.OutFlash))
                .Join(image.rectTransform.DOScale(END_PARTICLE_SIZE, randomMoveDuration).SetEase(Ease.OutFlash))
                .AppendCallback(() => Object.Destroy(image.gameObject))
                .onComplete += () =>
                {
                    _pulceSequence?.Kill();
                    _pulceSequence = DOTween.Sequence()
                    .Append(toRect.DOScale(EARN_PULCE_SIZE, EARN_PULCE_DURATION / 2f).SetEase(Ease.OutFlash))
                    .Append(toRect.DOScale(1f, EARN_PULCE_DURATION / 2f).SetEase(Ease.OutFlash));
                };

        }

    }

    public EarnAnimation(Vector2 from, TextMeshProUGUI toText, float fromValue, float toValue, Sprite sprite, int imagesAmount)
    {
        toText.text = fromValue.ToShortNumber();
        float addForPulce = (toValue - fromValue) / imagesAmount;
        float currentValue = fromValue;


        for (int i = 0; i < imagesAmount; i++)
        {
            var imagePrefab = new GameObject();
            var imageObject = Object.Instantiate(imagePrefab, UI.Canvas);
            var image = imageObject.AddComponent<Image>();
            image.sprite = sprite;
            image.rectTransform.sizeDelta = Vector2.one * IMAGE_SIZE;
            image.preserveAspect = true;
            image.transform.position = from;

            Vector2 randomDirection = Random.insideUnitCircle * PUSH_FORCE;
            float randomMoveDuration = Random.Range(MOVE_DURATION_FROM_TO.x, MOVE_DURATION_FROM_TO.y);

            DOTween.Sequence()
                .Append(image.rectTransform.DOMove(from + randomDirection, PUSH_DURATION).SetEase(Ease.OutFlash))
                .Append(image.rectTransform.DOMove(toText.rectTransform.position, randomMoveDuration).SetEase(Ease.OutFlash))
                .Join(image.rectTransform.DOScale(END_PARTICLE_SIZE, randomMoveDuration).SetEase(Ease.OutFlash))
                .AppendCallback(() => Object.Destroy(image.gameObject))
                .onComplete += () =>
                {
                    currentValue += addForPulce;
                    toText.text = currentValue.ToShortNumber();
                    _pulceSequence?.Kill();
                    _pulceSequence = DOTween.Sequence()
                    .Append(toText.rectTransform.DOScale(EARN_PULCE_SIZE, EARN_PULCE_DURATION / 2f).SetEase(Ease.OutFlash))
                    .Append(toText.rectTransform.DOScale(1f, EARN_PULCE_DURATION / 2f).SetEase(Ease.OutFlash));
                };

        }

    }

    public EarnAnimation(Vector2 from, TextMeshProUGUI toText, float fromValue, float toValue, EarnAnimationType animationType)
    {
        var settings = ConfigHub.EarnAnimations.GetSettings(animationType);
        new EarnAnimation(from, toText, fromValue, toValue, settings.sprite, settings.amount);
    }





}
