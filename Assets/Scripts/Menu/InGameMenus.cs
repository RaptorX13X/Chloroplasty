using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject lossScreen;
    [SerializeField] private int menuIndex;

    public static InGameMenus instance;

    private void Awake()
    {
        instance = this;
        gameScreen.SetActive(true);
        lossScreen.SetActive(false);
    }

    public void OnLoss()
    {
        lossScreen.SetActive(true);
        gameScreen.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(menuIndex);
    }
    
    
}
