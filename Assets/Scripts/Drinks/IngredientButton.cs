using UnityEngine;

public class IngredientButton : MonoBehaviour
{
    [SerializeField] private IngredientScriptVersion ingredient;
    public bool inCup;

    public void OnClicked()
    {
        if (inCup) return;
        if (MixingStation.Instance.amountInShaker < 3)
        {
            MixingStation.Instance.SelectIngredient(ingredient);
            inCup = true;
        }
    }
}
