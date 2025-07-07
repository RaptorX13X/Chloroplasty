using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSO", menuName = "IngredientSO")]
public class Ingredient : ScriptableObject
{
    public string name;
}

[CreateAssetMenu(fileName = "DrinkSO", menuName = "DrinkSO")]
public class DrinkSO : ScriptableObject
{
    public string drinkName;
    public Ingredient[] ingredients;
    public Color color;
}
