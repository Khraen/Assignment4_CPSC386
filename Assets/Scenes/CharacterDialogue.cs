using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    public TMP_Text dialogueText;       // Reference to your TextMeshPro text
    public float typingSpeed = 0.05f;   // Time between letters

    public DialogueLine[] lines;        // Array of all dialogue lines
    private int currentLineIndex = 0;
    private bool isTyping = false;
    public ScreenFader screenFader; // Drag your ScreenFader object here in Inspector
public string nextSceneName;     // The scene to load after dialogue

    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        currentLineIndex = 0;
        ShowNextLine();
    }

    public void ShowNextLine()
{
    if (currentLineIndex >= lines.Length)
    {
        // Hide all heads
        foreach (DialogueLine line in lines)
        {
            line.characterHead.SetActive(false);
        }

        // Fade to the next scene
        if (screenFader != null && !string.IsNullOrEmpty(nextSceneName))
        {
            screenFader.FadeToScene(nextSceneName);
        }

        dialogueText.text = "";
        return;
    }

    // Disable all heads first
    foreach (DialogueLine line in lines)
    {
        line.characterHead.SetActive(false);
    }

    // Enable current character's head
    DialogueLine currentLine = lines[currentLineIndex];
    currentLine.characterHead.SetActive(true);

    // Start typing
    StopAllCoroutines();
    StartCoroutine(TypeText(currentLine.text));
    currentLineIndex++;
}


    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        for (int i = 0; i <= line.Length; i++)
        {
            dialogueText.text = line.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // Optional: Call this to skip typing
    public void SkipOrNext()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = lines[currentLineIndex - 1].text; // show full line
            isTyping = false;
        }
        else
        {
            ShowNextLine();
        }
    }
}
