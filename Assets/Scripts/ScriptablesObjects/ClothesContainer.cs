using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Clothes Container", menuName = "Containers/Clothe Container", order = 1)]
public class ClothesContainer : ScriptableObject
{
    public List<ClothClass> Cloths;
}
public enum ClothTypeEnum {Head, Torso }


[System.Serializable]
public class ClothClass
{
    public string ClothName;
    public GameObject Cloth;
    public ClothTypeEnum ClothType;
    public ItemClass Item;

}

