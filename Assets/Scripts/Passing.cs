using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AnimationHandler),typeof(Movement))]
public class Passing : MonoBehaviour
{
    [SerializeField] private List<Zone> _zones = new List<Zone>();

    private States _state;
    private Movement _movement;
    private int _currentPoint = 0;
    private AnimationHandler _animation;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _animation = GetComponent<AnimationHandler>();

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
        _state = States.Stay;
        _animation.AnimState = AnimStates.Idle;
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
        if(_zones[_currentPoint].isEmpty)
        {
            NextState();
        }
        else
        {
            _movement.Rotate();
            _animation.AnimState = AnimStates.Shoot;
        }
    }

    private void NextState()
    {
        if(_currentPoint==_zones.Count-1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        _state = States.Run;
        _currentPoint++;
        _movement.Move(_zones[_currentPoint].transform);
        _animation.AnimState = AnimStates.Run;
        Movement.onMoveEnd += OnMoveEnd;
    }
}



