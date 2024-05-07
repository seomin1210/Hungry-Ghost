using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : Unit
{
    private LevelBillBoard _levelBillBoard;

    private Transform _model;
    private BoxCollider _boxCollider;

    private Vector3 _originModelSize = Vector3.zero;
    private Vector3 _originBoxColSize = Vector3.zero;

    protected float _moveSpeed;

    protected override void Awake()
    {
        base.Awake();
        _moveSpeed = _unitSO.MoveSpeed;
    }

    protected override void Start()
    {
        base.Start();

        _model = transform.GetChild(1);
        _boxCollider = GetComponent<BoxCollider>();

        _levelBillBoard = GetComponentInChildren<LevelBillBoard>();
        _levelBillBoard.UpdateLevel(_currentLevel);
        _levelBillBoard.UpdateOffset(_currentLevel - 1);

        _originModelSize = _model.localScale;
        _originBoxColSize = _boxCollider.size;

        if (_currentLevel != 1)
        {
            _model.localScale = _originModelSize * Mathf.Pow(1.3f, _currentLevel - 1);
            _boxCollider.size = _originBoxColSize * Mathf.Pow(1.3f, _currentLevel - 1);
            _boxCollider.center = new Vector3(0f, _boxCollider.size.y * 0.5f, 0f);
        }
    }

    #region LV
    public virtual void AddExp(int exp)
    {
        if (_currentLevel >= 15) return;
        _currentExp += exp;

        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (_currentExp >= NeedExpTable.NeedExp[_currentLevel - 1])
        {
            LevelUp();

            CheckLevelUp();
        }
    }

    protected virtual void LevelUp()
    {
        _currentExp -= NeedExpTable.NeedExp[_currentLevel - 1];
        _currentLevel += 1;

        _levelBillBoard.UpdateLevel(_currentLevel);
        _levelBillBoard.UpdateOffset(_currentLevel - 1);

        _model.localScale = _originModelSize * Mathf.Pow(1.3f, _currentLevel - 1);
        _boxCollider.size = _originBoxColSize * Mathf.Pow(1.3f, _currentLevel - 1);
        _boxCollider.center = new Vector3(0f, _boxCollider.size.y * 0.5f, 0f);
    }
    #endregion

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Entity>() != null)
        {
            if (collision.gameObject.GetComponent<Entity>().CurrentLevel > _currentLevel)
            {
                _collider.isTrigger = true;
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        _collider.isTrigger = false;
    }
    #endregion

    protected override void UnitDie()
    {
        base.UnitDie();

        _levelBillBoard.UnitDead();
    }
}
