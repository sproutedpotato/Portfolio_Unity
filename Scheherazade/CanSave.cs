using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSave : MonoBehaviour
{
    public bool isOnPlayer { get; set; }
    public bool isSaved { get; set; }

    private bool resetState;

    private PlayerController controller;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        isOnPlayer = false;
        isSaved = false;
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
        boxCollider = GetComponent<BoxCollider2D>();
        resetState = false;
    }

    private void Update()
    {
        if (isSaved)
        {
            if (!resetState)
            {
                controller.canSave = false;
                resetState = true;
            }
            boxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isOnPlayer = true;
            collision.GetComponent<PlayerController>().canSave = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().canSave = false;
            isOnPlayer = false;
        }
    }
}
