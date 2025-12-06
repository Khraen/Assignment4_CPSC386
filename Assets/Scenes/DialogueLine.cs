using UnityEngine;
[System.Serializable]
public class DialogueLine
{
    public GameObject characterHead; // the UI Image for the head
    [TextArea(2,5)]
    public string text; // what they say
}
