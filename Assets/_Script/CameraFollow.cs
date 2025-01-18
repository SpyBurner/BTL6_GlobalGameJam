using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float smoothSpeed = 0.125f;
    public float margin = 0.3f;
    public Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        if (Vector3.Distance(transform.position, target.transform.position) > margin)
        {
            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            smoothedPosition.z = transform.position.z;
            transform.position = smoothedPosition;
        }
    }
}
