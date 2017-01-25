using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.IO;
public class Movement_Tracker : MonoBehaviour {
    public Transform movementObject1;
    public Transform movementObject2;
    public Windows.Kinect.JointType partToTrack1;
    public Windows.Kinect.JointType partToTrack2;
    private float posFactor = 10;
    BodySourceManager bodySourceManager;
    StreamWriter sw;
    // Use this for initialization
    void Start () {
        
        bodySourceManager = GetComponent<BodySourceManager>();
        sw = new StreamWriter(@"D:\text.txt");

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
                var pos = body.Joints[partToTrack1].Position;
                movementObject1.position = new Vector3(pos.X,pos.Y,-pos.Z)*posFactor;

                sw.Write("Left: ");
                sw.WriteLine(movementObject1.position.ToString());

                pos = body.Joints[partToTrack2].Position;
                movementObject2.position = new Vector3(pos.X, pos.Y, -pos.Z)*posFactor;

                sw.Write("Right: ");
                sw.WriteLine(movementObject2.position.ToString());

                break;
            }
        }
        
    }
    void OnApplicationQuit()
    {
        if (sw != null)
        {
            sw.Close();
        }
    }
}
