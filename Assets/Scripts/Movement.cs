using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour, IMovable
{
    private NavMeshAgent _agent;
    public static Action onMoveEnd;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_agent.remainingDistance < 0.1 && _agent.remainingDistance !=0)
        {
            onMoveEnd?.Invoke();
        }
    }

    public void Move(Transform destination)
    {
        _agent.SetDestination(destination.position);
    }

    public void RotateToTap()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        transform.LookAt(hit.point);
    }
}
