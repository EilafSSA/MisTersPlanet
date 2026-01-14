using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [System.Serializable]
    public class QuestState
    {
        public string questName;
        public int value;
    }

    [Header("Current Game State")]
    public List<QuestState> allQuests = new List<QuestState>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public int GetQuestStatus(string questID)
    {
        foreach (var q in allQuests)
        {
            if (q.questName == questID) return q.value;
        }
        return 0;
    }

    public void SetQuestStatus(string questID, int newValue)
    {
        foreach (var q in allQuests)
        {
            if (q.questName == questID)
            {
                q.value = newValue;
                return;
            }
        }
        QuestState newQ = new QuestState();
        newQ.questName = questID;
        newQ.value = newValue;
        allQuests.Add(newQ);
    }
}