using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    [SerializeField] private GameObject menuGamePause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateGamePause();
        }
    }
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
    public void Continue()
    {
        Time.timeScale = 1f;
        menuGamePause.SetActive(false);
    }
    public void ActivateGamePause()
    {
        menuGamePause.SetActive(true);
        Time.timeScale = 0f;
    }
}
