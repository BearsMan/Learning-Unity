using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class QuestLog
{
    public string objectives, walkThrough;
    public string initialDialogue, intermediateDialogue, rewardDialogue;
    public string description;
    public int moneyReward;
    public int itemReward;
    public int experienceReward;
    //item Reward
    public int questID;
}