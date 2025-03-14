using UnityEngine;
using TMPro;

public class PNJ : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public string[] dialogues;
    public string[] dialoguesCompleted;
    private int index = 0;
    private bool isPlayerNear = false;

    public GameObject reward;

    public AudioClip firstSound;
    public AudioClip secondSound;
    public AudioClip thirdSound;
    private AudioSource audioSource;

    private void Start()
    {
        reward.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

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

        if (QuestManager.instance != null && QuestManager.instance.QuestAlmostCompleted)
        {
            dialogueText.text = dialoguesCompleted[index];
        }
        else
        {
            dialogueText.text = dialogues[index];
        }

        PlaySound();
    }

    void NextDialogue()
    {
        index++;

        if (QuestManager.instance != null && QuestManager.instance.QuestAlmostCompleted)
        {
            if (index < dialoguesCompleted.Length)
            {
                dialogueText.text = dialoguesCompleted[index];
            }
            else
            {
                reward.SetActive(true);
                QuestManager.instance.ResetQuest();
                EndDialogue();
            }
        }
        else
        {
            if (index < dialogues.Length)
            {
                dialogueText.text = dialogues[index];
            }
            else
            {
                if (!QuestManager.instance.QuestInProgress)
                {
                    QuestManager.instance.StartQuest("Recolter 3 pommes");
                    Debug.Log("Quete demarrer !");
                }
                EndDialogue();
            }
        }

        PlaySound();
    }

    void PlaySound()
    {
        if (index == 0 && firstSound != null)
        {
            audioSource.PlayOneShot(firstSound);
        }
        else if (index == 1 && secondSound != null)
        {
            audioSource.PlayOneShot(secondSound);
        }
        else if (thirdSound != null)
        {
            audioSource.PlayOneShot(thirdSound);
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        index = 0;
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
