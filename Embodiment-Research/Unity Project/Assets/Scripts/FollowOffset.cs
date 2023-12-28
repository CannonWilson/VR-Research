using UnityEngine;

public class FollowOffset : MonoBehaviour
{
    public Transform targetTransform; // The transform to follow
    public float Offset = 0.2f; // The desired y-axis offset

    public string offsetDirection = "z";

    private void Update()
    {
        if (targetTransform != null)
        {
            Vector3 targetPosition = targetTransform.position;
            Vector3 newPosition = transform.position;
            if (offsetDirection == "x") {
                newPosition.x = targetPosition.x + Offset;
            }
            else if (offsetDirection == "y") {
                newPosition.y = targetPosition.y + Offset;
            }
            else if (offsetDirection == "x") {
                newPosition.z = targetPosition.z + Offset;
            }
            
            transform.position = newPosition;
        }
    }
}
