using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using TMPro;


public class BluetoothTest : MonoBehaviour
{
    public Text deviceName;
    public Text dataToSend;
    private bool IsConnected;
    public static string dataRecived = "";
    public TextMeshProUGUI pairedDevicesText;
    public TextMeshProUGUI connectedDevicesText;
    public TextMeshProUGUI receivedDataText;
    public Transform lightObj; // point light to turn on and off
    public float buzzInterval; // how many seconds to wait between turning off and on motor/light
    private float timeCounter = 0.0f;
    private bool isMotorActive = false;
    private bool isLightOn = false;

    public float recordedInterval = 0.0f; // set by controller presses to match arduino


    // Start is called before the first frame update
    void Start()
    {
    #if UNITY_2020_2_OR_NEWER
        #if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
          || !Permission.HasUserAuthorizedPermission(Permission.FineLocation)
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE")
          || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
                    Permission.RequestUserPermissions(new string[] {
                        Permission.CoarseLocation,
                            Permission.FineLocation,
                            "android.permission.BLUETOOTH_SCAN",
                            "android.permission.BLUETOOTH_ADVERTISE",
                             "android.permission.BLUETOOTH_CONNECT"
                    });
        #endif
    #endif

        IsConnected = false;
        BluetoothService.CreateBluetoothObject();
        // StartCoroutine(Flashing());
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsConnected) {

            // Read in data from Bluetooth
            // try {
            //    string datain =  BluetoothService.ReadFromBluetooth();
            //     if (datain.Length > 1) {
            //         dataRecived = datain;
            //         receivedDataText.text = receivedDataText.text + dataRecived;
            //     }

            // }
            // catch (Exception e) {
            //     BluetoothService.Toast("Error in connection");
            //     receivedDataText.text = "Connection error";
            // }


            // If connected and motor is set on, flash the light/motors
            // if (isMotorActive && recordedInterval != 0) { // set by SendStartCommand()
            //     if (timeCounter >= recordedInterval) {
            //         lightObj.gameObject.SetActive(!isLightOn);
            //         isLightOn = !isLightOn;
            //         timeCounter = timeCounter - recordedInterval;
            //     }
            //     else {
            //         timeCounter += Time.deltaTime;
            //     }
            // }
            
        }
        
    }

    public void GetDevicesButton()
    {
       string[] devices = BluetoothService.GetBluetoothDevices();
        pairedDevicesText.text = "";
        foreach(var d in devices)
        {
            Debug.Log(d);
            pairedDevicesText.text = pairedDevicesText.text + " " + d;
        }
    }

    public void StartButton()
    {
        if (!IsConnected)
        {
            IsConnected =  BluetoothService.StartBluetoothConnection(deviceName.text.ToString());
            BluetoothService.Toast(deviceName.text.ToString()+" status: " + IsConnected);
            connectedDevicesText.text = "Connection created";
        }
    }

    // public void SendStartCommand() {
    //     if (IsConnected) {
    //         // Start pulsing the motor and light
    //         BluetoothService.WritetoBluetooth("START;");
    //         isMotorActive = true;
    //     }
    // }

    public void RunMotorForCollision() {
        if (IsConnected) {
            BluetoothService.WritetoBluetooth("M1-255;");
            receivedDataText.text = "Sent Collide Cmd";
            StartCoroutine(TurnOffMotor());
        }
    }

    IEnumerator TurnOffMotor() {
        yield return new WaitForSeconds(0.7f);
        TurnOffMotorForCollision();
    }

    private void TurnOffMotorForCollision() {
        BluetoothService.WritetoBluetooth("M1-0;");
    }

    public void SendButton()
    {
        if (IsConnected && (dataToSend.ToString() != "" || dataToSend.ToString() != null)) {
            BluetoothService.WritetoBluetooth(dataToSend.text.ToString()+";");
            connectedDevicesText.text = "Send succeeded";
        }
        else {
            connectedDevicesText.text = "Send failed";
            BluetoothService.WritetoBluetooth("Not connected");
        }
    }


    public void StopButton()
    {
        if (IsConnected)
        {
            BluetoothService.StopBluetoothConnection();
        }
        Application.Quit();
    }

    /*
    IEnumerator Flashing() {
        while (true)
        {
            // If connected to the microcontroller, 
            // turn off and on the motor at interval
            // buzzInterval
            if(IsConnected) {
                
                BluetoothService.WritetoBluetooth("MA-30;"); // Turn motor A on at 30/255 strength
                lightObj.gameObject.SetActive(true);
                // BluetoothService.WritetoBluetooth("MA-30;"); // Send same ON command to make sure it's received
                
                yield return new WaitForSeconds(buzzInterval); // Wait

                BluetoothService.WritetoBluetooth("MA-0;"); // Turn off motor A
                lightObj.gameObject.SetActive(false);
                // BluetoothService.WritetoBluetooth("MA-0;"); // Send same OFF command to make sure it's received
                
                yield return new WaitForSeconds(buzzInterval); // Wait
            }
            else yield return null;
        }

    }
    */
}





     
        
    
