using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(IAnimated),typeof(IMovable))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private List<Zone> _zones = new List<Zone>();

    private States _state;
    private IMovable _movement;
    private IAnimated _animation;
    private int _currentPoint = 0;

    private void OnEnable()
    {
        TapHandler.TapEvent += TapEvent;
    }

    private void Start()
    {
        _movement = GetComponent<IMovable>();
        _animation = GetComponent<IAnimated>();
    }

    private void OnDisable()
    {
        TapHandler.TapEvent -= TapEvent;
        Movement.onMoveEnd -= OnMoveEnd;
    }

    private void OnMoveEnd()
    {
        Movement.onMoveEnd -= OnMoveEnd;
        _state = States.Stay;
        _animation.SetState(AnimStates.Idle);
    }

    private void TapEvent(RectTransform rect, Vector3 position)
    {
        if (_state==States.Stay)
        {
            Vector2 direction;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, position, null, out direction);
            TryShoot(direction);
        }
    }

    private void TryShoot(Vector2 direction)
    {
        if(!_animation.IsState(AnimStates.Shoot))
        {
            if (_zones[_currentPoint].IsEmpty)
            {
                NextState();
            }
            else
            {
                _movement.RotateToTap();
                _animation.SetState(AnimStates.Shoot);
            }
        } 
    }

    private void NextState()
    {
        if(_currentPoint==_zones.Count-1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        _state = States.Run;
        _currentPoint++;
        _movement.Move(_zones[_currentPoint].transform);
        _animation.SetState(AnimStates.Run);
        Movement.onMoveEnd += OnMoveEnd;
    }
}



