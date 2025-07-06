using UnityEngine;

public class IngredientButton : MonoBehaviour
{
    [SerializeField] private Ingredient ingredient;
    public bool selected;
    [SerializeField] private IngredientButton[] buttons;
    public bool inCup;

    public void OnClicked()
    {
        if (inCup) return;
        if (MixingStation.Instance.amountInShaker < 3)
        {
            if (selected)
            {
                MixingStation.Instance.SelectIngredient(ingredient);
                inCup = true;
            }
            else
            {
                selected = true;
                foreach (var button in buttons)
                {
                    button.selected = false;
                }
            }
        }
    }
}
