using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [Space(10f)]
    [SerializeField]
    private AudioClip _eatSound;
    [SerializeField]
    private AudioClip _levelUpSound;
    private AudioSource _source;

    private Rigidbody _rigidbody;

    private CameraController _camController;

    protected override void Start()
    {
        base.Start();

        _rigidbody = transform.GetComponent<Rigidbody>();
        _camController = Camera.main.GetComponent<CameraController>();
        _source = GetComponent<AudioSource>();

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

    #region LV
    public override void AddExp(int exp)
    {
        if (_source.clip != _eatSound) _source.clip = _eatSound;
        _source.Play();

        base.AddExp(exp);
    }

    protected override void LevelUp()
    {
        base.LevelUp();

        // Level Up Effect
        _source.clip = _levelUpSound;
        _source.Stop();
        _source.Play();

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
    #endregion

    protected override void UnitDie()
    {
        GameManager.Instance.GameFailed();
    }

    protected override void ChangeSceneToRelease(Scene arg0, LoadSceneMode arg1)
    {
        // None
    }
}
