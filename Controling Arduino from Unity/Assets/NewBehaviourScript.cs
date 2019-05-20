using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Threading;

public class NewBehaviourScript : MonoBehaviour
{

    //communication
    SerialPort serial;
    public string myString;
    float temps = 0.0f;
    float delay = 0.0f;
    public float comRapidity = 2.0f;
    public string portName;
    bool setPort = true;

    //Servo 1 
    public int servoDegre1; //Degré value
    public Transform servo1;
    public Slider sliderServo1;
    public Button buttonS1plus;
    public Button buttonS1moins;
    public Text textS1;
    string A;

    // Servo 2
    public int servoDegre2; //Degré value
    public Transform servo2;
    public Slider sliderServo2;
    public Button buttonS2plus;
    public Button buttonS2moins;
    public Text textS2;
    string B;

    // Autre servo test


    // Use this for initialization
    void Start()
    {
        //communication
        serial = new SerialPort();

        //Button S1
        buttonS1plus.onClick.AddListener(TaskOnClickS1plus);
        buttonS1moins.onClick.AddListener(TaskOnClickS1moins);

        //Button S2
        buttonS2plus.onClick.AddListener(TaskOnClickS2plus);
        buttonS2moins.onClick.AddListener(TaskOnClickS2moins);
    }

    // Update is called once per frame
    void Update()
    {

        //Servo 1 
        if (servoDegre1 != (sliderServo1.value))
        {
            if (servoDegre1 < (sliderServo1.value))
            {
                servoDegre1 = servoDegre1 + 1;
            }
            if (servoDegre1 > (sliderServo1.value))
            {
                servoDegre1 = servoDegre1 - 1;
            }
        }
        servo1.localRotation = Quaternion.AngleAxis(-servoDegre1, Vector3.up);
        textS1.text = servoDegre1.ToString() + "°";

        //Servo 2 
        if (servoDegre2 != (sliderServo2.value))
        {
            if (servoDegre2 < (sliderServo2.value))
            {
                servoDegre2 = servoDegre2 + 1;
            }

            if (servoDegre2 > (sliderServo2.value))
            {
                servoDegre2 = servoDegre2 - 1;
            }
        }
        servo2.localRotation = Quaternion.AngleAxis(-servoDegre2, Vector3.up);
        textS2.text = servoDegre2.ToString() + "°";

        //communication

        A = servoDegre1.ToString("000");
        B = servoDegre2.ToString("000");

        myString = string.Concat(A, B);

        temps = Time.time;
        if ((delay + comRapidity) < temps)
        {
            Envoyer();
            delay = temps;
            //Debug.Log (myString);
        }

    }

    // button S1
    void TaskOnClickS1plus()
    {
        (sliderServo1.value) = (sliderServo1.value) + 1;
    }

    void TaskOnClickS1moins()
    {
        (sliderServo1.value) = (sliderServo1.value) - 1;
    }
    // button S2
    void TaskOnClickS2plus()
    {
        (sliderServo2.value) = (sliderServo2.value) + 1;
    }

    void TaskOnClickS2moins()
    {
        (sliderServo2.value) = (sliderServo2.value) - 1;
    }

    //communication
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

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + "seconds");
        serial.Close();
    }

}