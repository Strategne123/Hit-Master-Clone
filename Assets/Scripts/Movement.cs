using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : Movable
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public override void Move(Transform destination)
    {
        _agent.SetDestination(destination.position);
        
    }
    private void Update()
    {
        if (_agent.remainingDistance < 0.1)
        {
            Movable.onMoveEnd?.Invoke();
        }
    }
}
