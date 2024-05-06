using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : Unit
{
    private int _currentExp = 0;

    private LevelBillBoard _levelBillBoard;

    private Transform _model;
    private BoxCollider _boxCollider;

    protected override void Start()
    {
        base.Start();

        _model = transform.GetChild(1);
        _boxCollider = GetComponent<BoxCollider>();

        _levelBillBoard = transform.GetChild(0).GetComponent<LevelBillBoard>();
        _levelBillBoard.UpdateLevel(_currentLevel.ToString());
    }

    public void AddExp(int exp)
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

        if (_currentLevel >= 15)
        {
            _levelBillBoard.UpdateLevel("Max");
        }
        else
        {
            _levelBillBoard.UpdateLevel(_currentLevel.ToString());
        }
        _levelBillBoard.UpdateOffset();

        _model.localScale *= 1.5f;
        _boxCollider.center = new Vector3(0f, _boxCollider.center.y * 1.5f, 0f);
        _boxCollider.size *= 1.5f;
    }
}
