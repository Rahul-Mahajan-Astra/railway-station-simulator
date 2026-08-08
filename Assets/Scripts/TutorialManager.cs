using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("Dev Mode Settings")]
    public bool isDevMode = true;
    public int initialPassengerCount = 5;

    [Header("References")]
    public PassengerSpawner spawner;
    public ObjectiveMarker blueDotMarker;
    public Transform ticketCounterTarget;
    public Transform trainYardTarget;

    [Header("UI Pop-up References")]
    public GameObject popupPanel;
    public TMP_Text popupText;

    private bool ticketObjectiveComplete = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (isDevMode)
        {
            SpawnDevPassengers();
            StartTicketObjective();
        }
    }

    private void SpawnDevPassengers()
    {
        if (spawner != null)
        {
            for (int i = 0; i < initialPassengerCount; i++)
            {
                spawner.SpawnSinglePassenger();
            }
        }
    }

    public void StartTicketObjective()
    {
        ShowPopup("Head to ticket counter");
        if (blueDotMarker != null)
        {
            blueDotMarker.SetTarget(ticketCounterTarget);
        }
    }

    public void OnTicketSold()
    {
        if (!ticketObjectiveComplete)
        {
            ticketObjectiveComplete = true;
            StartTrainYardObjective();
        }
    }

    public void StartTrainYardObjective()
    {
        ShowPopup("Head to the train yard");
        if (blueDotMarker != null)
        {
            blueDotMarker.SetTarget(trainYardTarget);
        }
    }

    public void ShowPopup(string textMessage)
    {
        if (popupPanel != null && popupText != null)
        {
            popupText.text = textMessage;
            popupPanel.SetActive(true);
        }
    }

    public void HidePopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }


}