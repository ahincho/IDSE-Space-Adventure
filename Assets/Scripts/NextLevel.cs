using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
    public void ActivateMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
    }
}
