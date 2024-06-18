using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSneakers : MonoBehaviour
{
    [SerializeField] public float duration = 10f;
    [SerializeField] public float jumpBoost = 2f;
   


    private void OnTriggerEnter(Collider other)
    {
        PlayerEffects playerEffects = other.GetComponent<PlayerEffects>();
        if (playerEffects != null)
        {
            playerEffects.ActivateSuperSneakers(duration, jumpBoost);
            gameObject.SetActive(false);
        }
    }
}
