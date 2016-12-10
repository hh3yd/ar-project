using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Movement_Tracker : MonoBehaviour {
    public Transform movementObject1;
    public Transform movementObject2;
    public Windows.Kinect.JointType partToTrack1;
    public Windows.Kinect.JointType partToTrack2;
    public float posFactor = 2;
    public float lowPassFactor = 0.1F;
    private Vector3 currentPos1;
    private Vector3 currentPos2;
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
        if(bodySourceManager == null)
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
                CalcLowPassValues(new Vector3(pos.X, pos.Y, -pos.Z), ref currentPos1);

                movementObject1.position = currentPos1 * posFactor;

                pos = body.Joints[partToTrack2].Position;
                CalcLowPassValues(new Vector3(pos.X, pos.Y, -pos.Z), ref currentPos2);

                movementObject2.position = currentPos2 * posFactor;

                break;
            }
        }
    }

    void CalcLowPassValues(Vector3 newPos, ref Vector3 resultPos)
    {
        resultPos = Vector3.Lerp(resultPos, newPos, lowPassFactor);
    }
}
