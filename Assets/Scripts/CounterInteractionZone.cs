using UnityEngine;

public class CounterInteractionZone : MonoBehaviour
{
    private bool isPlayerInZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }

    public bool CanServeTicket()
    {
        return isPlayerInZone && TicketQueueManager.Instance.HasPassengerAtCounter();
    }
}