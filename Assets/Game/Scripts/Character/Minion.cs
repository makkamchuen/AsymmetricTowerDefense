using UnityEngine;

public class Minion : AI
{
    private Mover _mover;
    [SerializeField] private PatrolPattern _patrolPattern;

    protected override void Start()
    {
        base.Start();
        _mover = GetComponent<Mover>();
    }

    public Mover GetMover()
    {
        return _mover;
    }

    public PatrolPattern GetPatrolPattern()
    {
        return _patrolPattern;
    }
}
