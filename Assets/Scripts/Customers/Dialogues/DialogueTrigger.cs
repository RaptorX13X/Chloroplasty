using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    //public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;

    public bool hasChoices;
    public string choice1;
    public string choice2;
    public AudioClip soundToPlay;

    public DialogueSO dialoguetoStartOn1;
    public DialogueSO dialoguetoStartOn2;
}

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSO dialogue;
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        Destroy(this);
    }

    public bool isComplete()
    {
        return dialogue.isComplete;
    }
}