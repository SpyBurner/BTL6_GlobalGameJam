using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stat))]
[RequireComponent(typeof(Rigidbody2D))]
public class BubbleNode : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float launchForce;

    public bool isHead = false;
    public BubbleNode previousNode = null;
    public BubbleNode nextNode = null;

    public float maxDistance = 1.0f;
    public bool isCaptured = false;

    private Rigidbody2D rb;
    private Stat stat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stat = GetComponent<Stat>();
        stat.DeathEvent.AddListener(OnDestroy);

        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!previousNode)
        {
            if (!isHead)
            {
                isCaptured = false;
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
            rb.velocity = direction * previousNode.rb.velocity.magnitude;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BubbleNode>() && !isCaptured && transform.childCount > 1)
        {
            BubbleNode bubbleNode = collision.gameObject.GetComponent<BubbleNode>();
            if (bubbleNode.isHead)
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
            if (!collision.gameObject.GetComponent<Stat>().isDead || transform.childCount > 1) return;
            
            collision.gameObject.transform.parent = transform;
            collision.gameObject.transform.localPosition = Vector3.zero;

            collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }
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

        if (transform.childCount > 1)
        {
            transform.GetChild(1).GetComponent<Rigidbody2D>().simulated = true;
            transform.GetChild(1).parent = null;

            Destroy(transform.GetChild(0).gameObject);
        }

        Destroy(gameObject);
    }
}
