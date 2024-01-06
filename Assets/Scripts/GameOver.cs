using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void MenuHome(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false; // Remove line when deployed
        Application.Quit();
    }
    public void ActivateGameOver()
    {
        menuGameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
