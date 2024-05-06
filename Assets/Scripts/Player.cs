using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _moveSpeed;

    private void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
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
}
