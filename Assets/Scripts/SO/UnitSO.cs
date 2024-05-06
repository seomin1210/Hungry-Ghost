using UnityEngine;

[CreateAssetMenu(menuName = "SO/Unit")]
public class UnitSO : ScriptableObject
{
    public UnitType UnitType;

    public int StartLevel = 0;

    public int DropExp;

    public float MoveSpeed;
}
