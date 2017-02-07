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


                //dir.Normalize();
                //Debug.Log(old_dir);
                //Debug.Log(dir);
                //Quaternion rotation = Quaternion.Euler(dir);
                //Debug.Log(q);
                //movementObject2.rotation = q;
                
                /*Vector3 proj_xy = Vector3.ProjectOnPlane(dir, Vector3.forward);
                float drehung_z = Vector3.Angle(Vector3.right, proj_xy);

                Vector3 proj_xz = Vector3.ProjectOnPlane(dir, Vector3.up);
                float drehung_y = Vector3.Angle(Vector3.forward, proj_xz);

                Vector3 proj_yz = Vector3.ProjectOnPlane(dir, Vector3.right);
                float drehung_x = Vector3.Angle(Vector3.up, proj_yz);

                float y_angle = Vector3.Angle(dir, movementObject1.position);
                float x_angle = Vector3.Angle(dir, movementObject1.position);
                float z_angle = Vector3.Angle(dir, movementObject1.position);
                
                //dir.z = -dir.z;
                float x_angle = Vector3.Angle(Vector3.up, dir);
                float y_angle = Vector3.Angle(Vector3.right, dir);
                float z_angle = Vector3.Angle(Vector3.forward, dir);
                //Rotation um die Z-Achse ist die 
                //Winkel zur XAchse von der Dir
                */
                //Quaternion q2 = new Quaternion(drehung_x, drehung_y, drehung_z, 0);
                //Vector3 pos_2_vec = new Vector3(pos_2.X, pos_2.Y, pos_2.Z) * posFactor;
                //Vector3 targetDir = pos_2_vec - movementObject1.position;
                //Quaternion rotation = Quaternion.LookRotation(targetDir);

                //rotation *= movementObject1.rotation;

                //float step = posFactor * Time.deltaTime;
                //Vector3 newDir = Vector3.RotateTowards(movementObject1.position, targetDir, step, 0.0F);
                //Debug.DrawRay(transform.position, newDir, Color.red);
                //movementObject1.rotation = Quaternion.LookRotation(newDir);


                break;
            }
        }
        
    }

    void OnApplicationQuit()
    {
    }
}
