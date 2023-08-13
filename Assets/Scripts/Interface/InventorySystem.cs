using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public WeaponsContainer Weapons;
    public ClothesContainer Clothes;
    [Header("SelectedItem")]
    public string SelectedItem;
    public enum ItemTypeEnum {Weapon, Cloth };
    public ItemTypeEnum ItemType;
    [Space]
    
    public SlotCase SlotCase;
    public List<SlotCase> AllSlot;

    [Header("Inventory Mode")]
    public bool WeaponsMode = true;
    public bool ClothesMode = false;

    [Header("UI")]
    public GameObject SelectedItemArea;
    public Image ItemIcon;
    public TextMeshProUGUI ItemTypeText;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemPriceText;
    public Button EquipButton;
    public Button TrashButton;
    private void Start()
    {
    }

    private void OnEnable()
    {
        SpawnAll();
    }

    public void SpawnAll()
    {
        PlayerShooter weapons = GameManager.instance.Player.GetComponent<PlayerShooter>();
        PlayerUtilities clothes = GameManager.instance.Player.GetComponent<PlayerUtilities>();
        SelectedItemArea.SetActive(false);

        for(int i =0; i < 32; i ++)
        {
            SlotCase slot = Instantiate(SlotCase, SlotCase.transform.parent.transform);
            slot.gameObject.SetActive(true);
            AllSlot.Add(slot);
        }
        VerifyMode();
    }
    public void DestroyAll()
    {
        for (int i = 0; i < AllSlot.Count; i++)
        {
            Destroy(AllSlot[i].gameObject);
        }
        AllSlot.Clear();
    }
    public void SetMode(bool firstOption)
    {
        WeaponsMode = firstOption;
        ClothesMode = !firstOption;
        VerifyMode();
    }
    public void VerifyMode()
    {
        SelectedItemArea.SetActive(false);

        if (WeaponsMode == true)
        {
            PlayerShooter player = GameManager.instance.Player.GetComponent<PlayerShooter>();
            for(int i =0; i < AllSlot.Count;i++)
            {
                AllSlot[i].VerifySlot(null, null);
            }
            for (int i =0; i < player.MyWeapons.Count; i++)
            {
                string Name = player.MyWeapons[i];

                AllSlot[i].VerifySlot(Weapons.Weapons.Find(x => x.GunName == Name), null);
                AllSlot[i].GetComponent<Button>().onClick.RemoveAllListeners();
                AllSlot[i].GetComponent<Button>().onClick.AddListener(() => { SelectWeapon(Name); });
            }
        }
        else 
        {
            PlayerUtilities player = GameManager.instance.Player.GetComponent<PlayerUtilities>();
            for (int i = 0; i < AllSlot.Count; i++)
            {
                AllSlot[i].VerifySlot(null, null);
            }
            for (int i = 0; i < player.MyClothes.Count; i++)
            {
                string Name = player.MyClothes[i];

                AllSlot[i].VerifySlot(null,Clothes.Cloths.Find(x => x.ClothName == Name));
                AllSlot[i].GetComponent<Button>().onClick.RemoveAllListeners();
                AllSlot[i].GetComponent<Button>().onClick.AddListener(() => { SelectCloth(Name); });

            }
        }
    }
    public void SelectCloth(string ClothName)
    {
        ClothClass cloth = Clothes.Cloths.Find(x => x.ClothName == ClothName);
        SelectedItem = ClothName;
        ItemType = ItemTypeEnum.Cloth;
        SelectedItemArea.SetActive(true);
        ItemIcon.sprite = cloth.Item.ItemIcon;
        ItemTypeText.text = ItemType.ToString();
        ItemNameText.text = cloth.ClothName;
        ItemPriceText.text = "Price : " + cloth.Item.Price;
        EquipButton.onClick.RemoveAllListeners();
        EquipButton.onClick.AddListener(() => { EquipCloth(cloth.ClothType); });
    }

    public void SelectWeapon(string WeaponName)
    {
        WeaponClass weapon = Weapons.Weapons.Find(x => x.GunName == WeaponName);
        SelectedItem = WeaponName;
        ItemType = ItemTypeEnum.Weapon;
        SelectedItemArea.SetActive(true);
        ItemIcon.sprite = weapon.Item.ItemIcon;
        ItemTypeText.text = ItemType.ToString();
        ItemNameText.text = weapon.GunName;
        ItemPriceText.text = "Price : " + weapon.Item.Price;
        EquipButton.onClick.RemoveAllListeners();
        EquipButton.onClick.AddListener(() => { EquipWeapon(); });
    }
    public void EquipWeapon()
    {
        PlayerShooter player = GameManager.instance.Player.GetComponent<PlayerShooter>();

        if (player.SelectedWeapon != string.Empty)
        {
            player.MyWeapons.Add(player.SelectedWeapon);
        }
        player.MyWeapons.Remove(SelectedItem);
        player.SelectedWeapon = SelectedItem;
        player.VerifyMySlot();
        DestroyAll();
        SpawnAll();
    }

    public void EquipCloth(ClothTypeEnum type)
    {
        PlayerUtilities player = GameManager.instance.Player.GetComponent<PlayerUtilities>();

        switch (type)
        {
            case (ClothTypeEnum.Head):
                if(player.HeadObject != string.Empty)
                {
                    player.MyClothes.Add(player.HeadObject);
                }
                player.HeadObject = SelectedItem;
                player.MyClothes.Remove(SelectedItem);
                break;
            case (ClothTypeEnum.Torso):
                if (player.TorsoObject != string.Empty)
                {
                    player.MyClothes.Add(player.TorsoObject);
                }
                player.TorsoObject = SelectedItem;
                player.MyClothes.Remove(SelectedItem);
                break;
        
        }
        player.VerifyMySlots();
        player.VerifyCloths();
        DestroyAll();
        SpawnAll();
    }
    private void OnDisable()
    {
        DestroyAll();
    }

}
[System.Serializable]
public class ItemClass
{
    public Sprite ItemIcon;
    public int ItemId;
    public int Price;
}

