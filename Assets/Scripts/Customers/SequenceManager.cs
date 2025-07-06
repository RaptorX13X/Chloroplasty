using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private CharacterSO[] characters;
    [SerializeField] private Image characterImage;
    [SerializeField] private Transform characterTransform;
    //SO points/stars/whatever
    [SerializeField] private float movementRange;
    [SerializeField] private float moveDuration;
    [SerializeField] private float waitDuration;
    private int characterIndex;
    [SerializeField] private IngredientButton[] ingredientButtons; 

    private void Start()
    {
        characterIndex = 0;
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        RealDrink.Instance.drinkMade = false;
        foreach (IngredientButton ingredientButton in ingredientButtons)
        {
            ingredientButton.inCup = true;
        }
        characterImage.sprite = characters[characterIndex].sprite;
        characterImage.DOFade(255, 0.1f);
        characterTransform.DOMove(new Vector2(characterTransform.position.x + movementRange, characterTransform.position.y), moveDuration);
        yield return new WaitForSeconds(waitDuration + moveDuration);
        TriggerDialogue();
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);
        foreach (IngredientButton ingredientButton in ingredientButtons)
        {
            ingredientButton.inCup = false;
        }
        yield return new WaitUntil(() => RealDrink.Instance.drinkMade);
        if (characters[characterIndex].desiredDrink == RealDrink.Instance.drinkGiven)
        {
            //gib point
            DialogueManager.Instance.StartDialogue(characters[characterIndex].dialogueDrinkCorrect);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(characters[characterIndex].dialogueDrinkIncorrect);
        }
        yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);
        characterImage.DOFade(0, 0.1f);
        characterTransform.DOMove(new Vector2(characterTransform.position.x - movementRange, characterTransform.position.y), 0.1f);
        characterIndex++;
        yield return new WaitForSeconds(2f);
        if (characterIndex <= characters.Length)
        {
            StartCoroutine(Sequence());
        }
    }
    
    private void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(characters[characterIndex].dialogueToPlay);
    }
}
