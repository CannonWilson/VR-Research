using UnityEngine;
using Oculus.Platform;
using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject objectToToggle;
    public OVRInput.Button toggleMenuButton;
    public OVRInput.Button resetAvatarPosButton;
    public OVRInput.Button resetAvatarRotButton;
    public OVRInput.Button resetAvatarScaButton;
    public OVRInput.Button recordPressButton;
    public OVRInput.Button pauseGrowthButton;
    private bool isToggled = true; // Initial state
    private Vector3 originalOffset;
    public Transform avatarTransform;
    public Transform avEyelashTransform;
    public Transform centerEyeAnchor;
    public Growing growingScript; // Reference to the Growing script on your avatar
    public BluetoothTest bluetoothScript;
    public int numberOfPressesToRecord = 9;
    private bool recordingStarted = false;
    public PositionHead posHeadScript;
    public Vector3 eyeOffset = Vector3.zero;

    private List<float> pressTimestamps = new List<float>();
    private float averageTimeBetweenPresses = 0.0f;

    public Blinking blinkingScript;

    public Transform playerTransform;

    private void Start()
    {
        // Ensure the initial state is as expected
        objectToToggle.SetActive(isToggled);

        // Get the starting offset between the OVR Camera Rig and 
        // the avatar this offset will be used 
        originalOffset = avatarTransform.position - centerEyeAnchor.position;
    }

    private void Update()
    {
        if (OVRInput.GetDown(toggleMenuButton)) {
            isToggled = !isToggled; // Toggle the state

            // Set the active state of the GameObject
            objectToToggle.SetActive(isToggled);
        }

        if (OVRInput.GetDown(resetAvatarPosButton)) {

            Vector3 rightConPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            avatarTransform.position = new Vector3(rightConPos.x, rightConPos.y - 1.0f, rightConPos.z);
            
            // Vector3 eyeOffsetFromAvatar = centerEyeAnchor.position - (avEyelashTransform.position + eyeOffset);

            // // Move the avatar to the new position
            // avatarTransform.position += eyeOffsetFromAvatar;

            // Vector3 eyeFootOffset = avEyelashTransform.position - avatarTransform.position;

            // avatarTransform.position += eyeFootOffset;
        }

        if (OVRInput.GetDown(resetAvatarRotButton)) {
            // Update avatar rotation
            Vector3 newRotation = avatarTransform.eulerAngles;
            newRotation.y = centerEyeAnchor.eulerAngles.y;
            avatarTransform.eulerAngles = newRotation;
        }

        if (OVRInput.GetDown(resetAvatarScaButton)) {
            // Update player scale
            // avatarTransform.localScale = Vector3.one;
            playerTransform.localScale = Vector3.one;
        }

        if (OVRInput.GetDown(pauseGrowthButton)) {
            // Toggle the shouldGrow boolean in the Growing script
            growingScript.shouldGrow = !growingScript.shouldGrow;
        }

        if (OVRInput.GetDown(recordPressButton)) {
            if (!recordingStarted) {
                recordingStarted = true;
            }
            else {
                pressTimestamps.Add(Time.time);
                if (pressTimestamps.Count >= numberOfPressesToRecord)
                {
                    CalculateAverageTime();
                    blinkingScript.waitTime = averageTimeBetweenPresses;
                    blinkingScript.shouldBlink = true;
                }
            }
        }

    }


    private void CalculateAverageTime()
    {
        float totalTimeBetweenPresses = 0;

        for (int i = 1; i < pressTimestamps.Count; i++)
        {
            totalTimeBetweenPresses += pressTimestamps[i] - pressTimestamps[i-1];
        }

        averageTimeBetweenPresses = totalTimeBetweenPresses / (pressTimestamps.Count-1);
        bluetoothScript.recordedInterval = averageTimeBetweenPresses;

        // Reset the recording for the next set of button presses
        recordingStarted = false;
        pressTimestamps.Clear();
    }
}

