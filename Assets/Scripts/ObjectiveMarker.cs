using UnityEngine;

public class ObjectiveMarker : MonoBehaviour
{
    public Transform currentTarget;
    public Vector3 offset = new Vector3(0, 2f, 0);

    private void LateUpdate()
    {
        if (currentTarget != null)
        {
            transform.position = currentTarget.position + offset;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
        gameObject.SetActive(newTarget != null);
    }
}