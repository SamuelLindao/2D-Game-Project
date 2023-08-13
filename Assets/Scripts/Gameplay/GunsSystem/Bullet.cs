using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public int Force;
    [Space]
    public float Scale;
    [Space]
    public GameObject Particle;
    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = Scale + 0.09f;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(1,0) * Force);
        transform.localScale = new Vector3(Scale, Scale, Scale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectConfig obj = collision.GetComponent<ObjectConfig>();
        if (obj != null)
        {
            obj.GetDamage(Damage);
        }
        GameObject particle = Instantiate(Particle, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(particle, 3f);
        Destroy(gameObject);
    }
}
