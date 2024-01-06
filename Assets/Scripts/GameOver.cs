using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        SceneManagement.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MenuHome(string name)
    {
        SceneManager.Load(name);
    }
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false; // Remove line when deployed
        Application.Quit();
    }
}
