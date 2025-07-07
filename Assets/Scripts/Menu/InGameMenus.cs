using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject lossScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private int menuIndex;
    [SerializeField] private int nextIndex;

    public static InGameMenus instance;

    private void Awake()
    {
        instance = this;
        gameScreen.SetActive(true);
        lossScreen.SetActive(false);
        endScreen.SetActive(false);
    }

    public void OnLoss()
    {
        lossScreen.SetActive(true);
        gameScreen.SetActive(false);
        endScreen.SetActive(false);
    }

    public void OnEnd()
    {
        endScreen.SetActive(true);
        lossScreen.SetActive(false);
        gameScreen.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(menuIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(nextIndex);
    }
    
    
}
