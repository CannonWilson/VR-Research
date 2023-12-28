using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHead : MonoBehaviour
{


    public Transform referenceTransform; // The third transform to match
    public Transform childTransform; // The child transform whose position you want to align
    public Vector3 offset = Vector3.zero; // Offset from the reference transform


    

    public void AlignEyes() {
        // Move the avatar to the position
        // where it needs to be in order to locate the eyes at an offset to 
        // the eyelashes GO on the character

        Vector3 targetRelativePosition = childTransform.position - offset;

        // Move the reference transform to the target relative position
        referenceTransform.position = targetRelativePosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        // AlignEyes();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = followTransform.position + offset;
    }
}


