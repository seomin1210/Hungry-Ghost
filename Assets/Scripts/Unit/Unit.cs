using System.Net.Sockets;
using TMPro.EditorUtilities;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    protected UnitSO _unitSO;
    public UnitSO UnitSO => _unitSO;

    protected int _currentLevel;
    public int CurrentLevel => _currentLevel;

    protected Collider _collider;

    protected virtual void Awake()
    {
        _currentLevel = _unitSO.StartLevel;
    }

    protected virtual void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Entity>() != null)
        {
            if (collision.gameObject.GetComponent<Entity>().CurrentLevel > _currentLevel)
            {
                _collider.isTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Entity>() != null)
        {
            var entity = other.GetComponent<Entity>();
            if (entity.CurrentLevel > _currentLevel)
            {
                entity.AddExp(_unitSO.DropExp);

                // Destroy
                return;
            }
        }

        _collider.isTrigger = false;
    }
}
