using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickUp : MonoBehaviour, IInteractable
{
    public Outline objectOutline;
    public Item itemOnGround;

    public bool CanInteract(bool isPlayerInteraction)
    {
        return isPlayerInteraction;
    }

    private void Start()
    {
        StopHover();
    }

    public void Interact(PlayerMovement player)
    {
        if (player.TryGetComponent(out Inventory playerInventory))
        {
            if (playerInventory.AddToBackPack(itemOnGround))
            {
                Destroy(gameObject);
            }
            else
            {
                objectOutline.OutlineColor = Color.red;
            }
        }
    }

    public void StartHover()
    {
        objectOutline.enabled = true;
        objectOutline.OutlineColor = itemOnGround.itemColor;
    }

    public void StopHover()
    {
        objectOutline.enabled = false;

    }
}
