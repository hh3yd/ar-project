using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.IO;
public class Movement_Tracker : MonoBehaviour {
    public Transform movementObject1;
    //public Transform movementObject2;
    public Windows.Kinect.JointType partToTrack1;
    public Windows.Kinect.JointType partToTrack2;
    private float posFactor = 10;
    BodySourceManager bodySourceManager;
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
                var pos_elbow = body.Joints[partToTrack1].Position;
                movementObject1.position = new Vector3(pos_elbow.X, pos_elbow.Y,-pos_elbow.Z)*posFactor;
                
                //sw.Write("Left: ");
                //sw.WriteLine(movementObject1.position.ToString());

                var pos_hand = body.Joints[partToTrack2].Position;
                Vector3 pos_hand_vec = new Vector3(pos_hand.X, pos_hand.Y, -pos_hand.Z) * posFactor;
                Vector3 targetDir = pos_hand_vec - movementObject1.position;
                float step = posFactor * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(movementObject1.position, targetDir, step, 0.0F);
                Debug.DrawRay(transform.position, newDir, Color.red);
                movementObject1.rotation = Quaternion.LookRotation(newDir);
                break;
            }
        }
        
    }
    void OnApplicationQuit()
    {
        /*if (sw != null)
        {
            sw.Close();
        }*/
    }
}
