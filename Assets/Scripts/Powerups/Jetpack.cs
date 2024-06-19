using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public float jetpackDuration = 5f;      // Duration of the jetpack powerup
    public float jetpackForce = 10f;        // Upward force applied by the jetpack
    public ParticleSystem jetpackEffect;    // Visual effect for the jetpack
    public float forwardSpeed = 5f;         // Speed at which the player moves forward while flying

    private bool isFlying = false;
    private float timer = 0f;
    private GameObject player;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerRigidbody = player.GetComponent<Rigidbody>();
            playerAnimator = player.GetComponent<Animator>();
            ActivateJetpack();
            Destroy(gameObject); // Remove the powerup object
        }
    }

    void Update()
    {
        if (isFlying)
        {
            timer += Time.deltaTime;
            if (timer >= jetpackDuration)
            {
                DeactivateJetpack();
            }
        }
    }

    void ActivateJetpack()
    {
        isFlying = true;
        timer = 0f;

        if (jetpackEffect != null)
        {
            Instantiate(jetpackEffect, player.transform.position, Quaternion.identity, player.transform);
        }

        // Enable flying animation
        playerAnimator.SetBool("IsFlying", true);

        // Reset vertical velocity and apply initial upward force
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);
        playerRigidbody.AddForce(Vector3.up * jetpackForce, ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        if (isFlying)
        {
            // Continuously apply upward force to simulate flight
            playerRigidbody.AddForce(Vector3.up * jetpackForce * Time.fixedDeltaTime, ForceMode.Acceleration);

            // Move the player forward along the Z-axis
            playerRigidbody.MovePosition(player.transform.position + Vector3.forward * forwardSpeed * Time.fixedDeltaTime);
        }
    }

    void DeactivateJetpack()
    {
        isFlying = false;
        timer = 0f;

        // Disable flying animation
        playerAnimator.SetBool("IsFlying", false);

        // Optionally, stop the jetpack effect here if needed
        // If you have a particle effect, you might want to stop it here
    }
}
