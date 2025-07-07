using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RealDrink : MonoBehaviour
{
    [SerializeField] private float liftAmount = 50f;
    [SerializeField] private float liftDuration = 0.5f;
    [SerializeField] private float rotateDuration = 1f;

    [SerializeField] private float shakeDuration = 3f;
    [SerializeField] private float shakeStrength = 4f;
    [SerializeField] private int shakeVibrato = 10;
    [SerializeField] private float shakeRandomness = 90f;

    [SerializeField] private float settleDuration = 0.5f;

    [SerializeField] private float movementRange;
    [SerializeField] private float moveDuration;
    [SerializeField] private float waitDuration;
    [SerializeField] private float tiltAngle = 15f;
    [SerializeField] private float tiltDuration = 0.2f;

    [SerializeField] private Image image;
    [SerializeField] private Animator shakerCap;
    [SerializeField] private Sprite shaker;
    [SerializeField] private Image cap;

    public static RealDrink Instance;
    public bool drinkMade;
    public DrinkSO drinkGiven;

    private void Awake()
    {
        Instance = this;
    }

    public void DrinkMade(DrinkSO drink)
    {
        image.sprite = drink.icon;
        drinkGiven = drink;
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        RectTransform rt = transform as RectTransform;
        Vector3 startLocalPos = rt.localPosition;
        Quaternion startLocalRot = rt.localRotation;

        yield return rt.DOLocalMoveY(startLocalPos.y + liftAmount, liftDuration).WaitForCompletion();

        yield return rt.DOLocalRotate(new Vector3(0f, 0f, -45f), rotateDuration).WaitForCompletion();
        Vector3 localShakeDir = rt.up * shakeStrength;
        Tween shakeTween = DOTween.To(
            () => rt.localPosition,
            pos => rt.localPosition = pos,
            rt.localPosition + localShakeDir,
            shakeDuration / (shakeVibrato * 2)
        ).SetLoops(shakeVibrato * 2, LoopType.Yoyo).SetEase(Ease.InOutSine);
        yield return shakeTween.WaitForCompletion();
        
        yield return rt.DOLocalRotate(startLocalRot.eulerAngles, settleDuration).WaitForCompletion();
        yield return rt.DOLocalMove(startLocalPos, settleDuration).WaitForCompletion();

        shakerCap.SetTrigger("open");
        yield return new WaitForSeconds(waitDuration);
        cap.CrossFadeAlpha(0f, 0f, true);
        yield return new WaitForSeconds(0.01f);

        Vector3 leftPos = new Vector3(startLocalPos.x - movementRange, startLocalPos.y, startLocalPos.z);
        yield return rt.DOLocalMove(leftPos, moveDuration).WaitForCompletion();
        yield return rt.DOLocalRotate(new Vector3(0f, 0f, tiltAngle), tiltDuration).WaitForCompletion();
        yield return rt.DOLocalRotate(Vector3.zero, tiltDuration).WaitForCompletion();
        yield return new WaitForSeconds(3f);

        image.sprite = shaker;
        cap.CrossFadeAlpha(255f, 0f, true);
        yield return new WaitForSeconds(0.01f);
        shakerCap.SetTrigger("close");
        drinkMade = true;
        yield return new WaitForSeconds(moveDuration);
        drinkGiven = null;
    }
}