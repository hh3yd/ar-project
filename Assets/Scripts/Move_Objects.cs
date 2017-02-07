using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Move_Objects : MonoBehaviour {
    //update is called before the frame is 
    public float mSpeed;
    private int count;
    public Text countText;
    void Start()
    {   
        count = 0;
        SetCountText();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool w= Input.GetKey("w");
        if(w)
        {
            transform.Rotate(Vector3.up);
        }
        bool a = Input.GetKey("a");
        if (a)
        {
            transform.Rotate(Vector3.left);
        }
        
        //rb.AddForce(mv * mSpeed);
        transform.Translate(mSpeed*moveHorizontal * Time.deltaTime, 0.0f,mSpeed* moveVertical * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {/*
            Renderer r = other.GetComponent<Renderer>();
            Color col = r.material.color.linear;

            col.r = r.material.color.r+1.0f;
            r.material.SetColor("test",col);*/
            //r.material.shader.
            count++;
            SetCountText();
            if (count == 5)
            {

                other.gameObject.SetActive(false);
                //show win panel
            }
            
        }
    }
    private void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }

}
