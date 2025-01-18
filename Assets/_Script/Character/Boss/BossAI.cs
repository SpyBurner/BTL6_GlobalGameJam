using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum BossState
{
    Idle,
    Patrol,
    Chase,
}

public class BossAI : MonoBehaviour
{

    public Collider2D patrolBound;
    public Light2D[] lights;

    public BossState state = BossState.Idle;

    public GameObject target;

    public float targetMargin = 0.5f;

    public Vector2 patrolPoint = Vector2.zero;

    public float chaseTime = 30.0f;


    private Rigidbody2D rb;
    private Stat stat;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stat = GetComponent<Stat>();
        DeactivateLights();
    }

    void ActivateLights()
    {
        foreach (var light in lights)
        {
            light.enabled = true;
        }
    }

    void DeactivateLights()
    {
        foreach (var light in lights)
        {
            light.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stat.isDead) return;

        if (state == BossState.Idle)
        {
            if (!target)
            {
                patrolPoint = FindRandomPointOnBound();
                state = BossState.Patrol;
            }
            else
            {
                state = BossState.Chase;
            }
        }
        else if (state == BossState.Patrol)
        {
            Patrol();
        }
        else if (state == BossState.Chase)
        {
            Chase();
        }
    }



    Vector2 FindRandomPointOnBound()
    {
        Vector2 point = new Vector2(Random.Range(patrolBound.bounds.min.x, patrolBound.bounds.max.x),
            Random.Range(patrolBound.bounds.min.y, patrolBound.bounds.max.y));
        return point;
    }

    Vector2 FindChasePoint()
    {
        return ((Vector2)target.transform.position + target.GetComponent<Rigidbody2D>().velocity) + (Vector2)(target.transform.position - transform.position);
    }

    void Patrol()
    {
        rb.velocity = (patrolPoint - (Vector2)transform.position).normalized * stat.speed/2;

        if (Vector2.Distance(transform.position, patrolPoint) < targetMargin)
        {
            state = BossState.Idle;
        }
    }

    IEnumerator ClearChaseTarget()
    {
        yield return new WaitForSeconds(chaseTime);
        target = null;
    }

    public void StartChase(GameObject target)
    {
        this.target = target;
        if (target)
        {
            state = BossState.Chase;
            StartCoroutine(ClearChaseTarget());
            patrolPoint = FindChasePoint();
        }
    }

    void Chase()
    {
        rb.velocity = (patrolPoint - (Vector2)transform.position).normalized * stat.speed;

        if (Vector2.Distance(transform.position, patrolPoint) < targetMargin)
        {
            if (target)
                patrolPoint = FindChasePoint();
            else
                state = BossState.Idle;
        }
    }


}
