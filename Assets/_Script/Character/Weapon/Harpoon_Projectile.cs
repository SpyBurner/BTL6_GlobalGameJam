using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class Harpoon_Projectile : MonoBehaviour
{
    public Vector2 direction;
    public float launchForce;
    private Rigidbody2D rb;

    public bool isStuck = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.right = direction;
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Terrain") && !isStuck)
        {
            isStuck = true;
            Disable();
        }

        if (collision.CompareTag("Enemy") && ! isStuck)
        {
            gameObject.transform.parent = collision.transform;
            isStuck = true;
            Disable();
        }

        if (collision.CompareTag("Player"))
        {
            if (!isStuck) return;
            collision.GetComponent<PlayerAmmo>().currentHarpoon += 1;
            Destroy(gameObject);
        }
    }

    private void Disable()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        Destroy(GetComponent<ContactDamage>());
    }

}
