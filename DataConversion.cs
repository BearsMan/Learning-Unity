using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataConversion
{
    public static string ItemsToString(Item[] items)
    {
        string output = "";
        foreach (Item item in items)
        {
            if (item != null)
            {
                output += item.itemID + "/";
            }
            else
            {
                output += " /";
            }
        }
        return output;
    }

    public static Item[]StringToItems(string data)
    {
        Item[] allItems = Resources.LoadAll<Item>("");
        List<Item> items = new List<Item>();
        string[] dataArray = data.Split(char.Parse("/"));
        foreach (string item in dataArray)
        {
            bool foundMatch = false;
            foreach(Item i in allItems)
            {
                if (i.itemID == item)
                {
                    items.Add(i);
                    foundMatch = true;
                    break;
                }
            }
            if (!foundMatch)
            {
                items.Add(null);
            }
            Debug.Log(item);
        }
        return items.ToArray();
    }

    public static string QuestsToString(Quest[] quests)
    {
        string output = "";
        foreach (Quest quest in quests)
        {
            if (quest != null)
            {
                output += quest.questID + ",";
                foreach(QuestTask task in quest.tasks)
                {
                    //required item
                    //number to collect
                    //target character
                    //things to kill
                    //number to kill
                    //target location
                    //escort target
                }
            }
            else
            {
                output += " /";
            }
        }
        return output;
    }

    public static Quest[] StringToQuests(string data)
    {
        Quest[] allQuests = Resources.LoadAll<Quest>("");
        List<Quest> quests = new List<Quest>();
        string[] dataArray = data.Split(char.Parse("/"));
        foreach (string quest in dataArray)
        {
            bool foundMatch = false;
            foreach (Quest i in allQuests)
            {
                if (i.questID == quest)
                {
                    quests.Add(i);
                    foundMatch = true;
                    break;
                }
            }
            if (!foundMatch)
            {
                quests.Add(null);
            }
            
        }
        return quests.ToArray();
    }
}
