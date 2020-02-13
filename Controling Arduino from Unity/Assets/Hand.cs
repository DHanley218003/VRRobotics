using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Interactable m_Currentinteractable = null;
    public List<Interactable> m_Contactinteractables = new List<Interactable>();
    // Start is called before the first frame update
    void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_Contactinteractables.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_Contactinteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        m_Currentinteractable = GetNearestinteractable();

        if (!m_Currentinteractable)
            return;

        if (m_Currentinteractable.m_ActiveHand)
            m_Currentinteractable.m_ActiveHand.Drop();

        m_Currentinteractable.transform.position = transform.position;

        Rigidbody targetBody = m_Currentinteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        m_Currentinteractable.m_ActiveHand = this;
    }

    public void Drop()
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
