using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Threading;

public class TEST_RVM1 : MonoBehaviour
{
    SerialPort serial;
    public string myString;
    public float comRapidity = 2.0f;
    public string portName;
    float temps = 0.0f;
    float delay = 0.0f;
    bool setPort = true;

    public int servoDegre1; //Degré value
    public Transform servo1;
    public Button buttonServo1;
    string A;
    public int servoDegre2; //Degré value
    public Transform servo2;
    public Button buttonServo2;
    string B;
   


    // Start is called before the first frame update
    void Start()
    {
        serial = new SerialPort();
    }

    // Update is called once per frame
    void Update()
    {
        buttonServo1.onClick.AddListener(TaskOnClick);
        buttonServo2.onClick.AddListener(TaskOnClick2);
      
           

    }
    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
        {
            A = servoDegre1.ToString("NT");
            myString = string.Concat(A);
            Envoyer();
        }
    }
    void TaskOnClick2()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
        {
            B = servoDegre2.ToString("MO 30");
            myString = string.Concat(B);
            Envoyer();
        }
    }
    public void Envoyer()
    {
        if (setPort == true)
        {
            serial.PortName = portName;
            serial.Parity = Parity.None;
            serial.BaudRate = 9600;
            serial.DataBits = 8;
            serial.StopBits = StopBits.One;
            serial.Open();
            setPort = false;
        }
        serial.Write(myString + "\n");

    }
}
