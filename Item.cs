using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class Item : ScriptableObject 
{
    public string itemID;
    public int value;
    public string description;
    public bool questItem;
    public Sprite itemImage;
    public rarity itemRarity;
    public Color itemColor
    {
        get
        {
            switch (itemRarity)
            {
                case rarity.common: return Color.white;
                case rarity.uncommon: return Color.blue;
                case rarity.masterwork: return Color.green;
                case rarity.rare: return Color.yellow;
                case rarity.legendary: return new Color(1, .84f, 0);
                    default: return Color.black;
            }
        }
    }
}
public enum rarity
{
    common, 
    uncommon,
    masterwork,
    rare,
    legendary
}