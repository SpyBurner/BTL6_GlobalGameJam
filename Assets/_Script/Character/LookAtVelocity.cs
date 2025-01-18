using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LookAtVelocity : MonoBehaviour
{
    public Vector2 originalForward = Vector2.right;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 lastVelocity = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            lastVelocity = rb.velocity.normalized;
        }

        float signedAngle = Vector2.SignedAngle(originalForward, lastVelocity);

        transform.rotation = Quaternion.Euler(0, 0, signedAngle);
        
        sr.flipY = Vector2.Dot(originalForward, lastVelocity) < 0;
    }
}
