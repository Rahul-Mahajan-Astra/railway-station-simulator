using UnityEngine;

public class TrainController : MonoBehaviour
{
    public Transform trainYardPos;
    public Transform platform1Pos;
    public float moveSpeed = 8f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        
        if (trainYardPos != null)
        {
            transform.position = trainYardPos.position;
            transform.rotation = trainYardPos.rotation;
        }
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;
                isMoving = false;
                Debug.Log("Train – 1 has arrived at Platform 1!");
            }
        }
    }

    public void MoveToPlatform1()
    {
        if (platform1Pos != null)
        {
            targetPosition = platform1Pos.position;
            isMoving = true;
        }
    }
}