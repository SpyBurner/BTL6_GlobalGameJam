using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Stat))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    public float moveMargin = 0.3f;

    private Stat stat;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        stat = GetComponent<Stat>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        lastClick = transform.position;
        lastDirection = Vector2.up;
    }


    private Vector2 lastClick;
    private Vector2 lastDirection;

    public void ResetMovement()
    {
        lastClick = transform.position;
        lastDirection = Vector2.up;
    }

    private void Update()
    {
        if (stat.isDead) return;
        if (Input.GetMouseButton(1))
        {
            lastClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(lastClick);
        }


        if (Vector2.Distance(transform.position, lastClick) > moveMargin)
        {
            Vector2 direction = lastClick - (Vector2)transform.position;
            lastDirection = direction.normalized;
            rb.velocity = direction * stat.speed * Time.fixedDeltaTime;

            rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, stat.speed * Time.fixedDeltaTime, stat.speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        transform.up = rb.velocity.magnitude < moveMargin * 5 ? Vector2.up : lastDirection;
        sr.flipX = lastDirection.x < 0;

    }
}
