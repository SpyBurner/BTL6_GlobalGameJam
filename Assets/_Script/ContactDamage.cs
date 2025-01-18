using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public int targetLayer;
    public int damage = 1;

    public bool destroyOnContact = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null && collision.gameObject.layer == targetLayer)
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            damagable.TakeDamage(damage);

            //Debug.Log("Object " + collision.gameObject.name + " takes " + damage + " damage from" + transform.name);
            if (destroyOnContact)
            {
                Destroy(this);
            }
        }
    }
}
