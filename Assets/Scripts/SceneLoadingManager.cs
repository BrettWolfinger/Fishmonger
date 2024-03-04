using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public void LoadTutorialScreen()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadBuyScreen()
    {
        SceneManager.LoadScene("BuyMenu");
    }
    
    public void StartDay()
    {
        SceneManager.LoadScene("GameplayScreen");
    }

    public void EndDay()
    {
        SceneManager.LoadScene("PostGameplay");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void HowToPlayTwo()
    {
        SceneManager.LoadScene("HowToPlayTwo");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
