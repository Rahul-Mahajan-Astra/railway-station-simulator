using System.Collections.Generic;
using UnityEngine;

public class TicketQueueManager : MonoBehaviour
{
    public static TicketQueueManager Instance;

    [Header("Queue Config")]
    public Transform queueStartPoint;
    public float queueSpacing = 1.2f;
    public int maxQueueCapacity = 5;

    [Header("Station Exits")]
    public Transform exitPoint;
    public Transform leaveAngryPoint;

    private List<PassengerAI> passengerQueue = new List<PassengerAI>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool TryJoinQueue(PassengerAI passenger)
    {
        
        if (passengerQueue.Count >= maxQueueCapacity)
        {
            passenger.LeaveStation(leaveAngryPoint != null ? leaveAngryPoint.position : exitPoint.position);
            return false;
        }

        passengerQueue.Add(passenger);
        UpdateQueuePositions();
        return true;
    }

    public Vector3 GetQueueEntrancePosition()
    {
        
        int currentCount = passengerQueue.Count;
        return queueStartPoint.position - (queueStartPoint.forward * (currentCount * queueSpacing));
    }

    public void UpdateQueuePositions()
    {
        for (int i = 0; i < passengerQueue.Count; i++)
        {
            Vector3 targetPos = queueStartPoint.position - (queueStartPoint.forward * (i * queueSpacing));
            passengerQueue[i].MoveToPosition(targetPos);
        }
    }

    public bool HasPassengerAtCounter()
    {
        return passengerQueue.Count > 0 && passengerQueue[0].IsAtCounter();
    }

    public void ServeCurrentPassenger()
    {
        if (passengerQueue.Count > 0 && passengerQueue[0].IsAtCounter())
        {
            PassengerAI servedPassenger = passengerQueue[0];
            passengerQueue.RemoveAt(0);

            
            Vector3 platformSpot = exitPoint.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-0.8f, 0.8f));

            servedPassenger.CompletePurchase(platformSpot);
            UpdateQueuePositions();

            if (TutorialManager.Instance != null)
            {
                TutorialManager.Instance.OnTicketSold();
            }
        }
    }
}