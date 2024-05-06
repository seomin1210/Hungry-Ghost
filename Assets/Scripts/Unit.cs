using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private UnitSO _unitSO;
    public UnitSO UnitSO => _unitSO;

    private int _currentLevel;
    public int CurrentLevel => _currentLevel;

    private int _currentExp;

    public void AddExp(int exp)
    {
        _currentExp += exp;

        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (_currentExp >= NeedExpTable.NeedExp[_currentLevel - 1])
        {
            _currentExp -= NeedExpTable.NeedExp[_currentLevel - 1];
            _currentLevel += 1;
            CheckLevelUp();
        }
    }
}
