using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject ballPrefab; // Assign the ball prefab in the Unity Editor

    void Start() {
        SpawnNewBall();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves an object with the tag "Ball"
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy the collided ball
            Destroy(collision.gameObject);

            // Spawn a new ball at the specified location
            SpawnNewBall();
        }
    }

    void SpawnNewBall()
    {
        // Instantiate a new ball using the public ballPrefab variable
        GameObject newBall = Instantiate(ballPrefab, spawnLocation.position, Quaternion.identity);

        // Optionally, set any properties or apply forces to the new ball as needed
    }
}
