using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    public WeaponsContainer Container;
    public string SelectedWeapon;
    public bool CanShoot;
    public GameObject Bullet;

    [Space]
    public Transform Dir;
    public SpriteRenderer DirPoint;
    [Space]
    public GameObject SelectedObject;


    [Space]
    public List<string> MyWeapons;
    public SlotCase MySlot;
    public InventorySystem Inventory;
    private void Start()
    {
        VerifyMySlot();
    }
    private void Update()
    {
        SetDir();
        Shoot();
        SelectedObject = SelectObject();

        DirPoint.color = DirPointColor();
      
    }

    public void VerifyMySlot()
    {
       
         MySlot.UnequipButton.onClick.RemoveAllListeners();
         MySlot.GetComponent<Button>().onClick.RemoveAllListeners();
        
        if (SelectedWeapon != string.Empty)
        {
            MySlot.VerifySlot(Container.Weapons.Find(x => x.GunName == SelectedWeapon));
            MySlot.GetComponent<Button>().onClick.AddListener(() => { if (GetComponent<PlayerBasic>().Inventory.activeInHierarchy) MySlot.UnequipButton.gameObject.SetActive(true); });
            MySlot.UnequipButton.onClick.AddListener(() => { ClickButton(SelectedWeapon); });
        }
    }

    public void ClickButton(string Weapon)
    {
        SelectedWeapon = string.Empty;
        MyWeapons.Add(Weapon);
        MySlot.UnequipButton.gameObject.SetActive(false);
        MySlot.VerifySlot();
        Inventory.DestroyAll();
        Inventory.SpawnAll();
    }
    public void Shoot()
    {
        if(SelectedWeapon != string.Empty && !GetComponent<PlayerBasic>().Inventory.activeInHierarchy && !GetComponent<PlayerBasic>().Shop.activeInHierarchy)
        {
            WeaponClass weapon = Container.Weapons.Find(x => x.GunName == SelectedWeapon);
            if(Input.GetKey(KeyCode.Mouse0) && CanShoot)
            {
                Bullet ball = Instantiate(Bullet, DirPoint.transform.position, Dir.transform.rotation).GetComponent<Bullet>();
                ball.GetComponent<SpriteRenderer>().color = weapon.Color;
                ball.Scale = weapon.Scale;
                ball.Force = weapon.ShootForce;
                ball.Damage = weapon.Damage;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                Vector2 direction = (mousePosition - Dir.position).normalized;
                GetComponent<Rigidbody2D>().AddForce(-direction * weapon.ShootForce/10);
                transform.DOShakeScale(0.2f,weapon.ShootForce/1000 + 0.2f, 10, 90, true, ShakeRandomnessMode.Harmonic);
                StartCoroutine(RangeBack(weapon.Range));
            }
        }
    }

    public IEnumerator RangeBack(float Range)
    {
        CanShoot = false;
        yield return new WaitForSeconds(Range);
        CanShoot = true;
        transform.DOScale(new Vector3(1, 1, 1), 0.5f);

    }
    public void SetDir()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector2 direction = (mousePosition - Dir.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Dir.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public GameObject SelectObject()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    public Color DirPointColor()
    {
        if (SelectedObject != null)
        {
            if (SelectedObject.GetComponent<ObjectConfig>() != null)
            {
                return Color.red;
            }
        }
        return Color.white;
    }
}
