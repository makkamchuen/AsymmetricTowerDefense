using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    private IAction _currentAction = null;

    public void StartAction(IAction action)
    {
        if (_currentAction == action) return;
        _currentAction?.Cancel();
        _currentAction = action;
    }

    public void CancelCurrentAction()
    {
        StartAction(null);
    }
}
