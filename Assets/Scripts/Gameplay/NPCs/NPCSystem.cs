using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events ;

public class NPCSystem : MonoBehaviour
{
    public bool PlayerAround;
    public float MinDistance = 5;
    public KeyCode MyKey;
    [Space]
    public UnityEvent MeetPlayerAction;
    public UnityEvent GoodbyePlayerAction;
    public UnityEvent KeyAction;

    private void Update()
    {
        SetPlayerAround();
        if(Input.GetKeyDown(MyKey) && PlayerAround)
        {
            KeyAction.Invoke();
        }
    }

    public void SetPlayerAround()
    {
        float Dis = Vector3.Distance(transform.position, GameManager.instance.Player.transform.position);
        if (!PlayerAround && Dis <= MinDistance)
        {
            PlayerAround = true;
            MeetPlayerAction.Invoke();
        }

        if (PlayerAround && Dis >= MinDistance)
        {
            PlayerAround = false;
            GoodbyePlayerAction.Invoke();
        }
    }

    public void Deactive(GameObject obj)
    {
        StartCoroutine(ObjTime(obj));
    }
    public IEnumerator ObjTime(GameObject obj)
    {
        yield return new WaitForSeconds(3.0f);
        obj.SetActive(false);
    }
}
