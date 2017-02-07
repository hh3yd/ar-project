using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text counterText;
    
    public float timeLeft;
    // Use this for initialization
    void Start () {
        setCounterText();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        setCounterText();
        if (timeLeft < 0)
        {
            timeLeft = 0;
            // TODO: Game over ?
        }
    }

    private void setCounterText()
    {
        float minutes = (int)(timeLeft / 60f);
        float seconds = (int)(timeLeft % 60f);
        counterText.text = "Time left: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
