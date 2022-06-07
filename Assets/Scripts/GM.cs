using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static GM instance;

    private bool pauseState = false;

    public bool debugMode = true;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Restart()
    {
        UnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pauseState = true;
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
        pauseState = false;
    }
    public bool GetPausedState()
    {
        return pauseState;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
