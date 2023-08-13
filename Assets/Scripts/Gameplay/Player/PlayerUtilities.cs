using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUtilities : MonoBehaviour
{
    public ClothesContainer container;

    [Space]
    public string HeadObject;
    public string TorsoObject;

    [Space]
    public Transform HeadPoint;
    public Transform TorsoPoint;

    [Space]
    public SlotCase SlotHead;
    public SlotCase SlotTorso;

    public List<string> MyClothes;
    public InventorySystem Inventory;
    private void Start()
    {
        VerifyCloths();
    }
    public void VerifyMySlots()
    {
        SlotHead.GetComponent<Button>().onClick.RemoveAllListeners();
        SlotHead.UnequipButton.onClick.RemoveAllListeners();
        SlotTorso.GetComponent<Button>().onClick.RemoveAllListeners();
        SlotTorso.UnequipButton.onClick.RemoveAllListeners();

        SlotTorso.UnequipButton.onClick.RemoveAllListeners();

        if (HeadObject != string.Empty)
        {
            SlotHead.VerifySlot(null, container.Cloths.Find(x => x.ClothName == HeadObject));
            SlotHead.GetComponent<Button>().onClick.AddListener(() => { if (GetComponent<PlayerBasic>().Inventory.activeInHierarchy) SlotHead.UnequipButton.gameObject.SetActive(true); });
            SlotHead.UnequipButton.onClick.AddListener(() => { UnequipHead(); });
        }
        if (TorsoObject != string.Empty)
        {

            SlotTorso.VerifySlot(null, container.Cloths.Find(x => x.ClothName == TorsoObject));
            SlotTorso.GetComponent<Button>().onClick.AddListener(() => { if (GetComponent<PlayerBasic>().Inventory.activeInHierarchy) { SlotTorso.UnequipButton.gameObject.SetActive(true); } });
            SlotTorso.UnequipButton.onClick.AddListener(() => { UnequipTorso(); });
        }
    }
    public void UnequipHead()
    {
        MyClothes.Add(HeadObject);
        HeadObject = string.Empty;
        SlotHead.UnequipButton.gameObject.SetActive(false);
        SlotHead.VerifySlot();
        VerifyCloths();
        Inventory.DestroyAll();
        Inventory.SpawnAll();
    } 
    
    public void UnequipTorso()
    {
        MyClothes.Add(TorsoObject);
        TorsoObject = string.Empty;
        SlotTorso.UnequipButton.gameObject.SetActive(false);
        SlotTorso.VerifySlot();
        VerifyCloths();
        Inventory.DestroyAll();
        Inventory.SpawnAll();
    }
    public void VerifyCloths()
    {
        if(HeadObject != string.Empty)
        {
            ClothClass cloth = container.Cloths.Find(x => x.ClothName == HeadObject);
            if(HasItem(HeadPoint))
            {
                Destroy(HeadPoint.GetChild(0).gameObject);
            }
            GameObject obj = Instantiate(cloth.Cloth, HeadPoint);
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            if(HasItem(HeadPoint))
            {
                Destroy(HeadPoint.GetChild(0).gameObject);

            }
        }

        if(TorsoObject != string.Empty)
        {
            ClothClass cloth = container.Cloths.Find(x => x.ClothName == TorsoObject);
            if(HasItem(TorsoPoint))
            {
                Destroy(TorsoPoint.GetChild(0).gameObject);
            }
            GameObject obj = Instantiate(cloth.Cloth, TorsoPoint);
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localScale = new Vector3(1, 1, 1);

        }
        else
        {
            if (HasItem(TorsoPoint))
            {
                Destroy(TorsoPoint.GetChild(0).gameObject);
            }
        }
        VerifyMySlots();
    }
    public bool HasItem(Transform point)
    {
        if(point.childCount > 0)
        {
            return true;
        }
        return false;
    }
}
