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
    
    public static RealDrink Instance;
    public bool drinkMade;
    public DrinkSO drinkGiven;

    private void Awake()
    {
        Instance = this;
    }

    public void DrinkMade(DrinkSO drink)
    {
        image.color = drink.color;
        drinkGiven = drink;
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        image.DOFade(255, 0.1f);
        transform.DOMove(new Vector3(transform.position.x - movementRange, transform.position.y), moveDuration);
        yield return new WaitForSeconds(waitDuration + moveDuration);
        image.DOFade(0, 0.1f);
        transform.DOMove(new Vector3(transform.position.x + movementRange, transform.position.y), 0.1f);
        yield return new WaitForSeconds(moveDuration);
        image.DOFade(255, 0.1f);
        drinkMade = true;
        yield return new WaitForSeconds(moveDuration);
        drinkGiven = null;
    }
}
