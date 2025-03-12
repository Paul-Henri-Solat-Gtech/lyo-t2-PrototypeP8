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
        Debug.Log("Qu�te accept�e : Ramasse 3 pommes ");
    }

    public void CollectApple()
    {
        if (questAccepted)
        {
            applesCollected++;
            Debug.Log($"Pommes collect�es : {applesCollected}/{applesRequired}");

            if (applesCollected >= applesRequired)
            {
                Debug.Log("Qu�te termin�e  Retourne voir le PNJ.");
            }
        }
    }
}
