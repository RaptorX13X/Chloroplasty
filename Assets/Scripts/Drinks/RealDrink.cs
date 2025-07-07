using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RealDrink : MonoBehaviour
{
    [SerializeField] private float movementRange;
    [SerializeField] private float moveDuration;
    [SerializeField] private float waitDuration;
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
        shakerCap.SetTrigger("open");
        yield return new WaitForSeconds(waitDuration);
        cap.CrossFadeAlpha(0f, 0f, true);
        yield return new WaitForSeconds(0.01f);
        transform.DOMove(new Vector3(transform.position.x - movementRange, transform.position.y), moveDuration);
        yield return new WaitForSeconds(waitDuration + moveDuration);
        transform.DOMove(new Vector3(transform.position.x + movementRange, transform.position.y), 0.1f);
        yield return new WaitForSeconds(moveDuration);
        image.sprite = shaker;
        cap.CrossFadeAlpha(255f, 0f, true);
        yield return new WaitForSeconds(0.01f);
        shakerCap.SetTrigger("close");
        drinkMade = true;
        yield return new WaitForSeconds(moveDuration);
        drinkGiven = null;
    }
}
