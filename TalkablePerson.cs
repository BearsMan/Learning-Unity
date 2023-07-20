using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(ActionScheduler))]
public class TalkablePerson : MonoBehaviour, IInteractable
{
    public ConversationPiece[] conversationPieces;
    public UnityEvent startHoverEvent;
    public UnityEvent endHoverEvent;

    public bool CanInteract(bool isPlayerInteraction)
    {
        return true;
    }

    public void Interact(PlayerMovement player)
    {
        GetComponent<ActionScheduler>().CancelCurrentAction();
        HUD.Instance.StartConverstation(conversationPieces);
    }

    

    public void StartHover()
    {
        startHoverEvent?.Invoke();
    }

    public void StopHover()
    {
        endHoverEvent?.Invoke();
    }

    

    
}
