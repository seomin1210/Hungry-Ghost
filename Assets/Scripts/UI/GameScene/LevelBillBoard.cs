using TMPro;
using UnityEngine;


public class LevelBillBoard : BillBoard
{
    [SerializeField]
    private TextMeshProUGUI _levelText;

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void UpdateLevel(int lv)
    {
        if (lv < 15)
        {
            _levelText.text = "LV. " + lv.ToString();
        }
        else if (lv == 15)
        {
            _levelText.text = "LV. MAX";
            _levelText.fontSize = 3f;
        }

        if (lv == 10) _levelText.fontSize = 2f;
    }
}
