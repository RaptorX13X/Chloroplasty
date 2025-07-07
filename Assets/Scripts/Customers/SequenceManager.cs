using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private CharacterSO[] characters;
    [SerializeField] private float waitDuration;
    private int characterIndex;
    [SerializeField] private IngredientButton[] ingredientButtons;
    [SerializeField] private TextMeshProUGUI dialogueArea;
    [SerializeField] private DialogueSO lossDialogue;
    [SerializeField] private DialogueSO startDialogue;
    [SerializeField] private DialogueSO endDialogue;
    
    public static SequenceManager instance;

    [Header("MC")] 
    [SerializeField] private Image mcSprite;
    [SerializeField] private Image mcSpriteHead;
    [Header("Tiefling")]
    [SerializeField] private Image tieflingNeutral;
    [SerializeField] private Image tieflingNeutralEye;
    [SerializeField] private Image tieflingHappy;
    [SerializeField] private Image tieflingAngry;
    [SerializeField] private Image tieflingAngryEye;
    [Header("SwampLady")]
    [SerializeField] private Image swampLadyNeutral;
    [SerializeField] private Image swampLadyNeutralBlob;
    [SerializeField] private Image swampLadyHappy;
    [SerializeField] private Image swampLadyHappyBlob;
    [SerializeField] private Image swampLadyAngry;
    [SerializeField] private Image swampLadyAngryBlob;
    [Header("Bug")] 
    [SerializeField] private Image bugNeutral;
    [SerializeField] private Image bugNeutral1;
    [SerializeField] private Image bugNeutral2;
    [SerializeField] private Image bugAngry;
    [SerializeField] private Image bugAngry1;
    [SerializeField] private Image bugAngry2;
    [SerializeField] private Image bugHappy;
    [SerializeField] private Image bugHappy1;
    [SerializeField] private Image bugHappy2;
    

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
        foreach (IngredientButton ingredientButton in ingredientButtons)
        {
            ingredientButton.inCup = true;
        }
        McFadeIn();
        yield return new WaitForSeconds(fadeInDuration);
        DialogueManager.Instance.StartDialogue(startDialogue);
        yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);
        yield return new WaitForSeconds(0.5f);
        McFadeOut();
        yield return new WaitForSeconds(fadeInDuration);
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        yield return new WaitForSeconds(waitDuration);
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
        dialogueArea.text = characters[characterIndex].desiredDrink.drinkName + characters[characterIndex].drinkHint;
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
        if (characterIndex < characters.Length)
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
        McFadeIn();
        yield return new WaitForSeconds(fadeInDuration);
        DialogueManager.Instance.StartDialogue(lossDialogue);
        yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);
        McFadeOut();
        yield return new WaitForSeconds(fadeInDuration);
        InGameMenus.instance.OnLoss();
    }

    private IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(0.5f);
        McFadeIn();
        yield return new WaitForSeconds(fadeInDuration);
        DialogueManager.Instance.StartDialogue(endDialogue);
        yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);
        yield return new WaitForSeconds(0.5f);
        McFadeOut();
        yield return new WaitForSeconds(fadeInDuration);
        InGameMenus.instance.OnEnd();
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
    }

    private void SpawnCharacter()
    {
        switch (currentCharacterIndex)
        {
            case 0:
                tieflingNeutral.DOFade(1f, fadeInDuration);
                tieflingNeutralEye.DOFade(1f, fadeInDuration);
                break;
            case 1: 
                swampLadyNeutral.DOFade(1f, fadeInDuration);
                swampLadyNeutralBlob.DOFade(1f, fadeInDuration);
                break;
            case 2:
                bugNeutral.DOFade(1f, fadeInDuration);
                bugNeutral1.DOFade(1f, fadeInDuration);
                bugNeutral2.DOFade(1f, fadeInDuration);
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
                tieflingAngry.DOFade(1f, 0);
                tieflingAngryEye.DOFade(1f, 0);
                break;
            case 1:
                swampLadyNeutral.DOFade(0, 0f);
                swampLadyNeutralBlob.DOFade(0, 0f);
                swampLadyAngry.DOFade(1f, 0f);
                swampLadyAngryBlob.DOFade(1f, 0f);
                break;
            case 2:
                bugNeutral.DOFade(0, 0f);
                bugNeutral1.DOFade(0, 0f);
                bugNeutral2.DOFade(0, 0f);
                bugAngry.DOFade(1f, 0f);
                bugAngry1.DOFade(1f, 0f);
                bugAngry2.DOFade(1f, 0f);
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
                tieflingHappy.DOFade(1f, 0);
                break;
            case 1:
                swampLadyNeutral.DOFade(0, 0f);
                swampLadyNeutralBlob.DOFade(0, 0f);
                swampLadyHappy.DOFade(1f, 0);
                swampLadyHappyBlob.DOFade(1f, 0);
                break;
            case 2:
                bugNeutral.DOFade(0, 0f);
                bugNeutral1.DOFade(0, 0f);
                bugNeutral2.DOFade(0, 0f);
                bugHappy.DOFade(1f, 0);
                bugHappy2.DOFade(1f, 0);
                bugHappy1.DOFade(1f, 0);
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
            case 1:
                swampLadyNeutral.DOFade(0, fadeInDuration);
                swampLadyNeutralBlob.DOFade(0, fadeInDuration);
                swampLadyHappy.DOFade(0, fadeInDuration);
                swampLadyHappyBlob.DOFade(0, fadeInDuration);
                swampLadyAngry.DOFade(0, fadeInDuration);
                swampLadyAngryBlob.DOFade(0, fadeInDuration);
                break;
            case 2:
                bugNeutral.DOFade(0, 0f);
                bugNeutral1.DOFade(0, 0f);
                bugNeutral2.DOFade(0, 0f);
                bugAngry.DOFade(0f, 0);
                bugAngry1.DOFade(0f, 0);
                bugAngry2.DOFade(0f, 0);
                bugHappy.DOFade(0f, 0);
                bugHappy2.DOFade(0f, 0);
                bugHappy1.DOFade(0f, 0);
                break;
        }
    }

    private void McFadeIn()
    {
        mcSprite.DOFade(1f, fadeInDuration);
        mcSpriteHead.DOFade(1f, fadeInDuration);
    }

    private void McFadeOut()
    {
        mcSprite.DOFade(0f, fadeInDuration);
        mcSpriteHead.DOFade(0f, fadeInDuration);
    }
}
