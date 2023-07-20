using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Quest")]
public class Quest : ScriptableObject
{
    public string questID;
    public string questDescription;
    public string questName;
    public int rewardXP;
    public Item[] guaranteedRewards;
    public Item[] chosenRewards;
    public QuestTask[] tasks = new QuestTask[1];
}
[System.Serializable]
public struct QuestTask
{
   
    public enum TASKTYPE
    {
        fetch, // To bring an item back to the NPC for example
        kill, // Kill a certain number of enemies
        location, // get to a location
        delivery, // get item and bring it to another person or place
        escort, // to get the NPC to the location safely without dying
    }
    public TASKTYPE type;
    public string taskDescription;
    // Fetch
    
    public Item requiredItem;
    public int numberToCollect;
    public NPCData targetCharacter; // Also use for delivery

    // Kill
    
    public NPCData thingsToKill;
    
    public int numberToKill;
    public LocationData targetLocation; // Also used for location quest

    // Escort
    public NPCData escortTarget;

    public QuestTask(TASKTYPE type, string taskDescription) : this()
    {
        this.type = type;
        this.taskDescription = taskDescription;
    }
}
/*
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Quest myQuest = target as Quest;

        myQuest.questID = EditorGUILayout.TextField("Quest ID", myQuest.questID);
        myQuest.questDescription = EditorGUILayout.TextField("Quest Description", myQuest.questDescription);
        myQuest.questName = EditorGUILayout.TextField("Quest Name", myQuest.questName);
        myQuest.rewardXP = EditorGUILayout.IntField("XP Reward", myQuest.rewardXP);
        

        foreach (QuestTask task in myQuest.tasks)
        {
            QuestTask currentTask = task;
            switch (task.type)
            {
                case QuestTask.TASKTYPE.fetch:
                    currentTask.requiredItem = EditorGUILayout.ObjectField("Required Item", task.requiredItem, typeof(Item), true) as Item;
                    currentTask.targetCharacter = EditorGUILayout.ObjectField("Delivery Target Character", task.targetCharacter, typeof(NPCData), true)as NPCData;
                    break;
                case QuestTask.TASKTYPE.kill:
                    break;
                case QuestTask.TASKTYPE.location:
                    break;
                case QuestTask.TASKTYPE.delivery:
                    break;
                case QuestTask.TASKTYPE.escort:
                    break;
            }
        }
    }
}
*/