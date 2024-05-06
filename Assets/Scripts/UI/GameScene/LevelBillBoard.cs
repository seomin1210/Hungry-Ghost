using TMPro;


public class LevelBillBoard : BillBoard
{
    private TextMeshProUGUI _levelText;

    private void Start()
    {
        _levelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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
