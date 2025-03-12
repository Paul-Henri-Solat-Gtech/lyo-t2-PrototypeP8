using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private bool questAccepted = false;
    private int applesCollected = 0;
    private int applesRequired = 3;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void StartQuest()
    {
        questAccepted = true;
        Debug.Log("Quête acceptée : Ramasse 3 pommes ");
    }

    public void CollectApple()
    {
        if (questAccepted)
        {
            applesCollected++;
            Debug.Log($"Pommes collectées : {applesCollected}/{applesRequired}");

            if (applesCollected >= applesRequired)
            {
                Debug.Log("Quête terminée  Retourne voir le PNJ.");
            }
        }
    }
}
