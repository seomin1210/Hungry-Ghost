using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{ 
    private Rigidbody _rigidbody;

    private float _moveSpeed;

    private LevelBillBoard _levelBillBoard;

    protected override void Awake()
    {
        base.Awake();
        _moveSpeed = _unitSO.MoveSpeed;
    }

    protected override void Start()
    {
        base.Start();

        _rigidbody = transform.GetComponent<Rigidbody>();

        _levelBillBoard = FindObjectOfType<LevelBillBoard>();
        _levelBillBoard.UpdateLevel(_currentLevel);
    }

    public void Move(Vector2 dir)
    {
        Vector3 v = new Vector3(dir.x, 0f, dir.y);
        if (v != Vector3.zero)
        {
            transform.forward = v;
        }
        _rigidbody.velocity = v * _moveSpeed;
    }

    protected override void LevelUp()
    {
        base.LevelUp();

        // Level Up Effect
        _levelBillBoard.UpdateLevel(_currentLevel);
    }
}
