using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAtCursor : MonoBehaviour
{
    public Vector2 originalForward = Vector2.right;
    public Vector2 originalUp = Vector2.up;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;

        float angle = Vector2.SignedAngle(originalForward, direction);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        float flipAngle = Vector2.SignedAngle(originalUp, direction);
        spriteRenderer.flipY = flipAngle > 0;
    }
}
