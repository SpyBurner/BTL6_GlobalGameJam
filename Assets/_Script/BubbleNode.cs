using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stat))]
[RequireComponent(typeof(Rigidbody2D))]
public class BubbleNode : MonoBehaviour
{
    public BubbleNode previousNode = null;
    public BubbleNode nextNode = null;

    public float maxDistance = 1.0f;
    public bool isCaptured = false;

    private Rigidbody2D rb;
    private Stat stat;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stat = GetComponent<Stat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!previousNode) return;
        if (Vector3.Distance(transform.position, previousNode.transform.position) > maxDistance)
        {
            Vector3 direction = (previousNode.transform.position - transform.position).normalized;
            rb.velocity = direction * previousNode.rb.velocity.magnitude;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BubbleNode>() && !isCaptured)
        {
            BubbleNode bubbleNode = collision.gameObject.GetComponent<BubbleNode>();
            if (bubbleNode.isCaptured)
            {
                while (bubbleNode.nextNode != null)
                {
                    bubbleNode = bubbleNode.nextNode;
                }
                bubbleNode.nextNode = this;
                previousNode = bubbleNode;
                isCaptured = true;
            }
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            stat.TakeDamage(1);
            return;
        }
    }
}
