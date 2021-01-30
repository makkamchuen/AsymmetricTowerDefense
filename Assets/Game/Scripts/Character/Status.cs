using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    private bool _controllable;
    private bool _attackable;
    private bool _moveable;

    public Status()
    {
        _controllable = true;
        _attackable = true;
        _moveable = true;
    }

    public bool Moveable()
    {
        return _moveable;
    }
    
    public bool Attackable()
    {
        return _attackable;
    }

    public bool Controllable()
    {
        return _controllable;
    }

    public void SetDeadStatus()
    {
        _controllable = false;
        _attackable = false;
        _moveable = false;
    }
}
