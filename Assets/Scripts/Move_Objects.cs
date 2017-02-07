using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Move_Objects : MonoBehaviour {
    //update is called before the frame is 
    public float mSpeed;
    private int count;
    public Text countText;
    private GameObject panel;
    void Start()
    {
        panel = GameObject.Find("GameOverPanel");
        panel.SetActive(false);
        PlayerPrefs.SetString("timer", "true");
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
        
        transform.Translate(mSpeed*moveHorizontal * Time.deltaTime, 0.0f,mSpeed* moveVertical * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            count++;
            SetCountText();
            if (count == 5)
            {
                
                //show win panel
                panel.SetActive(true);
                PlayerPrefs.SetString("timer", "false");
                GameObject.Find("axe3").SetActive(false);
            }
            
        }
    }
    private void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        PlayerPrefs.SetInt("CurrentScore", count);
    }

}
