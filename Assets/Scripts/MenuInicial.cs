using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private GameObject panelControls;

    [SerializeField] private GameObject buttonPlay;
    [SerializeField] private GameObject buttonControls;
    [SerializeField] private GameObject buttonExit;
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Controls()
    {
        panelControls.SetActive(true);

        buttonPlay.SetActive(false);
        buttonControls.SetActive(false);
        buttonExit.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }

    public void ButtonExit()
    {
        panelControls.SetActive(false);

        buttonPlay.SetActive(true);
        buttonControls.SetActive(true);
        buttonExit.SetActive(true);
    }
}
