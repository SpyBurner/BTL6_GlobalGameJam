using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    private Collider2D attackBox;
    private Stat stat;
    void Awake()
    {
        attackBox = GetComponent<Collider2D>();
        stat = transform.parent.GetComponent<Stat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null)
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            damagable.TakeDamage(stat.damage);
        }
    }
}
