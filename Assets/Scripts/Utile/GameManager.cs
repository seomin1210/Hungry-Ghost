using System;
using UnityEngine;
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

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        GameObject obj = null;
        for (int i = 0; i < _unitList.Length; i++)
        {
            for (int j = 0; j < _unitList[i].cnt; j++)
            {
                obj = PoolManager.Instance.GetGameObject(_unitList[i].UnitName);
                obj.transform.position = GetRandomPos();
            }
        }
    }

    public Vector3 GetRandomPos()
    {
        Vector3 pos = Vector3.zero;

        _xPosition = Random.Range(_pos1.position.x, _pos2.position.x);
        _zPosition = Random.Range(_pos1.position.z, _pos2.position.z);

        pos = new Vector3(_xPosition, 0f, _zPosition);

        return pos;
    }
}
