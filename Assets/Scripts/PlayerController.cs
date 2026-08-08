using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float lookSensitivity = 0.1f;
    public float interactDistance = 3f;

    private CharacterController controller;
    private Camera playerCamera;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;

    public CounterInteractionZone counterZone;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        
        inputActions = new PlayerInputActions();

        
        inputActions.Player.Interact.performed += ctx => PerformInteraction();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        lookInput = inputActions.Player.Look.ReadValue<Vector2>();

        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    private void PerformInteraction()
    {
        if (counterZone != null && counterZone.CanServeTicket())
        {
            TicketQueueManager.Instance.ServeCurrentPassenger();
        }
    }
}