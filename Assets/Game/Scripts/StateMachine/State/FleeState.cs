using System;
using Game.Scripts;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[CreateAssetMenu (menuName = "StateMachine/State/Flee State")]
public class FleeState: State
{
    private Actor _target;
    private Vector3 _prevPosition;
    
    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        _target = controller.target.GetTargetActor();
        _prevPosition = controller.transform.position;
        if (controller.target is Minion minion)
        {
            var pos = Utils.GetRandomPoint(minion.transform.position, controller.target.GetFlee().minDist, controller.target.GetFlee().maxDist);
            minion.GetMover().MoveTo(pos);
        }
    }

    public override void OnExit(StateController controller)
    {
        base.OnExit(controller);
        if (controller.target is Minion minion)
        {
            minion.GetMover().MoveTo(_prevPosition);
        }
    }
    
}