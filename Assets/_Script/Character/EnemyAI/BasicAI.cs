using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Stat))]
[RequireComponent(typeof(Rigidbody2D))]
public class BasicAI : MonoBehaviour
{
    public GameObject target = null;

    public Vector2 patrolDirection = Vector2.right;
    public float patrolWallDetectionRange = 0.5f;

    public float detectRange = 5f;

    private Stat stat;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        stat = GetComponent<Stat>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stat.isDead) return;
        if (target == null)
            Patrol();
    }

    void Patrol()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, patrolDirection, patrolWallDetectionRange);
        //Debug.Log(hits.Length);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                //Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("Terrain"))
                {
                    patrolDirection = -patrolDirection;
                    //Debug.Log("AI Direction changed");
                    break;
                }
            }
        }

        rb.velocity = patrolDirection * stat.speed;

        sr.flipX = patrolDirection.x > 0;
    }

    void Chase()
    {
        Vector2 direction = target.transform.position - transform.position;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, detectRange);
        bool isTargetVisible = true;

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Terrain"))
            {
                isTargetVisible = false;
                break;
            }
        }

        if (isTargetVisible)
        {
            rb.velocity = direction.normalized * stat.speed;
        }
        else
        {
            target = null;
        }
    }
}
