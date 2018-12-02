using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ValveVR = Valve.VR.InteractionSystem;

[RequireComponent(typeof(MedicalDevice))]
public class MainAirbag : MonoBehaviour {
    public MedicalDevice goal;
    public float MAX_DIFF;
    public TextMesh textMesh;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDetachedFromHand(ValveVR.Hand hand)
    {
        float goal_x = goal.transform.position.x;
        float goal_y = goal.transform.position.y;
        float goal_z = goal.transform.position.z;


        if(Mathf.Abs(transform.position.x - goal_x) < MAX_DIFF)
        {
            if (Mathf.Abs(transform.position.y - goal_y) < MAX_DIFF)
            {
                if (Mathf.Abs(transform.position.z - goal_z) < MAX_DIFF)
                {
                    textMesh.text = "Good!";
                }
            }
        } else
        {
            textMesh.text = "";
        }
    }
}
