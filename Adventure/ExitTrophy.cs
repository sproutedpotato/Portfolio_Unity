using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ExitTrophy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject stageClearPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            animator.SetTrigger("Goal");

            int current = PlayerPrefs.GetInt("SelectedStage", 1);
            int reached = PlayerPrefs.GetInt("Stage", 1);

            if (current >= reached)
            {
                PlayerPrefs.SetInt("Stage", current + 1);
                Debug.Log("current stage is " + current + 1);
                PlayerPrefs.Save();
            }

            StartCoroutine(ShowPanelAfterDelay());
        }
    }

    public void ShowStageClearPanel()
    {
        stageClearPanel.SetActive(true);
    }

    private IEnumerator ShowPanelAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        stageClearPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
