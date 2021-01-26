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
}
