using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothObject : MonoBehaviour
{
    public Sprite DownView;
    public Sprite UpView;
    public Sprite SideView;

    SpriteRenderer Render;
    private void Start()
    {
        Render = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        KeyPressed();
    }
    void KeyPressed()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            SetSprite(UpView);
        }
       
        if (Input.GetKey(KeyCode.W))
        {
            SetSprite(SideView);

        }
        if (Input.GetKey(KeyCode.S))
        {
            SetSprite(DownView);

        }
    }
    private void SetSprite(Sprite sprite)
    {
        Render.sprite = sprite;
    }
}
