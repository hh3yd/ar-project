using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text counterText;
    
    public float timeLeft;


    public float score;

    private GameObject panel;
    private bool over = false;

    // Use this for initialization
    void Start()
    {
        panel = GameObject.Find("GameOverPanel");
        panel.SetActive(false);
        counterText = GetComponent<Text>() as Text;        
        setCounterText();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        setCounterText();
        if (timeLeft < 0)
        {
            timeLeft = 0;
            if (!over)
            {
                over = true;
                panel.SetActive(true);                
            }
        }
    }

    private void setCounterText()
    {
        float minutes = (int)(timeLeft / 60f);
        float seconds = (int)(timeLeft % 60f);
        counterText.text = "Time left: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
