using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    //[SerializeField] private PlayerController controller;

    private float fadeDuration = 0.5f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void ChangeScene(string name)
    {
        StartCoroutine(FadeOutAndLoadScene(name));
    }

    IEnumerator FadeOutAndLoadScene(string name)
    {
        yield return StartCoroutine(Fade(1f)); // 검게
        SceneManager.sceneLoaded += OnSceneLoaded; // 로드 완료 이벤트 등록
        SceneManager.LoadScene(name); // 씬 전환
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
        StartCoroutine(Fade(0f)); // 밝아지기 (연출 다 로드된 뒤)
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsed = 0f;

        gameManager.canMove = false;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        if(targetAlpha == 0f)
        {
            gameManager.canMove = true;
        }
    }
}
