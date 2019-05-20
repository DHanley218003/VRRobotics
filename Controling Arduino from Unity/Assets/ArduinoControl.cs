using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoControl : MonoBehaviour
{
    public string portName;
    SerialPort arduino;

    void Start()
    {
        arduino = new SerialPort(portName, 9600);
        arduino.Open();
    }

    void Update()
    {
        if (arduino.IsOpen)
        {
            if (Input.GetKey("1"))
            {
                arduino.Write("1");
                Debug.Log(1);
            }
            else if (Input.GetKey("0"))
            {
                arduino.Write("0");
                Debug.Log(0);
            }
        }
    }
}