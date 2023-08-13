using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class ObjectConfig : MonoBehaviour
{
    public string Name;
    public int HealthMax;
    public int Health;

    GameObject DamageObj;
    private void Start()
    {
        DamageObj = Resources.Load<GameObject>("Objects/DamageObj");
        if(Name == string.Empty)
        {
            Name = gameObject.name;
        }
    }

    public void GetDamage(int Damage)
    {
        Health = Mathf.Clamp(Health - Damage, 0, Health);
        transform.DOShakePosition(1, 0.25f, 10, 90);
        SpawnText(Damage.ToString());
        if(Health == 0)
        {
            SpawnText("Kill");
            UserArea.instance.value++;
            UserArea.instance.NewValue();
            Destroy(gameObject);
        }
        
    }
    public void SpawnText(string TextValue)
    {
        GameObject obj = Instantiate(DamageObj, transform.position, Quaternion.Euler(0, 0, 0));
        GameObject text = obj.transform.GetChild(0).gameObject;
        text.GetComponent<TextMeshProUGUI>().text = TextValue;
        text.transform.DOLocalMove(new Vector3(Random.Range(-2, 2), 2f, 0), 3);
        text.transform.DOShakeRotation(2, 20, 10, 90);
        Destroy(obj, 3.0f);
    }
   
}
