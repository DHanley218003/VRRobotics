using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEditor;
using UnityEngine;
using Valve.VR;

public class IKManager : MonoBehaviour
{
    public SteamVR_Behaviour_Pose m_Pose = null;
    public SteamVR_Action_Boolean m_TouchPadLeft = null;
    public SteamVR_Action_Boolean m_TouchPadRight = null;
    public RobotJoint[] Joints;
    public int childObject = 0;
    public float SamplingDistance;
    public float DistanceThreshold;
    public float LearningRate;
    public Vector3 target;
    public GameObject targetObject;
    public float[] angles;
    SerialPort serial;
    Socket TCPPort;
    public string myString;
    public string portName = "COM4";
    public System.Net.IPAddress host = IPAddress.Parse("192.168.1.8");
    public int port = 30004;
    public bool robotIsServo = false;
    public bool robotIsConnected = false;
    private Byte[] buffer = new Byte[256];

    private void Start()
    {
        InvokeRepeating("SendData", 0.0f, 0.8f);
    }
    private void Awake()
    {
        if (robotIsConnected)
            if (robotIsServo)
                OpenSerialPort();
            else
                OpenTCPPort();
        m_TouchPadLeft.AddOnStateUpListener(PrevIKTarget, m_Pose.inputSource);
        m_TouchPadRight.AddOnStateUpListener(NextIKTarget, m_Pose.inputSource);
    }
    private void OnDestroy()
    {
        m_TouchPadLeft.RemoveOnStateUpListener(PrevIKTarget, m_Pose.inputSource);
        m_TouchPadRight.RemoveOnStateUpListener(NextIKTarget, m_Pose.inputSource);
        if (robotIsConnected)
            if (robotIsServo)
                serial.Close();
            else
                TCPPort.Close();
    }

    private void FixedUpdate()
    {
        if (robotIsServo)
        {
            MoveServoArm();
        }
        else
        {
            MoveRobotArm();
        }
    }

    private void Update()
    {
        target = targetObject.transform.position;
        for (int i = 0; i < 1000; i++)
        {
            InverseKinematics(target, angles);
        }
    }

    public void PrevIKTarget(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (targetObject.transform.parent != null)
        {
            targetObject = targetObject.transform.parent.gameObject;
            childObject--;
        }
    }

    public void NextIKTarget(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (targetObject.transform.childCount > 0)
        {
            targetObject = targetObject.transform.GetChild(0).gameObject;
            childObject++;
        }
    }

    private void OpenTCPPort()
    {
        TCPPort = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

        TCPPort.Connect(host, port);
    }
    private void OpenSerialPort()
    {
        serial = new SerialPort
        {
            PortName = portName,
            Parity = Parity.None,
            BaudRate = 2000000,
            DataBits = 8,
            StopBits = StopBits.One,
            ReadTimeout = 20,
            WriteTimeout = 20 // Used to counter lag.
        };
        serial.Open();
    }

    private void SendData()
    {
        if (robotIsServo)
        {
            serial.Write(myString + "\n");
        }
        else
        {
            TCPPort.Send(Encoding.ASCII.GetBytes(myString + "\n"));
            TCPPort.Receive(buffer);
        }
    }

    private Vector3 ForwardKinematics(float[] angles)
    {
        Vector3 prevPoint = Joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < Joints.Length; i++)
        {
            // Rotates around a new axis
            rotation *= Quaternion.AngleAxis(angles[i - 1], Joints[i - 1].Axis);
            Vector3 nextPoint = prevPoint + rotation * Joints[i].StartOffset;

            prevPoint = nextPoint;
        }
        return prevPoint;
    }

    private float DistanceFromTarget(Vector3 target, float[] angles)
    {
        Vector3 point = ForwardKinematics(angles);
        return Vector3.Distance(point,target);
    }

    private float PartialGradient(Vector3 target, float[] angles, int i)
    {
        // Saves the angle,
        // it will be restored later
        float angle = angles[i];

        // Gradient : [F(x+SamplingDistance) - F(x)] / h
        float f_x = DistanceFromTarget(target, angles);

        angles[i] += SamplingDistance;
        float f_x_plus_d = DistanceFromTarget(target, angles);

        float gradient = (f_x_plus_d - f_x) / SamplingDistance;

        // Restores
        angles[i] = angle;

        return gradient;
    }

    private void InverseKinematics(Vector3 target, float[] angles)
    {
        if (DistanceFromTarget(target, angles) < DistanceThreshold)
            return;

        for (int i = Joints.Length -1; i >= 0; i--)
        {
            // Gradient descent
            float gradient = PartialGradient(target, angles, i);
            angles[i] -= LearningRate * gradient;

            // Clamp angles to account for servo limits
            // angles[i] = Mathf.Clamp(angles[i], Joints[i].MinAngle, Joints[i].MaxAngle);

            // Early termination
            if (DistanceFromTarget(target, angles) < DistanceThreshold)
                return;
        }
    }

    private void MoveRobotArm()
    {
        // clear the string
        myString = "movej([";
        for (int i = 0; i < Joints.Length; i++)
        {
            if (i > 0)
                myString += ", ";
            // The angles are muiltiplied with the axis variable, so only the moving axis actually moves. The other angles are set to 0 as a result.
            Joints[i].transform.localEulerAngles = new Vector3(angles[i] * Joints[i].Axis.x, angles[i] * Joints[i].Axis.y, angles[i] * Joints[i].Axis.z);
            myString += ConvertDegreesToRadians(angles[i]);
        }
        myString += "])";
        //"movej([-0.5405182705025187, -2.350330184112267, -1.316631037266588, -2.2775736604458237, 3.3528323423665642, -1.2291967454894914], a = 1.3962634015954636, v = 1.0471975511965976)"
    }

    private void MoveServoArm()
    {
        // clear the string
        myString = "";
        for (int i = 0; i < Joints.Length; i++)
        {
            // The angles are muiltiplied with the axis variable, so only the moving axis actually moves. The other angles are set to 0 as a result.
            Joints[i].transform.localEulerAngles = new Vector3(angles[i] * Joints[i].Axis.x, angles[i] * Joints[i].Axis.y, angles[i] * Joints[i].Axis.z);
            // Divides by 180 to convert to a number between 0 and 1, then takes away one to invert the scaling and multiplies by 180 to get the inverse scaled angle
            // In other words, this changes 0-180 to 180-0. This is needed since the model is inverted to the robot.
            myString += (Math.Round(-((angles[i] / 180) - 1) * 180)).ToString("000");
        }
    }
    public static double ConvertDegreesToRadians(double degrees)
    {
        double radians = (Math.PI / 180) * degrees;
        return (radians);
    }
}
