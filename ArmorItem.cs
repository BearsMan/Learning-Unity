using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Armor")]
public class ArmorItem : Item
{
    public DamageType[] immune, resistant, weakness, blindness;
}