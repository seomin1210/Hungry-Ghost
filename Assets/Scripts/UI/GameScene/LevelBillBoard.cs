using TMPro;


public class LevelBillBoard : BillBoard
{
    private TextMeshProUGUI _levelText;

    private void Start()
    {
        _levelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateLevel(string lv)
    {
        _levelText.text = "LV. " + lv;
    }
}
