using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public TextMeshProUGUI questText;  
    private string currentQuestDescription = "";  
    private int applesCollected = 0; 
    private int applesRequired = 3;  
    private bool questInProgress = false; 
    private bool questCompleted = false;  

    public bool QuestInProgress
    {
        get { return questInProgress; }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (questInProgress)
        {
            questText.gameObject.SetActive(true);
            questText.text = $"Quêtes en cours : {currentQuestDescription} {applesCollected}/{applesRequired}";
        }
        else
        {
            questText.gameObject.SetActive(false);
        }

        if (questCompleted)
        {
            questText.text = "Quêtes en cours : veuillez parler au PNJ";
        }
    }

    public void StartQuest(string questDescription)
    {
        currentQuestDescription = questDescription;
        questInProgress = true;
        applesCollected = 0;  
        questCompleted = false;
    }


    public void CollectApple()
    {
        if (questInProgress && applesCollected < applesRequired)
        {
            applesCollected++;
            if (applesCollected >= applesRequired)
            {
                questCompleted = true;
                questInProgress = false; 
            }
        }
    }

    public void ResetQuest()
    {
        questInProgress = false;
        applesCollected = 0;
        questCompleted = false;
    }
}
