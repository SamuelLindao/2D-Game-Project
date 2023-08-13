using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapons Container", menuName = "Containers/Weapons Container", order = 1)]

public class WeaponsContainer : ScriptableObject
{
    public List<WeaponClass> Weapons;
}

[System.Serializable]
public class WeaponClass
{
    public string GunName;
    public float Range;
    public int Damage;
    public int ShootForce;
    [Range(0.1f, 0.35f)]
    public float Scale;
    public Color Color;

    public ItemClass Item;


}

