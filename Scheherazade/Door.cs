using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private PlayerController controller;

    private float fadeDuration = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(HandleDoorTransition(other));
        }
    }

    IEnumerator HandleDoorTransition(Collider2D other)
    {
        controller.SetCanMove(false);

        yield return StartCoroutine(Fade(1f));

        yield return new WaitForSeconds(0.5f);

        float x = other.transform.position.x;
        float y = other.transform.position.y;

        if (y < 35.7f)
        {
            other.transform.position = new Vector2(-7f, 37.34f);
        }
        else if (y >= 35.7f && y < 66f)
        {
            if (x > 0f)
            {
                other.transform.position = new Vector2(-7f, 67.76f);
            }
            else
            {
                other.transform.position = new Vector2(20f, 11.08f);
            }
        }
        else
        {
            other.transform.position = new Vector2(20f, 37.34f);
        }

        yield return StartCoroutine(Fade(0f));

        controller.SetCanMove(true);
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }
}
