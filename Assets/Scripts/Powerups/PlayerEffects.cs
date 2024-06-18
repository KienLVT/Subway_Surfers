using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void ActivateSuperSneakers(float duration, float jumpBoost)
    {
        StartCoroutine(SuperSneakersRoutine(duration, jumpBoost));
    }

    private IEnumerator SuperSneakersRoutine(float duration, float jumpBoost)
    {
        float originalJump = player.jumpForce;
        player.jumpForce = jumpBoost;

        yield return new WaitForSeconds(duration);

        player.jumpForce = originalJump;
    }

   
}
