using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinking : MonoBehaviour
{
    public float shrinkCoef = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Assumes scale is the same in all directions
        Vector3 newScale = transform.localScale - Vector3.one * Time.deltaTime * shrinkCoef;
        
        // Ensure the scale doesn't become negative
        newScale = Vector3.Max(newScale, Vector3.zero);
        
        transform.localScale = newScale;

    }
}
