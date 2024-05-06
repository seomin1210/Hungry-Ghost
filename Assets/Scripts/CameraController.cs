using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;
    private CinemachineTransposer _transposer;

    private Vector3 _originOffset;

    private void Awake()
    {
        _cam = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        _transposer = _cam.GetCinemachineComponent<CinemachineTransposer>();

        _originOffset = _transposer.m_FollowOffset;
    }

    public void ZoomOut(int lv)
    {
        _transposer.m_FollowOffset = _originOffset * (1f + 0.25f * lv);
    }
}
