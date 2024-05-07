using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private Rigidbody _rigidbody;

    private CameraController _camController;

    protected override void Start()
    {
        base.Start();

        _rigidbody = transform.GetComponent<Rigidbody>();
        _camController = Camera.main.GetComponent<CameraController>();

        GameManager.Instance.Init();
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

        // Camera Zoom Out
        if (_currentLevel > 8)
        {
            _camController.ZoomOut(_currentLevel - 6);
        }

        // Max
        if (_currentLevel >= 15)
        {
            GameManager.Instance.GameClear();
        }
    }

    protected override void UnitDie()
    {
        GameManager.Instance.GameFailed();

        base.UnitDie();
    }
}
