using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject credits;
    [SerializeField] private int gameBuildNumber;

    private void Start()
    {
        main.SetActive(true);
        credits.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Confined;
        Screen.fullScreen = true;
        QualitySettings.vSyncCount = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        main.SetActive(true);
        credits.SetActive(false);
    }

    public void Credits()
    {
        main.SetActive(false);
        credits.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync(gameBuildNumber);
    }
}
