using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    [TextArea(4, 8)] public List<string> dialogueLines;
    public float charDelayInterval;
    private int index;

   
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Go to next line if line has finished typing. Otherwise, skip forward to display the full line on click.
            if (textComponent.text == dialogueLines[index])
            {
                NextLine();
            }
            else
            {
                // Pause the typing of the current line and display the full line
                StopAllCoroutines();
                textComponent.text = dialogueLines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Type each character in the line one by one
        foreach(char c in dialogueLines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(charDelayInterval);
        }
    }

    void NextLine()
    {
        if (index < dialogueLines.Count - 1)
        {
            index++;
            // Clears the existing dialogue
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
}
