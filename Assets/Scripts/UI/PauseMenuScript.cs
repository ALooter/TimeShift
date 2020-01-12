using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
