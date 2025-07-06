using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]

public class DialogueSO : ScriptableObject
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    public bool isComplete = false;
}