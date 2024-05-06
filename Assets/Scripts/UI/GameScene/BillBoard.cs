using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform _mainCam;
    private Transform _player;

    private Vector3 _offset = Vector3.zero;
    private float[] _offsetY = { 1.5f, 3f, 4f, 5f, 5f, 6f, 7f, 8f, 10f, 13f, 16f, 20f, 24f, 30f, 35f};

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

    public void UpdateOffset(int lv)
    {
        _offset = Vector3.up * _offsetY[lv];
    }
}