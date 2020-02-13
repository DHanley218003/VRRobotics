using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Threading;

public class ControlArduino : MonoBehaviour
{
    public float Map(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }

    SerialPort serial;
    public string myString;
    public float comRapidity = 1.0f;
    public string portName;
    float temps = 0.0f;
    float delay = 0.0f;
    bool setPort = true;

    public int servoDegre1; //Degré value
    public Transform servo1;
    public Slider sliderServo1;
    string A;

    public int servoDegre2; //Degré value
    public Transform servo2;
    public Slider sliderServo2;
    string B;

    public int servoDegre3; //Degré value
    public Transform servo3;
    public Slider sliderServo3;
    string C;

    public int servoDegre4; //Degré value
    public Transform servo4;
    public Slider sliderServo4;
    string D;

    public int servoDegre5; //Degré value
    public Transform servo5;
    public Slider sliderServo5;
    string E;

    public int servoDegre6; //Degré value
    public Transform servo6;
    public Transform servo62;
    public Slider sliderServo6;
    string F;


    // Start is called before the first frame update
    void Start()
    {
        serial = new SerialPort();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        servo1.localRotation = Quaternion.AngleAxis(-servoDegre1, Vector3.back);
        D = servoDegre1.ToString("000");

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
        servo2.localRotation = Quaternion.AngleAxis(servoDegre2, Vector3.right);
        A = servoDegre2.ToString("000");

        if (servoDegre3 != (sliderServo3.value))
        {
            if (servoDegre3 < (sliderServo3.value))
            {
                servoDegre3 = servoDegre3 + 1;
            }

            if (servoDegre3 > (sliderServo3.value))
            {
                servoDegre3 = servoDegre3 - 1;
            }
        }
        servo3.localRotation = Quaternion.AngleAxis(servoDegre3, Vector3.right);
        B = servoDegre3.ToString("000");

        if (servoDegre4 != (sliderServo4.value))
        {
            if (servoDegre4 < (sliderServo4.value))
            {
                servoDegre4 = servoDegre4 + 1;
            }

            if (servoDegre4 > (sliderServo4.value))
            {
                servoDegre4 = servoDegre4 - 1;
            }
        }
        servo4.localRotation = Quaternion.AngleAxis(servoDegre4, Vector3.right);
        C = servoDegre4.ToString("000");

        if (servoDegre5 != (sliderServo5.value))
        {
            if (servoDegre5 < (sliderServo5.value))
            {
                servoDegre5 = servoDegre5 + 1;
            }
            if (servoDegre5 > (sliderServo5.value))
            {
                servoDegre5 = servoDegre5 - 1;
            }
        }
        servo5.localRotation = Quaternion.Euler(240f, 0f, servoDegre5);
        E = servoDegre5.ToString("000");

        if (servoDegre6 != (sliderServo6.value))
        {
            if (servoDegre6 < (sliderServo6.value))
            {
                servoDegre6 = servoDegre6 + 1;
            }
            if (servoDegre6 > (sliderServo6.value))
            {
                servoDegre6 = servoDegre6 - 1;
            }
        }
        servo6.localPosition = new Vector3(Map(0f,0.44f,0f,180f,servoDegre6),0,2);
        servo62.localPosition = new Vector3(-Map(0f, 0.44f, 0f, 180f, servoDegre6), 0, 2);
        F = servoDegre6.ToString("000");

        myString = string.Concat(A, B, C, D, E, F);

        /*temps = Time.time;
        if ((delay + comRapidity) < temps)
        {
            Envoyer();
            delay = temps;
            //Debug.Log (myString);
        }*/
        Envoyer();

    }
    public void Envoyer()
    {
        if (setPort == true)
        {
            serial.PortName = portName;
            serial.Parity = Parity.None;
            serial.BaudRate = 115200;
            serial.DataBits = 8;
            serial.StopBits = StopBits.One;
            serial.Open();
            setPort = false;
        }
        serial.Write(myString + "\n");

    }
}
