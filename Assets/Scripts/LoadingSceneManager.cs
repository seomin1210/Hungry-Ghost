using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingSceneManager : MonoBehaviour
{
    public static string NextSceneName;
    [SerializeField]
    private Image _progressBar;
    [SerializeField]
    private TextMeshProUGUI _tipText;

    private string[] _tip =
    {
        "9레벨 이상이 되면 건물을\n통과할 수 있습니다!",
        "수명이 많은 거북이를 잡으면\n더 빨리 성장할 수 있습니다.",
        "레벨이 낮은 다른 유령들을\n흡수해보세요!"
    };

    private void Start()
    {
        _tipText.text = "Tip. " + _tip[Random.Range(0, _tip.Length)];

        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        NextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(NextSceneName);
        op.allowSceneActivation = false;
        float timer = 0f;
        float loadingTimer = 0f;

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            loadingTimer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, op.progress, timer);
                if (_progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                if (loadingTimer < 3f)
                {
                    _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, 0.33f * loadingTimer, timer);
                }
                else
                {
                    _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, 1f, timer);
                    if (_progressBar.fillAmount >= 1f)
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }
}
