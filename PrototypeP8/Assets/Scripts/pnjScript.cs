using UnityEngine;
using TMPro; 

public class PNJ : MonoBehaviour
{
    public GameObject dialogueUI;  
    public TextMeshProUGUI dialogueText;  
    public string[] dialogues;  
    private int index = 0;
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            dialogueUI.SetActive(true);
            if (!dialogueUI.activeInHierarchy)
            {
                StartDialogue();
            }
            else
            {
                NextDialogue();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        dialogueUI.SetActive(true);
        dialogueText.text = dialogues[index];
    }

    void NextDialogue()
    {
        index++;
        if (index < dialogues.Length)
        {
            dialogueText.text = dialogues[index];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            EndDialogue();
        }
    }
}
