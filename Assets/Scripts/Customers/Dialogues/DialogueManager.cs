using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    //public Image characterIcon;
    public TextMeshProUGUI dialogueArea;
    public TextMeshProUGUI characterNameArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.08f;
    private float cachedSpeed;

    public DialogueSO currentDialogue; 
    private DialogueLine currentLine;

    private bool isDialogueTyping;

    [SerializeField] private float timeBetweenLines;
    private float timeForLines;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
        timeForLines = timeBetweenLines;
        cachedSpeed = typingSpeed;
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        currentDialogue = dialogue;
        isDialogueActive = true;

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    private void Update()
    {
        if (!isDialogueActive) return;
        if (!isDialogueTyping && timeForLines >= 0.1f)
        {
            timeForLines -= Time.deltaTime;
        }
        else if (!isDialogueTyping && timeForLines <= 0.1f)
        {
            timeForLines = timeBetweenLines;
            //DisplayNextDialogueLine();
        }
    }

    public void DialogueButton()
    {
        if (isDialogueTyping)
        {
            typingSpeed = 0.01f;
        }
        else
        {
            typingSpeed = cachedSpeed;
            DisplayNextDialogueLine();
        }
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            currentDialogue.isComplete = true;
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();

        //characterIcon.sprite = currentLine.character.icon;
        characterNameArea.text = currentLine.character.name;

        dialogueArea.gameObject.SetActive(true);
        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        isDialogueTyping = true;
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            if (letter.ToString() == "<" || letter.ToString() == "/" ||letter.ToString() == "i" ||letter.ToString() == ">" || letter.ToString() == "b")
            {
                dialogueArea.text += letter;
                continue;
            }
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isDialogueTyping = false;
    }

    void EndDialogue()  
    {
        isDialogueActive = false;
        dialogueArea.text = "";
        characterNameArea.text = "";
    }
}