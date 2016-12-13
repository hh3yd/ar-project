using UnityEngine;
using System.Collections;

public class Move_Objects : MonoBehaviour {
    //update is called before the frame is 
    private float mSpeed;
    void Start()
    {
        mSpeed = 1f;
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        transform.Translate(mSpeed*moveHorizontal * Time.deltaTime, 0.0f,mSpeed* moveVertical * Time.deltaTime);
    }
}
