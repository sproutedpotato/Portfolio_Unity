using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlider : MonoBehaviour
{
    [SerializeField] private RectTransform stagePanel;
    [SerializeField] private float distance = 800f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private GameObject rightArrow, leftArrow;

    private bool isMoving = false;
    int count = 1;
    int maxPage = 3;

    private void OnEnable()
    {
        isMoving = false;

        if (GameManager.Instance != null)
        {
            int savedCount = GameManager.Instance.ReturnCount();
            count = (savedCount > 0) ? savedCount : 1;
        }
        else
        {
            count = 1;
        }

        if (stagePanel != null)
        {
            Time.timeScale = 1f;

            float targetX = -(count - 1) * distance;
            stagePanel.anchoredPosition = new Vector2(targetX, 0);
        }
    
    CheckArrowActive();
}

    private void Start()
    {
        CheckArrowActive();
    }

    public void OnClickRightArrowButton()
    {
        if (isMoving || count >= maxPage) { Debug.Log(count + " is count");  return; }

        count++;
        GameManager.Instance.GetCount(count);
        Vector2 targetPos = stagePanel.anchoredPosition + new Vector2(-distance, 0);

        StartCoroutine(MoveCoroutine(targetPos));
    }

    public void OnClickLeftArrowButton()
    {
        if (isMoving || count <= 1) return;

        count--;
        GameManager.Instance.GetCount(count);
        Vector2 targetPos = stagePanel.anchoredPosition + new Vector2(distance, 0);

        StartCoroutine(MoveCoroutine(targetPos));
    }

    private IEnumerator MoveCoroutine(Vector2 target)
    {
        isMoving = true;
        Vector2 startPos = stagePanel.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (Time.timeScale == 0)
            {
                Debug.LogWarning("경고: 현재 Time.timeScale이 0입니다! 코루틴이 진행되지 않습니다.");
            }

            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);

            t = t * t * (3f - 2f * t);

            stagePanel.anchoredPosition = Vector2.Lerp(startPos, target, t);
            yield return null;
        }

        stagePanel.anchoredPosition = target;
        isMoving = false;

        CheckArrowActive();
    }

    private void CheckArrowActive()
    {
        leftArrow.SetActive(count > 1);
        rightArrow.SetActive(count < maxPage);
    }
}
