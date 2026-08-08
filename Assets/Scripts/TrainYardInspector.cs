using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainYardInspector : MonoBehaviour
{
    [Header("Brake Inspection Settings")]
    public float holdDuration = 10f;
    private float currentHoldTimer = 0f;

    private bool isPlayerInYard = false;
    private bool isHoldingE = false;
    private bool brakesChecked = false;

    [Header("UI References")]
    public GameObject progressBarContainer;
    public Image progressBarFill;            
    public TMP_Text progressPercentageText;  
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        
        inputActions.Player.Interact.started += ctx => OnInteractStarted();
        inputActions.Player.Interact.canceled += ctx => OnInteractCanceled();
    }

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !brakesChecked)
        {
            isPlayerInYard = true;
            if (TutorialManager.Instance != null)
            {
                TutorialManager.Instance.ShowPopup("check train’s brake");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInYard = false;
            ResetHoldProgress();
        }
    }

    private void OnInteractStarted()
    {
        if (isPlayerInYard && !brakesChecked)
        {
            isHoldingE = true;
            if (progressBarContainer != null) progressBarContainer.SetActive(true);
        }
    }

    private void OnInteractCanceled()
    {
        if (!brakesChecked)
        {
            ResetHoldProgress();
        }
    }

    private void Update()
    {
        if (isHoldingE && isPlayerInYard && !brakesChecked)
        {
            currentHoldTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(currentHoldTimer / holdDuration);

            
            if (progressBarFill != null) progressBarFill.fillAmount = progress;
            if (progressPercentageText != null) progressPercentageText.text = Mathf.FloorToInt(progress * 100f) + "%";

            
            if (currentHoldTimer >= holdDuration)
            {
                CompleteBrakeCheck();
            }
        }
    }

    private void ResetHoldProgress()
    {
        isHoldingE = false;
        currentHoldTimer = 0f;
        if (progressBarFill != null) progressBarFill.fillAmount = 0f;
        if (progressPercentageText != null) progressPercentageText.text = "0%";
        if (progressBarContainer != null) progressBarContainer.SetActive(false);
    }

    private void CompleteBrakeCheck()
    {
        brakesChecked = true;
        isHoldingE = false;

        if (progressBarContainer != null) progressBarContainer.SetActive(false);

        
        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.ShowPopup("train is ready to go");
            if (TutorialManager.Instance.blueDotMarker != null)
            {
                TutorialManager.Instance.blueDotMarker.SetTarget(null);
            }
        }
    }
}