using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private CharacterSO[] characters;
    // [SerializeField] private Image characterImage;
    // [SerializeField] private Transform characterTransform;
    // [SerializeField] private float movementRange;
    // [SerializeField] private float moveDuration;
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

    [Header("Tiefling")]
    [SerializeField] private GameObject tieflingParent;
    [SerializeField] private Image tieflingNeutral;
    [SerializeField] private Image tieflingNeutralEye;
    [SerializeField] private Image tieflingHappy;
    [SerializeField] private Image tieflingAngry;
    [SerializeField] private Image tieflingAngryEye;

    [SerializeField] private float fadeInDuration;
    private int currentCharacterIndex;

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
        ChooseCharacter();
        yield return new WaitForSeconds(0.1f);
        SpawnCharacter();
        yield return new WaitForSeconds(fadeInDuration);
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
            CharacterHappy();
            DialogueManager.Instance.StartDialogue(characters[characterIndex].dialogueDrinkCorrect);
        }
        else
        {
            CharacterAngry();
            DialogueManager.Instance.StartDialogue(characters[characterIndex].dialogueDrinkIncorrect);
            yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive); 
            PointsManager.instance.LosePoints();
        }
        yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);
        DespawnCharacter();
        characterIndex++;
        yield return new WaitForSeconds(fadeInDuration);
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

    private void ChooseCharacter()
    {
        currentCharacterIndex = characters[characterIndex].characterIndex;
        switch (currentCharacterIndex)
        {
            case 0:
                tieflingParent.SetActive(true);
                break;
        }
    }

    private void SpawnCharacter()
    {
        switch (currentCharacterIndex)
        {
            case 0:
                tieflingNeutral.DOFade(255, fadeInDuration);
                tieflingNeutralEye.DOFade(255, fadeInDuration);
                break;
        }
    }

    private void CharacterAngry()
    {
        switch (currentCharacterIndex)
        {
            case 0:
                tieflingNeutral.DOFade(0, 0f);
                tieflingNeutralEye.DOFade(0, 0f);
                tieflingAngry.DOFade(255, 0);
                tieflingAngryEye.DOFade(255, 0);
                break;
        }
    }

    private void CharacterHappy()
    {
        switch (currentCharacterIndex)
        {
            case 0:
                tieflingNeutral.DOFade(0, 0f);
                tieflingNeutralEye.DOFade(0, 0f);
                tieflingHappy.DOFade(255, 0);
                break;
        }
    }

    private void DespawnCharacter()
    {
        switch (currentCharacterIndex)
        {
            case 0:
                tieflingNeutral.DOFade(0, fadeInDuration);
                tieflingNeutralEye.DOFade(0, fadeInDuration);
                tieflingAngry.DOFade(0, fadeInDuration);
                tieflingAngryEye.DOFade(0, fadeInDuration);
                tieflingHappy.DOFade(0, fadeInDuration);
                break;
        }
    }
}
