using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnPos;
    [SerializeField] private GameObject batPrefab;

    private bool canSpawn;

    private void Start()
    {
        canSpawn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player") && canSpawn)
        {
            canSpawn = false;
            Instantiate(batPrefab, spawnPos, Quaternion.identity);
            StartCoroutine(CanSpawnRoutine());
        }
    }

    private IEnumerator CanSpawnRoutine()
    {
        yield return new WaitForSeconds(5f);
        canSpawn = true;
    }
}
