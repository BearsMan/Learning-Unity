using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    public IAction currentAction, previousAction;
    public void StartAction(IAction newAction)
    {
        previousAction = currentAction;
        if (currentAction == newAction)
        {
            return;
        }
        if (currentAction != null)
        {
            currentAction.Cancel();
        }
        currentAction = newAction;
    }

    public void CancelCurrentAction()
    {
        StartAction(null);
    }

    public void ResumePreviousAction()
    {
        currentAction = null;
        previousAction.Resume();
    }
}
