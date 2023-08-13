using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ShopSystem : MonoBehaviour
{
    public GameObject BuyTemplate;
    public GameObject SellTemplate;
    [Header("Shop Mode")]
    public bool Weapons;
    public bool Clothes;
    [Space]
    public PlayerBasic Player;
    public WeaponsContainer WeaponsContainer;
    public ClothesContainer ClothesContainer;
    List<GameObject> allObj = new List<GameObject>();
    private void OnEnable()
    {
        SetMode(true);
    }
    public void SetMode(bool First)
    {
        Weapons = First;
        Clothes = !First;
        if(First)
        {
            SpawnWeaponTab();
        }
        else
        {
            SpawnClothesTab();
        }
    }
    public void DestroyAll()
    {
        for(int i =0; i < allObj.Count;i++)
        {
            Destroy(allObj[i]);
        }
        allObj.Clear();
    }
    public void SpawnWeaponTab()
    {
        DestroyAll();
        //Buy Items
        for(int i = 0; i < WeaponsContainer.Weapons.Count;i++)
        {
            WeaponClass weapon = WeaponsContainer.Weapons[i];
            CreateWeaponItem(BuyTemplate, weapon,() => BuyWeapon(weapon.Item.Price, weapon.GunName));
        }
        //Sell Items
        PlayerShooter shooter = Player.GetComponent<PlayerShooter>();

        for (int i =0; i < shooter.MyWeapons.Count;i++)
        {
            WeaponClass weapon = WeaponsContainer.Weapons.Find(x => x.GunName == shooter.MyWeapons[i]);
            CreateWeaponItem(SellTemplate, weapon, () => SellWeapon(weapon.Item.Price, weapon.GunName));

        }
    }
    public void CreateWeaponItem(GameObject Template, WeaponClass weapon, UnityAction action = null)
    {
        ShopSlot slot = Instantiate(Template, Template.transform.parent.transform).GetComponent<ShopSlot>();
        slot.gameObject.SetActive(true);
        slot.ItemName.text = weapon.GunName;
        slot.ItemPrice.text = weapon.Item.Price.ToString();
        slot.Icon.sprite = weapon.Item.ItemIcon;
        slot.ActionButton.onClick.AddListener(action);
        allObj.Add(slot.gameObject);
    }
    public void SellWeapon(int price, string weaponName)
    {
        PlayerShooter shooter = Player.GetComponent<PlayerShooter>();
        shooter.MyWeapons.Remove(weaponName);
        Player.Economy += price;
        SpawnWeaponTab();
    }
    public void BuyWeapon(int price, string weaponName)
    {
        PlayerShooter shooter = Player.GetComponent<PlayerShooter>();
        if(Player.Economy >= price && shooter.MyWeapons.Count < 32 )
        {
            Player.Economy -= price;
            shooter.MyWeapons.Add(weaponName);
            SpawnWeaponTab();
        }
    }

    //Clothes
    public void SpawnClothesTab()
    {
        DestroyAll();
        //Buy Items
        for (int i = 0; i < ClothesContainer.Cloths.Count; i++)
        {
            ClothClass cloth = ClothesContainer.Cloths[i];
            CreateClothItem(BuyTemplate, cloth, () => BuyCloth(cloth.Item.Price, cloth.ClothName));
        }
        //Sell Items
        PlayerUtilities utilities = Player.GetComponent<PlayerUtilities>();

        for (int i = 0; i < utilities.MyClothes.Count; i++)
        {
            ClothClass cloth = ClothesContainer.Cloths.Find(x => x.ClothName == utilities.MyClothes[i]);
            CreateClothItem(SellTemplate, cloth, () => SellCloth(cloth.Item.Price, cloth.ClothName));

        }
    }
    public void CreateClothItem(GameObject Template, ClothClass cloth, UnityAction action = null)
    {
        ShopSlot slot = Instantiate(Template, Template.transform.parent.transform).GetComponent<ShopSlot>();
        slot.gameObject.SetActive(true);
        slot.ItemName.text = cloth.ClothName;
        slot.ItemPrice.text = cloth.Item.Price.ToString();
        slot.Icon.sprite = cloth.Item.ItemIcon;
        slot.ActionButton.onClick.AddListener(action);
        allObj.Add(slot.gameObject);
    }
    public void SellCloth(int price, string clothName)
    {
        PlayerUtilities utilities = Player.GetComponent<PlayerUtilities>();
        utilities.MyClothes.Remove(clothName);
        Player.Economy += price;
        SpawnClothesTab();
    }
    public void BuyCloth(int price, string clothName)
    {
        PlayerUtilities utilities = Player.GetComponent<PlayerUtilities>();
        if (Player.Economy >= price && utilities.MyClothes.Count < 32)
        {
            Player.Economy -= price;
            utilities.MyClothes.Add(clothName);
            SpawnClothesTab();
        }
    }
}
