using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image itemImage;
    public delegate void SlotClicked(InventorySlot slot);
    public SlotClicked slotClicked;
    public void Clicked()
    {
        slotClicked.Invoke(this);
    }
}
