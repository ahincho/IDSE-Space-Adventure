using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;

    [SerializeField] private float textSpeed = 0.2f;
    [SerializeField] private GameObject dialogueController;
    [SerializeField] private FuelController fuel;
    [SerializeField]private ControlDeNave shipControl;

    private int index = 0;
    private float autoCloseDelay = 3f;
    private bool dialogueInProgress = false;

    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (dialogueInProgress && Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
        }
    }

    public void StartDialogue()
    {
        dialogueInProgress = true;
        shipControl.setCanMove(false);
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
        yield return new WaitForSecondsRealtime(autoCloseDelay);
        NextLine();
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueInProgress = false;
        fuel.Restore(100);
        shipControl.setCanMove(true);
        dialogueController.SetActive(false);
        gameObject.SetActive(false);
    }
}
