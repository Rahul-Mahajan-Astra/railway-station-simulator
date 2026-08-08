using UnityEngine;
using UnityEngine.InputSystem;

public class ScheduleMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainTabMenu;
    public GameObject platformDropdownPanel;

    [Header("Train Reference")]
    public TrainController trainController;

    private bool isMenuOpen = false;

    private void Update()
    {
        
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ToggleTabMenu();
        }
    }

    public void ToggleTabMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (mainTabMenu != null)
            mainTabMenu.SetActive(isMenuOpen);

        if (!isMenuOpen && platformDropdownPanel != null)
            platformDropdownPanel.SetActive(false);

        
        Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMenuOpen;
    }

    public void OnClick_ScheduleTrain()
    {
        if (platformDropdownPanel != null)
        {
            platformDropdownPanel.SetActive(!platformDropdownPanel.activeSelf);
        }
    }

    public void OnClick_SelectPlatform1()
    {
        if (trainController != null)
        {
            trainController.MoveToPlatform1();
        }

        ToggleTabMenu();
    }
}