using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon")]
public class WeaponItem : Item
{
    public DamageStuct[] damageType; 
}
[System.Serializable]
public struct DamageStuct
{
    public int damage;
    public DamageType typeOfDamage;
}