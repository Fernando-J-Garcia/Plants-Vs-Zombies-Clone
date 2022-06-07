using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class is responsible for displaying the main ui of the game
/// </summary>
public class UI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI popUpText;
    [SerializeField]
    TextMeshProUGUI MoneyText;
    [SerializeField]
    Texture2D shovelCursorTexture;
    [SerializeField]
    private GameObject pauseMenu;

    public static UI instance;    

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("There is 2 UI Sctipts in the scene");
            return;
        }
        instance = this;

        UpdateMoneyText();
        popUpText.gameObject.SetActive(false);
    }
    public void CreateNewPopUp(string text)
    {
        popUpText.text = text;
        StartCoroutine(AnimatePopUpText());
    }
    IEnumerator AnimatePopUpText()
    {
        if (popUpText.gameObject.activeSelf) { yield break; }

        popUpText.gameObject.SetActive(true);

        int dir;

        Color c = popUpText.color;
        c.a = 1f;
        popUpText.transform.localScale = new Vector3(1,1,0);

        Vector3 scale;
        int repitions = 40;
        ///Transition the text to be visible on the screen then at the halfway 
        ///point tranistion back to invisible
        for (int i = 0; i < repitions; i++)
        {
            dir = i % 20 < (repitions / 4) ? 1 : -1;
            float fadeDir = 0.01f * dir;

            //c.a += fadeDir;
            scale = new Vector3(fadeDir, fadeDir, 0);

            popUpText.transform.localScale += scale;
            popUpText.color = c;
            yield return new WaitForSeconds(0.025f);
        }

        popUpText.gameObject.SetActive(false);

    }
    public void SetDefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void SetShovelCursor()
    {
        Cursor.SetCursor(shovelCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void UpdateMoneyText()
    {
        MoneyText.text = "$" + PlayerStats.Instance.Money;
    }
    public void ToggleStartMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf) GM.instance.Pause();
        else GM.instance.UnPause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) CreateNewPopUp("Test");
        if (Input.GetKey(KeyCode.A)) CreateNewPopUp("Hello");
    }
}
