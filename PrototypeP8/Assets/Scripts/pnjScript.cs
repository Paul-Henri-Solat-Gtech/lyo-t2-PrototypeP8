using UnityEngine;
using TMPro;

public class PNJ : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public string[] dialogues;
    public string[] dialoguesCompleted;
    public AudioClip firstSound;
    public AudioClip secondSound;
    public AudioClip thirdSound;
    private AudioSource audioSource;
    private int index = 0;
    private bool isPlayerNear = false;
    private bool secondSoundPlayed = false;
    private bool firstSoundPlayed = false;

    void Start()
    {
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
        secondSoundPlayed = false;
        firstSoundPlayed = false;
        dialogueUI.SetActive(true);

        if (QuestManager.instance != null && QuestManager.instance.QuestAlmostCompleted)
        {
            dialogueText.text = dialoguesCompleted[index];
        }
        else
        {
            dialogueText.text = dialogues[index];
        }

        if (firstSound != null && audioSource != null && !firstSoundPlayed)
        {
            audioSource.PlayOneShot(firstSound);
            firstSoundPlayed = true;
        }
    }

    void NextDialogue()
    {
        index++;

        bool dialogueInProgress = false;

        if (QuestManager.instance != null && QuestManager.instance.QuestAlmostCompleted)
        {
            if (index < dialoguesCompleted.Length)
            {
                dialogueText.text = dialoguesCompleted[index];
                dialogueInProgress = true;
            }
            else
            {
                QuestManager.instance.ResetQuest();
                EndDialogue();
            }
        }
        else
        {
            if (index < dialogues.Length)
            {
                dialogueText.text = dialogues[index];
                dialogueInProgress = true;
            }
            else
            {
                if (!QuestManager.instance.QuestInProgress)
                {
                    QuestManager.instance.StartQuest("Récolter 3 pommes");
                }
                EndDialogue();
            }
        }

        if (dialogueInProgress && audioSource != null)
        {
            if (!secondSoundPlayed && secondSound != null)
            {
                audioSource.PlayOneShot(secondSound);
                secondSoundPlayed = true;
            }
            else if (thirdSound != null)
            {
                audioSource.PlayOneShot(thirdSound);
            }
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




