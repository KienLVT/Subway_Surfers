using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoStick : MonoBehaviour
{
    [SerializeField] public float pogoJumpforce = 20f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if(playerRb != null)
            {
                playerRb.AddForce(Vector3.up * pogoJumpforce, ForceMode.Impulse);
                Debug.Log("Applied jump force to player.");
            }
            else
            {
                Debug.LogWarning("Player Rigidbody not found!");
            }
            Destroy(gameObject);
        }
    }
}
