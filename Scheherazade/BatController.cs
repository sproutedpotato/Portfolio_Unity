using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private float[] speeds = { 9f, 11f, 13f };
    private Vector3[] vectors = { new Vector3(-1, -3, 0).normalized,
                                    new Vector3(-1, -5, 0).normalized,
                                    new Vector3(-1, -7, 0).normalized,
                                    new Vector3(-0.5f, -2, 0).normalized };
    private bool canDestroy, isStartRoutine, isSetRand;
    private int rand, speed;

    private void Start()
    {
        canDestroy = false;
        isStartRoutine = false;
        isSetRand = false;
    }

    void Update()
    {
        if (!isStartRoutine)
        {
            SetDestroyBool();
        }

        if (canDestroy)
        {
            Destroy(gameObject);
            return;
        }

        if (!isSetRand)
        {
            rand = Random.Range(0, vectors.Length);
            Debug.Log(rand);
            speed = Random.Range(0, speeds.Length);
            isSetRand = true;
        }
        
        transform.Translate(vectors[rand] * speeds[speed] * Time.deltaTime);
    }

    private IEnumerator SetDestroyBoolRoutine()
    {
        isStartRoutine = true;
        yield return new WaitForSeconds(5f);
        canDestroy = true;
    }

    private void SetDestroyBool()
    {
        StartCoroutine(SetDestroyBoolRoutine());
    }
}
