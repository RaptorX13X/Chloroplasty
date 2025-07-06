using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public Sprite sprite;
    public DrinkSO desiredDrink;
    public DialogueSO dialogueToPlay;
    public DialogueSO dialogueDrinkCorrect;
    public DialogueSO dialogueDrinkIncorrect;
    public string drinkName;
    public string drinkHint;
}
