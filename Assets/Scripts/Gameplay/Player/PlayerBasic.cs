using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBasic : MonoBehaviour
{
    public int Economy;
    public TextMeshProUGUI EconomyText;
    public GameObject Inventory;
    public GameObject Shop;

  
    private void Update()
    {
        EconomyText.text = Economy.ToString();
        if(Input.GetKeyDown(KeyCode.Y))
        {
            Inventory.SetActive(!Inventory.activeInHierarchy);
        }
    }
}
