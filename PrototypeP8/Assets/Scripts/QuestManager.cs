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
    private bool questAlmostCompleted = false;  

    public bool QuestAlmostCompleted
    {
        get { return questAlmostCompleted; }
    }
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
            questText.text = $"Quetes en cours : {currentQuestDescription} {applesCollected}/{applesRequired}";
        }
        else if (questAlmostCompleted)
        {
            questText.gameObject.SetActive(true);  
            questText.text = "Quetes en cours : veuillez parler au PNJ";
        }
        else
        {
            questText.gameObject.SetActive(false);
        }
    }


    public void StartQuest(string questDescription)
    {
        currentQuestDescription = questDescription;
        questInProgress = true;
        applesCollected = 0;  
        questAlmostCompleted = false;
    }


    public void CollectApple()
    {
        if (questInProgress && applesCollected < applesRequired)
        {
            applesCollected++;
            if (applesCollected >= applesRequired)
            {
                questAlmostCompleted = true;
                questInProgress = false; 
            }
        }
    }

    public void ResetQuest()
    {
        questInProgress = false;
        applesCollected = 0;
        questAlmostCompleted = false;
    }
}
