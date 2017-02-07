using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartButtonOnClick : MonoBehaviour
{

    public void LoadStage()
    {
        //Application.LoadLevel("Score");
        //PlayerPrefs.SetString("timer", "true");
        SceneManager.LoadScene("DestroyTower");
    }
}
