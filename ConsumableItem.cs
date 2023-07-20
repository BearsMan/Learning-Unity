using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Item")]
public class ConsumableItem : Item
{
    public Effect[] itemEffect; 
}
public struct Effect
{
    public EffectType type;
    public float value;
}
public enum EffectType
{
    heal,
    posion,
    strength
}