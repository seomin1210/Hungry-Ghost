using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : PoolAble
{
    [SerializeField]
    protected UnitSO _unitSO;
    public UnitSO UnitSO => _unitSO;

    protected int _currentLevel;
    public int CurrentLevel => _currentLevel;
    protected int _currentExp = 0;

    protected Collider _collider;

    protected virtual void Awake()
    {
        _currentLevel = _unitSO.StartLevel;
    }

    protected virtual void Start()
    {
        _collider = GetComponent<Collider>();

        SceneManager.sceneLoaded += ChangeSceneToRelease;
    }

    protected virtual void ChangeSceneToRelease(Scene arg0, LoadSceneMode arg1)
    {
        ReleaseObject();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>() != null)
        {
            var entity = other.GetComponent<Entity>();
            if (entity.CurrentLevel > _currentLevel)
            {
                if (_unitSO.UnitType == UnitType.prey)
                {
                    entity.AddExp(_unitSO.DropExp);
                }
                else
                {
                    int exp = (int)(NeedExpTable.NeedExp[_currentLevel - 1] * 0.1f) + _currentExp;
                    entity.AddExp(exp);
                }

                UnitDie();
                return;
            }
        }
    }

    protected virtual void UnitDie()
    {
        ReleaseObject();
    }
}
