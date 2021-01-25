using UnityEngine;

public class Minion : AI
{
    [HideInInspector] public Mover mover;
    public PatrolPattern patrolPattern;

    protected override void Start()
    {
        base.Start();
        mover = GetComponent<Mover>();
    }
}
