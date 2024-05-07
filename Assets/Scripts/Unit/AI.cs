using UnityEngine;
using UnityEngine.AI;

public class AI : Entity
{
    [SerializeField]
    private Transform _target = null;
    private NavMeshAgent _agent;

    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            if (_target.gameObject.activeSelf)
            {
                _agent.SetDestination(_target.position);
            }
            else FindTarget();
        }
        else
        {
            FindTarget();
        }
    }

    private void FindTarget()
    {
        var unitCols = Physics.OverlapSphere(transform.position, 50f, 1 << LayerMask.NameToLayer("Unit"));
        
        for (int i = 0; i < unitCols.Length; ++i)
        {
            if (unitCols[i].GetComponent<Unit>().CurrentLevel < _currentLevel)
            {
                if (unitCols[i].gameObject.activeSelf)
                {
                    _target = unitCols[i].transform;
                    break;
                }
            }
        }
    }

    protected override void LevelUp()
    {
        base.LevelUp();

        _agent.radius *= 1.3f;
    }

    public override void AddExp(int exp)
    {
        base.AddExp(exp);
        
        FindTarget();
    }
}
