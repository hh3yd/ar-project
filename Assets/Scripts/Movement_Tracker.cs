using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.IO;
using System;

public class Movement_Tracker : MonoBehaviour {
    public Transform movementObject1;
    public Windows.Kinect.JointType partToTrack1;
    public Windows.Kinect.JointType partToTrack2;
    BodySourceManager bodySourceManager;

    // Use this for initialization
    void Start () {
        
        bodySourceManager = GetComponent<BodySourceManager>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate()
    {
            if (bodySourceManager == null)
            {
                return;
            }

        Windows.Kinect.Body[] data = bodySourceManager.GetData();
        
        if (data == null)
        {
            return;
        }
        
        //bei mehreren Personen
        foreach (var body in data)
        {

            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                var pos_1 = body.Joints[partToTrack1].Position;
                Vector3 left_Hand = new Vector3(pos_1.X, pos_1.Y, -pos_1.Z);
                //movementObject1.position = pos_1_vec;

                var pos_2 = body.Joints[partToTrack2].Position;
                Vector3 right_Hand = new Vector3(pos_2.X, pos_2.Y, -pos_2.Z);
                Vector3 dir = right_Hand - left_Hand;
                Quaternion q = Quaternion.LookRotation(dir);
                movementObject1.rotation = q;
                movementObject1.position = right_Hand*30;
                
                break;
            }
        }
        
    }

    void OnApplicationQuit()
    {
    }
}
