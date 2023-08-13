using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour
{
    SpriteRenderer SpriteRender = null;
    private void Start()
    {
        SpriteRender = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(IsMajor())
        {
            SpriteRender.sortingOrder = 5;
        }
        else
        {
            SpriteRender.sortingOrder = 2;

        }
    }
    public bool IsMajor()
    {
        if (transform.position.y <= GameManager.instance.Player.transform.position.y)
            return true;
        else
            return false;
    }

}

