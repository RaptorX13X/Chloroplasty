using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private CharacterSO[] characters;
    [SerializeField] private Image characterImage;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float movementRange;
    [SerializeField] private float moveDuration;
    [SerializeField] private float waitDuration;
    private int characterIndex;
    [SerializeField] private IngredientButton[] ingredientButtons;
    [SerializeField] private TextMeshProUGUI dialogueArea;

    [SerializeField] private Transform mainCharacterTransform;
    [SerializeField] private Image mainCharacterImage;
    [SerializeField] private DialogueSO lossDialogue;
    [SerializeField] private DialogueSO startDialogue;
    [SerializeField] private DialogueSO endDialogue;
    
    public static SequenceManager instance;

    private void Start()
    {
        instance = this;
        characterIndex = 0;
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(0.5f);
        mainCharacterImage.DOFade(255, 3f);
        yield return new WaitForSeconds(3f);
        DialogueManager.Instance.StartDialogue(startDialogue);
        yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);
        yield return new WaitForSeconds(0.5f);
        mainCharacterImage.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
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
        dialogueArea.text = characters[characterIndex].desiredDrink.drinkName;
        // lub drink hint zależnie od odkrycia
        foreach (IngredientButton ingredientButton in ingredientButtons)
        {
            ingredientButton.inCup = false;
        }
        yield return new WaitUntil(() => RealDrink.Instance.drinkMade);
        dialogueArea.text = "";
        if (characters[characterIndex].desiredDrink == RealDrink.Instance.drinkGiven)
        {
            DialogueManager.Instance.StartDialogue(characters[characterIndex].dialogueDrinkCorrect);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(characters[characterIndex].dialogueDrinkIncorrect);
            yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive); 
            PointsManager.instance.LosePoints();
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
        else
        {
            StartCoroutine(EndSequence());
        }
    }

    private IEnumerator LossSequence()
    {
        yield return new WaitForSeconds(0.5f);
        mainCharacterImage.DOFade(255, 3f);
        yield return new WaitForSeconds(3f);
        DialogueManager.Instance.StartDialogue(lossDialogue);
        yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);
        InGameMenus.instance.OnLoss();
    }

    private IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(0.5f);
        mainCharacterImage.DOFade(255, 3f);
        yield return new WaitForSeconds(3f);
        DialogueManager.Instance.StartDialogue(endDialogue);
        yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);
        yield return new WaitForSeconds(0.5f);
        mainCharacterImage.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        //end screen
    }

    public void OnLoss()
    {
        StopAllCoroutines();
        StartCoroutine(LossSequence());
    }
    
    private void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(characters[characterIndex].dialogueToPlay);
    }
}
