using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
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
    public string myString;
    public string portName = "COM4";
    bool antiLag = true;
    public bool robotIsConnected = false;
    // I think the lag is caused by a USB buffer overflow

    private void Awake()
    {
        if(robotIsConnected)
            OpenSerialPort();
        m_TouchPadLeft.AddOnStateUpListener(PrevIKTarget, m_Pose.inputSource);
        m_TouchPadRight.AddOnStateUpListener(NextIKTarget, m_Pose.inputSource);
    }

    private void FixedUpdate()
    {
        // Updates 25 times a second instead of 50, to reduce lag
        if (antiLag)
        {
            MoveArm(angles);
            antiLag = false;
        }
        else
            antiLag = true;
    }

    private void Update()
    {
        target = targetObject.transform.position;
        InverseKinematics(target, angles);
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
        serial.Write(myString + "\n");
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
            angles[i] = Mathf.Clamp(angles[i], Joints[i].MinAngle, Joints[i].MaxAngle);

            // Early termination
            if (DistanceFromTarget(target, angles) < DistanceThreshold)
                return;
        }
    }

    private void MoveArm(float[] angles)
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
        if(robotIsConnected)
            SendData();
    }
}
