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

    private int _currentExp = 0;

    private Collider _collider;

    protected virtual void Awake()
    {
        _currentLevel = _unitSO.StartLevel;
    }

    protected virtual void Start()
    {
        _collider = GetComponent<Collider>();
    }

    public void AddExp(int exp)
    {
        _currentExp += exp;

        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (_currentExp >= NeedExpTable.NeedExp[_currentLevel - 1])
        {
            LevelUp();

            CheckLevelUp();
        }
    }

    protected virtual void LevelUp()
    {
        _currentExp -= NeedExpTable.NeedExp[_currentLevel - 1];
        _currentLevel += 1;

        // Level Text Update
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Unit>() != null)
        {
            if (collision.gameObject.GetComponent<Unit>().CurrentLevel > _currentLevel)
            {
                _collider.isTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Unit>() != null)
        {
            var unit = other.GetComponent<Unit>();
            if (unit.CurrentLevel > _currentLevel)
            {
                unit.AddExp(_unitSO.DropExp);

                // Destroy
                return;
            }
        }

        _collider.isTrigger = false;
    }
}
