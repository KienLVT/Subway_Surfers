using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] mapPrefabs;

    private void OnTriggerEnter(Collider other)
    {
        GameObject mapPrefab = mapPrefabs[Random.Range(0, mapPrefabs.Length)];
        Vector3 endPoint = other.transform.position;
        float mapPrefabSize = mapPrefab.GetComponentInChildren<Renderer>().bounds.size.z;

        Vector3 spawnPos = endPoint + new Vector3(0, 0, mapPrefabSize / 2 );
        spawnPos.x = 0;
        spawnPos.y = 0;
        
        Instantiate(mapPrefab, spawnPos, Quaternion.identity);

    }

   
}
