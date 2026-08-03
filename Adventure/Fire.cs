using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private FireController fireController;

    private bool isOn;
    private PlayerHealth playerHealth;

    private void OnEnable()
    {
        fireController.OnFireStateChanged += UpdateFireState;
    }

    private void OnDisable()
    {
        fireController.OnFireStateChanged -= UpdateFireState;
    }

    private void UpdateFireState(bool newState)
    {
        isOn = newState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.GetComponent<PlayerHealth>();
        }        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isOn && collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(2);
        }
    }
}
