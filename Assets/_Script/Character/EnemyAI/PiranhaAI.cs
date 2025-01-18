using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaAI : BasicAI
{
    // Update is called once per frame
    new void Update()
    {
        if (stat.isDead) return;
        if (target == null)
            Patrol();
        else if (target != null)
            Chase();
        sr.flipX = rb.velocity.x > 0;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRange);

        bool found = false;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                target = hit.gameObject;
                found = true;
                break;
            }
        }

        if (!found)
        {
            target = null;
            return;
        }
    }

    public float dashCooldown;
    public float dashMultiplier;
    public bool dashed = false;

    void Chase()
    {
        if (dashed) return;
        try
        {
            Vector2 direction = target.transform.position - transform.position;


            if (Vector2.Distance(transform.position, target.transform.position) > 0.5 * detectRange)
                rb.velocity = direction.normalized * stat.speed;
            else
            {
                rb.AddForce(direction.normalized * stat.speed * dashMultiplier);
                StartCoroutine(DashCooldown());
            }
        }
        catch (MissingReferenceException)
        {
            target = null;
        }

    }

    IEnumerator DashCooldown()
    {
        dashed = true;
        yield return new WaitForSeconds(dashCooldown);
        dashed = false;
    }
}
