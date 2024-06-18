using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    [SerializeField] private float jetpackDuration = 5f;
    [SerializeField] private float upForce = 5f;
    private bool jetActive = false;
    [SerializeField] private float flyTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateJetpack(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        flyTime = Time.time;
        if (flyTime >= jetpackDuration)
        {
            DeactivatejetPack();
        }
    }

    private void ActivateJetpack(GameObject player)
    {
        jetActive = true;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        }
    }
    
    private void DeactivatejetPack()
    {
        jetActive = false;
        flyTime = 0f;
    }
}
