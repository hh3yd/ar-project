using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScoreScript : MonoBehaviour {

    public Text endScoreText; 

    private void Start()
    {
        endScoreText = GetComponent<Text>() as Text;
    }

    // Update is called once per frame
    void Update () {
        endScoreText.text = "Your score: " + PlayerPrefs.GetInt("CurrentScore");
    }
}
