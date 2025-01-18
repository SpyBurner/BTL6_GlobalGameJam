using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossNode : MonoBehaviour
{
    public bool isHead = false;
    public BossNode previousNode = null;
    public BossNode nextNode = null;

    public float maxDistance = 1.0f;

    private Rigidbody2D rb;
    private Stat stat;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stat = GetComponent<Stat>();
        stat.DeathEvent.AddListener(OnDestroy);
    }

    private void OnDestroy()
    {
        if (nextNode)
        {
            nextNode.previousNode = null;
        }
        if (previousNode)
        {
            previousNode.nextNode = null;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!previousNode)
        {
            if (!isHead)
            {
                if (nextNode)
                {
                    nextNode.previousNode = null;
                    nextNode = null;
                }
            }

            return;
        }
        if (Vector3.Distance(transform.position, previousNode.transform.position) > maxDistance)
        {
            Vector3 direction = (previousNode.transform.position - transform.position).normalized;
            rb.velocity = direction * previousNode.GetComponent<Rigidbody2D>().velocity.magnitude;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
