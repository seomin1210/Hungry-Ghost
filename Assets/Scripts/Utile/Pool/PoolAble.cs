using UnityEngine;
using UnityEngine.Pool;

public class PoolAble : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }

    public virtual void ReleaseObject()
    {
        if (gameObject.activeSelf == false) return;
        transform.SetParent(PoolManager.Instance.transform);
        Pool.Release(gameObject);
    }
}
