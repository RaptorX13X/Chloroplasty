using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MixingStation : MonoBehaviour
{
    [SerializeField] private DrinkSO[] drinks;
    public static MixingStation Instance;
    private List<Ingredient> ingredients = new List<Ingredient>();
    public int amountInShaker;
    [SerializeField] private IngredientButton[] ingredientButtons;
    private DrinkSO currentDrink;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
        amountInShaker++;
        Debug.Log(amountInShaker);
        foreach (var slop in ingredients)
        {
            Debug.Log(slop.name);
        }
    }

    public void FinishDrink()
    {
        foreach (var drink in drinks)
        {
            if (drink.ingredients[0] == ingredients[0] || drink.ingredients[0] == ingredients[1] || drink.ingredients[0] == ingredients[2])
            {
                if (drink.ingredients[1] == ingredients[0] || drink.ingredients[1] == ingredients[1] || drink.ingredients[1] == ingredients[2])
                {
                    if (drink.ingredients[2] == ingredients[0] || drink.ingredients[2] == ingredients[1] || drink.ingredients[2] == ingredients[2])
                    {
                        currentDrink = drink;
                    }
                }
            }
        }
        ingredients.Clear();
        foreach (var button in ingredientButtons)
        {
            button.inCup = false;
        }

        amountInShaker = 0;
        
        RealDrink.Instance.DrinkMade(currentDrink);
    }

    public void PourOut()
    {
        ingredients.Clear();
        amountInShaker = 0;
        foreach (var button in ingredientButtons)
        {
            button.inCup = false;
        }
        Debug.Log(amountInShaker);
        foreach (var slop in ingredients)
        {
            Debug.Log(slop.name);
        }
    }
}
