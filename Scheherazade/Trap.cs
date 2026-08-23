using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent<PlayerInfo>(out var player);
            player.TakeDamage(0.5f);
        }
    }
}
