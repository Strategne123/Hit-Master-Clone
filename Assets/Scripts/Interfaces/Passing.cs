using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Passing : MonoBehaviour
{
    [SerializeField] private List<Zone> _zones = new List<Zone>();
    [SerializeField] private List<Transform> _wayPoints = new List<Transform>();

    private States _state;
    private Movement _movement;
    private int _currentPoint=0;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        
    }

    private void OnEnable()
    {
        TapHandler.TapEvent += TapEvent;
    }

    private void OnDisable()
    {
        TapHandler.TapEvent -= TapEvent;
        Movement.onMoveEnd -= OnMoveEnd;
    }

    private void OnMoveEnd()
    {
        _state = (int)_state < Enum.GetNames(typeof(States)).Length-1 ? _state+1 : 0;
        print(_state);
    }

    private void TapEvent(RectTransform rect, Vector3 position)
    {
        if(_state==States.Stay)
        {
            Vector2 direction;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, position, null, out direction);
            TryShoot(direction);
        }
    }

    private void TryShoot(Vector2 direction)
    {
        NextState();
        if(_zones[_currentPoint].isEmpty)
        {
            NextState();
        }
        else
        {
            
        }
    }

    private void NextState()
    {
        _currentPoint++;
        _movement.Move(_wayPoints[_currentPoint]);
        Movement.onMoveEnd += OnMoveEnd;
    }
}



