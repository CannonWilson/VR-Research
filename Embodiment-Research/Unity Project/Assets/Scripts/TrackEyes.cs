using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEyes : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform centerEyeAnchor;
    public float yoffset = 1.7f;
    public float zoffset = 0.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float avatarScale = transform.localScale.y;
        Vector3 eyePos = centerEyeAnchor.transform.position;
        float cury = transform.position.y;
        transform.position = new Vector3(eyePos.x, cury, eyePos.z - zoffset);
        float yRotation = centerEyeAnchor.eulerAngles.y;
        transform.eulerAngles = new Vector3(0, yRotation, 0);
    }
}
