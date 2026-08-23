using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] Image[] hearts;

    private void OnEnable()
    {
        playerInfo.OnHealthChanged += UpdateHearts;
    }

    private void OnDisable()
    {
        playerInfo.OnHealthChanged -= UpdateHearts;
    }

    void UpdateHearts(float currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            float heartValue = currentHealth - i;

            if (heartValue >= 1f)
                hearts[i].fillAmount = 1f;
            else if (heartValue > 0f)
                hearts[i].fillAmount = heartValue;
            else
                hearts[i].fillAmount = 0f;
        }
    }



}
