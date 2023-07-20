using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    // This script will be used to track quests
    public QuestLog activeQuest;
    private QuestLog currentQuest = null;
    public List<QuestLog> notCompleted;
    public List<QuestLog> completed;
    public List<QuestLog> failed;
    public List<QuestTask> tasks;
    public bool takeQuests = false; //Some quests can be done more then once depending on the type of quest that is current active
    private bool isQuestActive = false;
    private bool isQuestCompleted = false;
    private bool isQuestFailed = false;
    public int numberOfQuests = 0;
    public Image questArrow;
    public Image questIcon;
    public Image questText;
    public Image greenStar;

    public void StartQuest(QuestLog log, QuestTask questTask)
    {
        activeQuest = log;
        completed = new List<QuestLog>();
        failed = new List<QuestLog>(failed.Count);

        if (activeQuest != null)
        {
            questTask = new QuestTask();
        }
        foreach (QuestTask task in tasks)
        {
            //Find each quest task name and label it
        }
    }
    public enum QuestReward
    {
        XPGain,
        ItemsRewarded,
        AmontOfMoneyGiven
    }
    public enum QuestType
    {
        bringItemBacktoNPC,
        killAnEnemy,
        runToSpecificNPC
    }
    private void Awake()
    {
        while (true)
        {
            for (int i = 0; i < numberOfQuests; i++)
            {
                activeQuest = null;
            }
            if (numberOfQuests == 0)
            {
                isQuestCompleted = false;
            }
            else
            {
                isQuestCompleted = true;
            }
        }
    }
    public void DirectionPoint(QuestLog log)
    {
        Physics.gravity = Vector3.zero;
    }
    public struct TrackQuest
    {
        public string questName;
        public string questDescription;
        public string rarity;
        public string reward;
        public bool getActiveQuest;
        public bool isQuestCompleted;
        public bool isQuestFailed;
    }
}
