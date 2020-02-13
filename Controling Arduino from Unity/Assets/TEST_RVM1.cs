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
    
    public string portName;
    
    bool setPort = true;

    public int servoDegre1; //Degré value
    public Transform servo1;
    public Button buttonServo1;
    string A;
    public int servoDegre2; //Degré value
    public Transform servo2;
    public Button buttonServo2;
    string B;
    public float updateInterval = 0.1f;
 


   
    // Start is called before the first frame update
    void Start()
    {
        serial = new SerialPort();
        serial.PortName = "COM4";
        serial.Parity = Parity.None;
        serial.BaudRate = 9600;
        serial.DataBits = 8;
        serial.StopBits = StopBits.One;
        serial.Open();
        setPort = false;
        InvokeRepeating("UpdateInterval", updateInterval, updateInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    void UpdateInterval()
    {
        buttonServo1.onClick.AddListener(TaskOnClick);
        buttonServo2.onClick.AddListener(TaskOnClick2);
        
        //use this as the secondary update.
    }
    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
        {
            //A = servoDegre1.ToString("NT");
            //myString = string.Concat(A);
            myString = "GC";
            Envoyer();
            

        }
    }
    void TaskOnClick2()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
        {
            // B = servoDegre2.ToString("MO 1");
            //myString = string.Concat(B);
            myString = "NT";
            Envoyer();
            
        }
    }
   /* public void Envoyer()
    {
        if (setPort == true)
        {
            serial.PortName = "COM3";
            serial.Parity = Parity.None;
            serial.BaudRate = 9600;
            serial.DataBits = 8;
            serial.StopBits = StopBits.One;
            serial.Open();
            setPort = false;
        }
        serial.Write(myString + "\n\r");
        
    }
    */
    public void Envoyer()
    {
        
        serial.Write(myString + "\n\r");
        
    }
}
