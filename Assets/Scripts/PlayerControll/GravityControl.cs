using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{

    [SerializeField] public float gravityScale= 14;
    public const float gravity = -9.81f;

    public void Start()
    {
        UpdateGravity();
    }

    [ContextMenu("Update Gravity")]
    public void UpdateGravity()
    {
        Physics.gravity = new Vector3(0, gravity * gravityScale, 0);
    }
}
