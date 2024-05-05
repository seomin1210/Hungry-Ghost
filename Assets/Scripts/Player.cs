using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
    }

    public void Move()
    {

    }
}
