﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Valve.VR;
using System.Data;
using UnityEngine.Events;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

public class Hand : MonoBehaviour
{
	public SteamVR_Action_Boolean m_GrabAction = null;
	public SteamVR_Action_Vibration hapticAction = null;
	public SteamVR_Action_Boolean m_TouchPadUp = null;
	public SteamVR_Action_Boolean m_TouchPadDown = null;
	public SteamVR_Action_Boolean m_TouchPadLeft = null;
	public SteamVR_Action_Boolean m_TouchPadRight = null;
	private SteamVR_Behaviour_Pose m_Pose = null;
	private FixedJoint m_Joint = null;
	public GameObject IKTarget;
	public GameObject IKTargetPrefab;
	public GameObject VR_Controller;
	private Interactable m_Currentinteractable = null;
	public List<Interactable> m_Contactinteractables = new List<Interactable>();
	public GameObject robotArm = null;
	public List<GameObject> gameObjects = new List<GameObject>();
	public bool movingForward = true;
	public bool robotIsConnected = false;
	public int childObject = 0;
	public System.Net.IPAddress hostIP = IPAddress.Parse("192.168.1.9");
	public int port = 30003;
	public Socket TCPPort = null;
	public string commandString = null;
	public int scaler = 1; // Scales the co-ordinates, in this case changes meters to mm.
	public float acceleration = 1.2f;
	public float velocity = 0.25f;

	void Awake()
	{
		m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
		m_Joint = GetComponent<FixedJoint>();
		m_GrabAction.AddOnStateDownListener(Pickup, m_Pose.inputSource);
		m_GrabAction.AddOnStateUpListener(Drop, m_Pose.inputSource);
		m_TouchPadUp.AddOnStateUpListener(DPadUp, m_Pose.inputSource);
		m_TouchPadDown.AddOnStateUpListener(DPadDown, m_Pose.inputSource);
		m_TouchPadLeft.AddOnStateUpListener(DPadLeft, m_Pose.inputSource);
		m_TouchPadRight.AddOnStateUpListener(DPadRight, m_Pose.inputSource);
		gameObjects.Add(IKTarget);
		if(robotIsConnected)
		{
			try
			{
				TCPPort = new Socket(AddressFamily.InterNetwork,
					SocketType.Stream,
					ProtocolType.Tcp);
				TCPPort.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
				TCPPort.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
								TCPPort.Connect(hostIP, port);
			}
			catch (Exception e)
			{ }
		}
	}

	private void OnDestroy()
	{
		m_GrabAction.RemoveOnStateDownListener(Pickup, m_Pose.inputSource);
		m_GrabAction.RemoveOnStateUpListener(Drop, m_Pose.inputSource);
		m_TouchPadUp.RemoveOnStateUpListener(DPadUp, m_Pose.inputSource);
		m_TouchPadDown.RemoveOnStateUpListener(DPadDown, m_Pose.inputSource);
		m_TouchPadLeft.RemoveOnStateUpListener(DPadLeft, m_Pose.inputSource);
		m_TouchPadRight.RemoveOnStateUpListener(DPadRight, m_Pose.inputSource);
		if (robotIsConnected)
			TCPPort.Close();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Interactable"))
			return;

		Pulse(0.01f, 200, 15, SteamVR_Input_Sources.Any);
		m_Contactinteractables.Add(other.gameObject.GetComponent<Interactable>());
	}

	private void OnTriggerExit(Collider other)
	{
		if (!other.gameObject.CompareTag("Interactable"))
			return;

		m_Contactinteractables.Remove(other.gameObject.GetComponent<Interactable>());
	}

	private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
	{
		hapticAction.Execute(0, duration, frequency, amplitude, source);
	}

