using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoutX1 : MonoBehaviour
{
    [SerializeField] private ScoutPanel panel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Sprite[] scoutImages;
    [SerializeField] private Image mainImage;
    [SerializeField] private Animator animator;
    [SerializeField] private Box box;
    [SerializeField] private ScoutResultDisableManager scoutManager;

    private int scoutIndex;
    private bool canExit;
    // Start is called before the first frame update
    void OnEnable()
    {
        scoutIndex = panel.scoutIndex;
        mainImage.sprite = scoutImages[scoutIndex];
        canExit = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isTrigger = box.isTrigger;
        if (isTrigger)
        {
            canExit = true;
        }
        if (canExit)
        {
            if(gameObject.activeSelf && Input.GetMouseButtonDown(0))
            {
                scoutManager.Open();
                resultPanel.SetActive(true);
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (gameObject.activeSelf && Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Open");
            }
        }
    }
}
