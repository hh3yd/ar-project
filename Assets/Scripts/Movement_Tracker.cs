using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.IO;
using System;

public class Movement_Tracker : MonoBehaviour {
    public Transform movementObject1;
    public Transform movementObject2;
    public Windows.Kinect.JointType partToTrack1;
    public Windows.Kinect.JointType partToTrack2;
    public float lowPassFactor = 0.1F;
    private float posFactor = 20;
    BodySourceManager bodySourceManager;
    private Vector3 currentPos1;
    private Vector3 currentPos2;
    //StreamWriter sw;
    // Use this for initialization
    void Start () {
        
        bodySourceManager = GetComponent<BodySourceManager>();
        //sw = new StreamWriter(@"D:\text.txt");

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

        int counter = 0;

        //bei mehreren Personen
        foreach (var body in data)
        {
            counter++;

            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                var pos_1 = body.Joints[partToTrack1].Position;
                CalcLowPassValues(new Vector3(pos_1.X, pos_1.Y, -pos_1.Z), ref currentPos1);
                Vector3 pos_1_vec = currentPos1 * posFactor;
                movementObject1.position = pos_1_vec;
                
                //sw.Write("Left: ");
                //sw.WriteLine(movementObject1.position.ToString());

                var pos_2 = body.Joints[partToTrack2].Position;
                CalcLowPassValues(new Vector3(pos_2.X, pos_2.Y, -pos_2.Z), ref currentPos2);
                Vector3 pos_2_vec = currentPos2 * posFactor;
                Vector3 dir = pos_2_vec - pos_1_vec;
                //dir.Normalize();
                Vector3 old_dir = movementObject2.rotation.eulerAngles;
                //Debug.Log(old_dir);
                //Debug.Log(dir);
                Quaternion q = Quaternion.FromToRotation(old_dir, dir);
                //Quaternion rotation = Quaternion.Euler(dir);
                //Debug.Log(q);
                //movementObject2.rotation = q;
                

                //Vector3 pos_2_vec = new Vector3(pos_2.X, pos_2.Y, pos_2.Z) * posFactor;
                //Vector3 targetDir = pos_2_vec - movementObject1.position;
                //Quaternion rotation = Quaternion.LookRotation(targetDir);

                //rotation *= movementObject1.rotation;
                
                //float step = posFactor * Time.deltaTime;
                //Vector3 newDir = Vector3.RotateTowards(movementObject1.position, targetDir, step, 0.0F);
                //Debug.DrawRay(transform.position, newDir, Color.red);
                //movementObject1.rotation = Quaternion.LookRotation(newDir);
                movementObject1.rotation = q;
                
                break;
            }
        }
        
    }

    private void CalcLowPassValues(Vector3 newPos, ref Vector3 resultPos)
    {
        resultPos = Vector3.Lerp(resultPos, newPos, lowPassFactor);
    }

    void OnApplicationQuit()
    {
        /*if (sw != null)
        {
            sw.Close();
        }*/
    }
}
