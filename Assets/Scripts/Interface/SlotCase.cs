using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotCase : MonoBehaviour
{
    public Sprite Default;
    public Image ItemIcon;

    [Header("HotBar")]
    public bool HotbarButton;
    public Button UnequipButton;
    public void VerifySlot(WeaponClass weapon = null, ClothClass myCloth = null)
    {
        if (weapon == null && myCloth == null)
        {
            ItemIcon.sprite = Default;
            return;
        }
        if (weapon != null)
        {
            ItemIcon.sprite = weapon.Item.ItemIcon;
        }
        else if (myCloth != null)
        {
            ItemIcon.sprite = myCloth.Item.ItemIcon;

        }
    }
}

