using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : Unit
{
    private int _currentExp = 0;

    private LevelBillBoard _levelBillBoard;

    private Transform _model;
    private BoxCollider _boxCollider;

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

        _levelBillBoard = transform.GetChild(0).GetComponent<LevelBillBoard>();
        _levelBillBoard.UpdateLevel(_currentLevel);
        _levelBillBoard.UpdateOffset(_currentLevel - 1);
    }

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

        _model.localScale *= 1.3f;
        _boxCollider.center = new Vector3(0f, _boxCollider.center.y * 1.3f, 0f);
        _boxCollider.size *= 1.3f;
    }
}