	public void DPadUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		if (fromSource == SteamVR_Input_Sources.RightHand)
		{
			SpawnIKTarget(fromAction, fromSource);
		}
		else
		{
			JogMove();
		}
	}

	public void DPadDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		if (fromSource == SteamVR_Input_Sources.RightHand)
		{
			DeleteIKTarget(fromAction, fromSource);
		}
		else
		{
			ParabolaMove();
		}
	}

	public void DPadLeft(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		if (fromSource == SteamVR_Input_Sources.RightHand)
		{
			LastPoint();
		}
		else
		{
			ChangeDirection();
		}
	}

	public void DPadRight(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		if (fromSource == SteamVR_Input_Sources.RightHand)
		{
			NextPoint();
		}
		else
		{
			LinearMove();
		}
	}

	public void JogMove()
	{
		// clear the string
		commandString = "movej(p[";
		RobotMove();
	}

	public void LinearMove()
	{
		// clear the string
		commandString = "movel(p[";
		RobotMove();
	}

	public void ParabolaMove()
	{
		// clear the string
		commandString = "movep(p[";
		RobotMove();
	}

	public void RobotMove()
	{
		if (movingForward)
		{
			if(NextPoint())
			{
				commandString += IKTarget.transform.position.x * scaler + ", " + IKTarget.transform.position.z * scaler + ", " + IKTarget.transform.position.y * scaler + ", "
				+ ConvertDegreesToRadians(IKTarget.transform.rotation.x) + ", " + ConvertDegreesToRadians(IKTarget.transform.rotation.y) + ", " + ConvertDegreesToRadians(IKTarget.transform.rotation.z);
				commandString += "], a=" + acceleration + ", v=" + velocity + ")";
			}
			if (robotIsConnected)
				TCPPort.Send(Encoding.ASCII.GetBytes(commandString + "\n"));
		}
		else
		{
			if(LastPoint())
			{
				commandString += IKTarget.transform.position.x * scaler + ", " + IKTarget.transform.position.z * scaler + ", " + IKTarget.transform.position.y * scaler + ", "
				+ ConvertDegreesToRadians(IKTarget.transform.rotation.x) + ", " + ConvertDegreesToRadians(IKTarget.transform.rotation.y) + ", " + ConvertDegreesToRadians(IKTarget.transform.rotation.z);
				commandString += "], a=" + acceleration + ", v=" + velocity + ")";
			}
			if (robotIsConnected)
				TCPPort.Send(Encoding.ASCII.GetBytes(commandString + "\n"));
		}
		
	}

	public void ChangeDirection()
	{
		movingForward = !movingForward;
	}

	public bool NextPoint()
	{
		if (IKTarget.transform.childCount > 0)
		{
			IKTarget = IKTarget.transform.GetChild(0).gameObject;
			childObject++;
			return true;
		}
		else
			return false;
	}

	public bool LastPoint()
	{
		if (IKTarget.transform.parent != null)
		{
			IKTarget = IKTarget.transform.parent.gameObject;
			childObject--;
			return true;
		}
		else
			return false;
	}
	public void SpawnIKTarget(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		gameObjects.Add(Instantiate(IKTargetPrefab as GameObject));
		gameObjects[gameObjects.Count - 1].transform.parent = gameObjects[gameObjects.Count - 2].transform;
		gameObjects[gameObjects.Count - 1].transform.position = VR_Controller.transform.position;
	}

	public void DeleteIKTarget(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		if (gameObjects.Count > 1)
		{
			robotArm.GetComponent<IKManager>().PrevIKTarget(fromAction, fromSource);
			Destroy(gameObjects[gameObjects.Count - 1].gameObject);
			gameObjects.RemoveAt(gameObjects.Count-1);
		}
	}

	public void Pickup(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		m_Currentinteractable = GetNearestinteractable();

		if (!m_Currentinteractable)
			return;

		if (m_Currentinteractable.m_ActiveHand)
			m_Currentinteractable.m_ActiveHand.Drop(fromAction, fromSource);

		m_Currentinteractable.transform.position = transform.position;

		Rigidbody targetBody = m_Currentinteractable.GetComponent<Rigidbody>();
		m_Joint.connectedBody = targetBody;

		m_Currentinteractable.m_ActiveHand = this;
	}

	public void Drop(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		if (!m_Currentinteractable)
			return;
		m_Joint.connectedBody = null;

		m_Currentinteractable.m_ActiveHand = null;
		m_Currentinteractable = null;
	}

	private Interactable GetNearestinteractable()
	{
		Interactable nearest = null;
		float minDistance = float.MaxValue;
		float distance = 0.0f;

		foreach(Interactable interactable in m_Contactinteractables)
		{
			distance = (interactable.transform.position - transform.position).sqrMagnitude;
			if(distance < minDistance)
			{
				minDistance = distance;
				nearest = interactable;
			}
		}
		return nearest;
	}
	public static double ConvertDegreesToRadians(double degrees)
	{
		double radians = (Math.PI / 180) * degrees;
		return (radians);
	}
	public static double ConvertRadiansToDegrees(double radians)
	{
		double degrees = radians * 180 / Math.PI;
		return (degrees);
	}
}
