using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform _mainCam;
    private Transform _player;

    private Vector3 _offset = new Vector3(0f, 1.5f, 0.5f);

    private void Awake()
    {
        _mainCam = Camera.main.transform;
        _player = FindObjectOfType<Player>().transform;
    }

    private void LateUpdate()
    {
        transform.rotation = _mainCam.rotation;
        transform.position = _player.position + _offset;
    }
}