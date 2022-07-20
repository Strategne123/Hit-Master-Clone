using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    private NavMeshAgent _agent;
    public static Action onMoveEnd;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Move(Transform destination)
    {
        _agent.SetDestination(destination.position);
        
    }

    public void Rotate()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        transform.LookAt(hit.point);
    }

    private void Update()
    {
        if (_agent.remainingDistance < 0.1 && _agent.remainingDistance !=0)
        {
            onMoveEnd?.Invoke();
        }
    }
}
