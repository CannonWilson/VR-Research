using UnityEngine;

public class Growing : MonoBehaviour
{
    public float growthFactor = 0.01f; // The factor by which the object will grow in the x and z directions
    public float growthInterval = 0.1f; // The time interval between each growth

    private float timer = 0f;

    public bool shouldGrow = false;

    private void Update()
    {
        if (shouldGrow) {
            timer += Time.deltaTime;

            if (timer >= growthInterval)
            {
                // Increase the scale of the object in only the y direction
                Vector3 currentScale = transform.localScale;
                Vector3 newScale = new Vector3(currentScale.x, currentScale.y + growthFactor, currentScale.z);
                transform.localScale = newScale;

                timer = 0f; // Reset the timer
            }
        }
    }
}

