using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using System.Globalization;
using System.Linq;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI skipText;
    [SerializeField] private TextMeshProUGUI nextText;
    [SerializeField] private Image skipGauge;
    [SerializeField] private GameState gameState;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private Sprite[] images_00;
    [SerializeField] private Sprite[] images_01;
    [SerializeField] private Sprite[] images_10;
    [SerializeField] private Sprite[] images_11;
    [SerializeField] private Sprite[] images_20;
    [SerializeField] private Sprite[] images_21;
    [SerializeField] private Image image;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip btnSound;

    private int currentLine;
    private string sceneID;
    private string[] story;
    private bool isHolding;
    private float keyDownTime;
    private float skipCount = 3f;
    private GameManager manager;

    private void Start()
    {
        manager = GameManager.Instance;
        sceneID = "" + manager.ReturnTimeOrDay("Day") + manager.ReturnTimeOrDay("Time") + "Intro";
        LoadStoryFromXML(sceneID);
        text.text = "";
        isHolding = false;
        sceneController = GameObject.Find("GameManager").GetComponent<SceneController>();
    }
    void Update()
    {
        if (!manager.canMove)
        {
            return;
        }

        if (gameState.isGamePaused) // GameState의 isGamePaused 상태 확인
        {
            return; // 게임이 일시 정지 상태일 때는 업데이트를 건너뜁니다.
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keyDownTime = Time.time;
                isHolding = true;
                skipText.text = "";
                nextText.text = "";
            }
            if (Input.GetKey(KeyCode.Space) && isHolding)
            {
                float holdTime = Time.time - keyDownTime;
                if (holdTime >= 1f)
                {
                    float gaugePercent = Mathf.InverseLerp(1f, skipCount, holdTime);
                    skipGauge.fillAmount = gaugePercent;
                }
                else
                {
                    skipGauge.fillAmount = 0;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                audioSource.PlayOneShot(btnSound);
                isHolding = false;
                float holdTime = Time.time - keyDownTime;

                skipGauge.fillAmount = 0f;
                if (holdTime >= skipCount)
                {
                    Debug.Log(manager.ReturnTimeOrDay("Time"));
                    ChangeScene();
                }
                else
                {
                    currentLine += 1;
                    image.color = Color.white;
                    if (currentLine < story.Length)
                    {
                        this.text.text = story[currentLine];
                    }
                    else
                    {
                        ChangeScene();
                        return;
                    }
                    
                    if (sceneID == "00Intro")
                    {
                        if (currentLine > 0 && currentLine <= images_00.Length)
                        {
                            image.sprite = images_00[currentLine - 1];
                        }
                        else
                        {
                            image.sprite = images_00[images_00.Length - 1];
                        }
                    }
                    else if (sceneID == "01Intro")
                    {
                        if (currentLine > 0 && currentLine <= images_01.Length)
                        {
                            image.sprite = images_01[currentLine - 1];
                        }
                        else
                        {
                            image.sprite = images_01[images_01.Length - 1];
                        }
                    }
                    else if (sceneID == "10Intro")
                    {
                        if (currentLine > 0 && currentLine <= images_10.Length)
                        {
                            image.sprite = images_10[currentLine - 1];
                        }
                        else
                        {
                            image.sprite = images_10[images_10.Length - 1];
                        }
                    }
                    else if (sceneID == "11Intro")
                    {
                        if (currentLine > 0 && currentLine <= images_11.Length)
                        {
                            image.sprite = images_11[currentLine - 1];
                        }
                        else
                        {
                            image.sprite = images_11[images_11.Length - 1];
                        }
                    }
                    else if (sceneID == "20Intro")
                    {
                        if (currentLine > 0 && currentLine <= images_20.Length)
                        {
                            image.sprite = images_20[currentLine - 1];
                        }
                        else
                        {
                            image.sprite = images_20[images_20.Length - 1];
                        }
                    }
                    else if (sceneID == "21Intro")
                    {
                        if (currentLine > 0 && currentLine <= images_21.Length)
                        {
                            image.sprite = images_21[currentLine - 1];
                        }
                        else
                        {
                            image.sprite = images_21[images_21.Length - 1];
                        }
                    }
                }
            }
        }
    }

    void LoadStoryFromXML(string id)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("StoryText");
        XmlSerializer serializer = new XmlSerializer(typeof(Story));
        using (StringReader reader = new StringReader(textAsset.text))
        {
            Story data = serializer.Deserialize(reader) as Story;
            SceneData scene = data.scenes.Find(s => s.id == id);

            if (scene != null)
            {
                // 줄바꿈 문자 치환 처리
                story = scene.line
                    .Select(line => line.Replace("\\n", "\n"))
                    .ToArray();
            }
            else
            {
                Debug.Log("No data");
                story = new string[] { "Can't find story." };
            }
        }
    }

    private void ChangeScene()
    {
        if (manager.ReturnTimeOrDay("Time") == 0)
        {
            sceneController.ChangeScene("DayTime");
        }
        else
        {
            string sceneName = "Stage" + manager.ReturnTimeOrDay("Day");
            sceneController.ChangeScene(sceneName);
        }
    }
}
