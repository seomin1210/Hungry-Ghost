using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    [Serializable]
    private class UnitInfo
    {
        public string UnitName;
        public int cnt;
    }

    [SerializeField]
    private Transform _pos1;
    [SerializeField]
    private Transform _pos2;

    private float _xPosition;
    private float _zPosition;

    [SerializeField]
    private UnitInfo[] _unitList = null;
    [SerializeField]
    private UnitInfo[] _aiList = null;
    private Coroutine _coroutine;

    private GameCanvasManager _gameCanvasManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
            {
                Application.Quit();
            }
            else if (SceneManager.GetActiveScene().name == "GameScene")
            {
                LoadingSceneManager.LoadScene("MainScene");
            }
        }
    }

    public void Init()
    {
        _gameCanvasManager = FindObjectOfType<GameCanvasManager>();

        GameObject obj = null;
        for (int i = 0; i < _unitList.Length; i++)
        {
            for (int j = 0; j < _unitList[i].cnt; j++)
            {
                obj = PoolManager.Instance.GetGameObject(_unitList[i].UnitName);
                obj.transform.position = GetRandomPos();
                obj.transform.rotation = new Quaternion(0f, Random.Range(-180f, 180f), 0f, 0f);
            }
        }

        _coroutine = StartCoroutine(AICreate());
    }

    public Vector3 GetRandomPos()
    {
        Vector3 pos = Vector3.zero;

        _xPosition = Random.Range(_pos1.position.x, _pos2.position.x);
        _zPosition = Random.Range(_pos1.position.z, _pos2.position.z);

        pos = new Vector3(_xPosition, 0f, _zPosition);

        return pos;
    }

    private IEnumerator AICreate()
    {
        var waitTime = new WaitForSeconds(30f);
        GameObject obj = null;
        yield return new WaitForSeconds(60f);
        for (int i = 0; i < _aiList.Length; i++)
        {
            for (int j = 0; j < _aiList[i].cnt; j++)
            {
                obj = PoolManager.Instance.GetGameObject(_aiList[i].UnitName);
                obj.transform.position = GetRandomPos();

                yield return waitTime;
            }
        }
    }

    public void GameClear()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        Time.timeScale = 0f;

        _gameCanvasManager.GameClear();
    }

    public void GameFailed()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        Time.timeScale = 0f;

        _gameCanvasManager.GameFailed();
    }
}
