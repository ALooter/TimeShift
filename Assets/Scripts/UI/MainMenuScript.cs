using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Button opencontrols;
    public Button exitbut;
    public Button startbut;
    public int controlsisopen = 0;
    public Image controlsimg;

    private void Start()
    {
        
        controlsisopen = 0;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void Controls()
    {
        if (controlsisopen == 0)
        {
            controlsimg.gameObject.SetActive(true);
            startbut.gameObject.SetActive(false);
            exitbut.gameObject.SetActive(false);
            controlsisopen = 1;
        }
        else
        {
            controlsimg.gameObject.SetActive(false);
            startbut.gameObject.SetActive(true);
            exitbut.gameObject.SetActive(true);
            controlsisopen = 0;
        }
    }
}
