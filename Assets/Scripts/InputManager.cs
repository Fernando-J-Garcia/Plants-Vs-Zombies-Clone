using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UI.instance.ToggleStartMenu();
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (GM.instance.debugMode)
                Debug.Log("foo");
            {
                PlayerStats.Instance.Money += 500;
            }
        }
    }
}
