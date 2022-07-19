using System;
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
    private void Update()
    {
        if (_agent.remainingDistance < 0.1)
        {
            onMoveEnd?.Invoke();
        }
    }
}
