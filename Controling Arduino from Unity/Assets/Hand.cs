using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;
    public SteamVR_Action_Vibration hapticAction = null;
    public SteamVR_Action_Boolean m_TouchPadUp = null;
    public SteamVR_Action_Boolean m_TouchPadDown = null;
    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;
    public GameObject IKTarget;
    public GameObject IKTargetPrefab;
    private Interactable m_Currentinteractable = null;
    public List<Interactable> m_Contactinteractables = new List<Interactable>();
    public GameObject robotArm = null;
    public List<GameObject> gameObjects = new List<GameObject>();

    void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
        m_GrabAction.AddOnStateDownListener(Pickup, m_Pose.inputSource);
        m_GrabAction.AddOnStateUpListener(Drop, m_Pose.inputSource);
        m_TouchPadUp.AddOnStateUpListener(SpawnIKTarget, m_Pose.inputSource);
        m_TouchPadDown.AddOnStateUpListener(DeleteIKTarget, m_Pose.inputSource);
        gameObjects.Add(IKTarget);
    }

   /* void Update()
    {
        // Down
        if(m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            Pickup();
        }
        // Up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            Drop();
        }
    }*/

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

    public void SpawnIKTarget(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        gameObjects.Add(Instantiate(IKTargetPrefab as GameObject));
        gameObjects[gameObjects.Count - 1].transform.parent = gameObjects[gameObjects.Count - 2].transform;
        gameObjects[gameObjects.Count - 1].transform.position = gameObjects[gameObjects.Count - 2].transform.position + new Vector3(0.1f,0.1f,0.1f);
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

}
