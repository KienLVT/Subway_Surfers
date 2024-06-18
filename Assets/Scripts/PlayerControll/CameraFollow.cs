using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not set for FollowPlayer script.");
            return;
        }
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        
        float targetX = target.position.x;
        float targetZ = target.position.z;

        
        Vector3 newPos = new Vector3(targetX + offset.x, transform.position.y, targetZ + offset.z);

        
        transform.position = Vector3.Lerp(transform.position, newPos, 10 * Time.deltaTime);
    }
}
