using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossNode))]
public class BossChain : MonoBehaviour
{
    public GameObject segmentPrefab;
    public int segmentCount = 10;

    public float segmentSpacing = 1.0f;
    public Vector2 forward = (Vector2.right + Vector2.down).normalized;


    // Start is called before the first frame update
    void Start()
    {
        BossNode node = GetComponent<BossNode>();
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segment = Instantiate(segmentPrefab, transform.position - (Vector3)forward * segmentSpacing * i, Quaternion.identity);
            segment.transform.parent = transform.parent;
            BossNode segmentNode = segment.GetComponent<BossNode>();
            segmentNode.previousNode = node;
            node.nextNode = segmentNode;

            node = segmentNode;

            segmentNode.maxDistance = segmentSpacing;
        }
        Destroy(this);
    }
}
