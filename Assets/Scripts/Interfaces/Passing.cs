using System;
using System.Collections.Generic;
using UnityEngine;

public class Passing : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints = new List<Transform>();

    private States _state;
    private Movable _movement;
    private int _currentPoint=1;

    private void Start()
    {
        _movement = GetComponent<Movable>();
        _movement.Move(_wayPoints[_currentPoint]);
        Movable.onMoveEnd += OnMoveEnd;
    }

    private void OnDisable()
    {
        Movable.onMoveEnd -= OnMoveEnd;
    }

    private void OnMoveEnd()
    {
        _state = (int)_state < Enum.GetNames(typeof(States)).Length-1 ? _state+1 : 0;
        print(_state);
    }
}



