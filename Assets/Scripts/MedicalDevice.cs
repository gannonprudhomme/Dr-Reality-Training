using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ValveVR = Valve.VR.InteractionSystem;

[RequireComponent(typeof(ValveVR.Interactable))]
public class MedicalDevice : MonoBehaviour {
    public GameObject Human;

    private ValveVR.Hand.AttachmentFlags attachmentFlags = ValveVR.Hand.defaultAttachmentFlags & (~ValveVR.Hand.AttachmentFlags.SnapOnAttach) & (~ValveVR.Hand.AttachmentFlags.DetachOthers) & (~ValveVR.Hand.AttachmentFlags.VelocityMovement);

    private ValveVR.Interactable interactable;

    private void Awake()
    {
        this.interactable = this.GetComponent<ValveVR.Interactable>();
    }


    // Update is called once per frame
    void Update () {
		
	}

    //-------------------------------------------------
    // Called when a Hand starts hovering over this object
    //-------------------------------------------------
    private void OnHandHoverBegin(ValveVR.Hand hand)
    {
        //textMesh.text = "Hovering hand: " + hand.name;
    }


    //-------------------------------------------------
    // Called when a Hand stops hovering over this object
    //-------------------------------------------------
    private void OnHandHoverEnd(ValveVR.Hand hand)
    {
        //ValveVR.textMesh.text = "No Hand Hovering";
    }


    //-------------------------------------------------
    // Called every Update() while a Hand is hovering over this object
    //-------------------------------------------------
    private void HandHoverUpdate(ValveVR.Hand hand)
    {
        ValveVR.GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && startingGrabType != ValveVR.GrabTypes.None)
        {
            // Save our position/rotation so that we can restore it when we detach
            //oldPosition = transform.position;
            //oldRotation = transform.rotation;

            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attach this object to the hand
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
        else if (isGrabEnding)
        {
            // Detach this object from the hand
            hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);

            // Restore position/rotation
            //transform.position = oldPosition;
            //transform.rotation = oldRotation;
        }
    }


    //-------------------------------------------------
    // Called when this GameObject becomes attached to the hand
    //-------------------------------------------------
    private void OnAttachedToHand(ValveVR.Hand hand)
    {
    }


    //-------------------------------------------------
    // Called when this GameObject is detached from the hand
    //-------------------------------------------------
    private void OnDetachedFromHand(ValveVR.Hand hand)
    {

    }


    //-------------------------------------------------
    // Called every Update() while this GameObject is attached to the hand
    //-------------------------------------------------
    private void HandAttachedUpdate(ValveVR.Hand hand)
    {
        //textMesh.text = "Attached to hand: " + hand.name + "\nAttached time: " + (Time.time - attachTime).ToString("F2");
    }


    //-------------------------------------------------
    // Called when this attached GameObject becomes the primary attached object
    //-------------------------------------------------
    private void OnHandFocusAcquired(ValveVR.Hand hand)
    {
        Debug.Log("Hand Focus Acquired");
    }


    //-------------------------------------------------
    // Called when another attached GameObject becomes the primary attached object
    //-------------------------------------------------
    private void OnHandFocusLost(ValveVR.Hand hand)
    {
        Debug.Log("Hand Focus Lost");
    }
}
    