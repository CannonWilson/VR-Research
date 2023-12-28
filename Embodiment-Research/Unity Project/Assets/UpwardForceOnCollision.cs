using UnityEngine;

public class UpwardForceOnCollision : MonoBehaviour
{
    public float forceMultiplier = 2.0f;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is from below (you might need to adjust this based on your object's orientation)
        if (collision.contacts[0].normal.y > 0.5f)
        {
            // Get the velocity and direction of the colliding object
            Vector3 collisionVelocity = collision.relativeVelocity;
            Vector3 collisionDirection = collisionVelocity.normalized;

            // Calculate the upward force based on the magnitude of the velocity
            float upwardForce = forceMultiplier * Mathf.Max(collisionVelocity.y, 0.0f);

            // Apply the upward force in the direction of the colliding object
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(collisionDirection * upwardForce, ForceMode.Impulse);
            }
        }
    }
}
