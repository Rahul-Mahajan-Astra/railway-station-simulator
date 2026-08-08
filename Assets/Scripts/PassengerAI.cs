using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PassengerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isApproachingLine = true;
    private bool isWaitingInQueue = false;
    private bool isWaitingOnPlatform = false;
    private bool isLeavingAngry = false;
    private Vector3 currentTargetPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        
        if (TicketQueueManager.Instance != null)
        {
            currentTargetPos = TicketQueueManager.Instance.GetQueueEntrancePosition();
            agent.SetDestination(currentTargetPos);
        }
    }

    private void Update()
    {
        
        if (isApproachingLine && !agent.pathPending && agent.remainingDistance <= 0.8f)
        {
            isApproachingLine = false;

            if (TicketQueueManager.Instance != null)
            {
                TicketQueueManager.Instance.TryJoinQueue(this);
            }
        }

        
        if (isLeavingAngry && !agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            Destroy(gameObject);
        }

        
        if (isWaitingOnPlatform && !agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            
            agent.isStopped = true;
        }
    }

    public void MoveToPosition(Vector3 position)
    {
        currentTargetPos = position;
        isWaitingInQueue = true;
        agent.SetDestination(position);
    }

    public bool IsAtCounter()
    {
        if (!isWaitingInQueue) return false;
        return Vector3.Distance(transform.position, currentTargetPos) <= 1.2f;
    }

    
    public void CompletePurchase(Vector3 platformPosition)
    {
        isWaitingInQueue = false;
        isWaitingOnPlatform = true;
        agent.isStopped = false;

        Debug.Log("Passenger bought ticket and is walking to Platform 1!");
        agent.SetDestination(platformPosition);
    }

    
    public void LeaveStation(Vector3 angryExitPosition)
    {
        isWaitingInQueue = false;
        isLeavingAngry = true;
        agent.isStopped = false;

        Debug.Log("Station line full! Passenger leaving angry.");
        agent.SetDestination(angryExitPosition);
    }
}