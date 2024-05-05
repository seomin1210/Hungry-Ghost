using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _titleObject;
    [SerializeField]
    private GameObject _skinObject;
    [SerializeField]
    private GameObject _settingObject;

    private void Awake()
    {
        ButtonSetting();
    }

    /// <summary>
    /// Button Click Action Add
    /// </summary>
    private void ButtonSetting()
    {
        // Start Button
        _titleObject.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            LoadingSceneManager.LoadScene("GameScene");
        });

        // Skin Button
        _titleObject.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {

        });

        // Setting Button
        _titleObject.transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
        {

        });
    }
}
