using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameCanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _joystickPanel;
    [SerializeField]
    private GameObject _resultPanel;

    [Space(10f)]
    [SerializeField]
    private GameObject _clearText;
    [SerializeField]
    private GameObject _failedText;

    [Space(10f)]
    [SerializeField]
    private TextMeshProUGUI _timerText;
    [SerializeField]
    private TextMeshProUGUI _resultTimerText;

    private float _timer = 0f;
    private bool _isStop = false;

    private void Awake()
    {
        transform.GetChild(2).GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
        {
            LoadingSceneManager.LoadScene("MainScene");
        });
        _resultPanel.SetActive(false);
    }

    private void Update()
    {
        if (_isStop == false)
        {
            _timer += Time.deltaTime;

            if (_timer >= 60f)
            {
                _timerText.text = Mathf.Floor(_timer / 60f).ToString() + ":" + (_timer % 60).ToString("F1");
            }
            else
            {
                _timerText.text = _timer.ToString("F2");
            }
        }
    }

    public void GameClear()
    {
        _resultPanel.SetActive(true);
        _clearText.SetActive(true);
        _failedText.SetActive(false);

        if (_timer >= 60f)
        {
            _resultTimerText.text = Mathf.Floor(_timer / 60f).ToString() + ":" + (_timer % 60).ToString("F1");
        }
        else
        {
            _resultTimerText.text = _timer.ToString("F2");
        }
    }

    public void GameFailed()
    {
        _resultPanel.SetActive(true);
        _clearText.SetActive(false);
        _failedText.SetActive(true);

        if (_timer >= 60f)
        {
            _resultTimerText.text = Mathf.Floor(_timer / 60f).ToString() + ":" + (_timer % 60).ToString("F2");
        }
        else
        {
            _resultTimerText.text = _timer.ToString("F2");
        }
    }
}
