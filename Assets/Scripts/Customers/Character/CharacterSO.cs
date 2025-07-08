using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public Sprite sprite;
    public DrinkScriptVersion desiredDrink;
    public DialogueSO dialogueToPlay;
    public DialogueSO dialogueDrinkCorrect;
    public DialogueSO dialogueDrinkIncorrect;
    public string drinkHint;
    public int characterIndex;
}
