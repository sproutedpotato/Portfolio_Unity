using TMPro;
using UnityEngine;

public class ItemText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    private GameManager manager;
    private int prevItemNum;
    private int curItemNum;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        prevItemNum = manager.itemNum;
    }

    // Update is called once per frame
    void Update()
    {
        curItemNum = manager.itemNum;
        if(curItemNum != prevItemNum)
        {
            if(curItemNum == 0)
            {
                text.text = "No Item";
            }
            else if(curItemNum == 1)
            {
                text.text = "Atk Bouns Lv.1";
            }
            else if( curItemNum == 2)
            {
                text.text = "Hp Bouns Lv.1";
            }
            else if (curItemNum == 3)
            {
                text.text = "Atk Bouns Lv.2";
            }
            else if (curItemNum == 4)
            {
                text.text = "Hp Bouns Lv.2";
            }
            else if (curItemNum == 5)
            {
                text.text = "Atk Bouns Lv.3";
            }
            else
            {
                text.text = "Hp Bouns Lv.3";
            }
        }
    }
}
