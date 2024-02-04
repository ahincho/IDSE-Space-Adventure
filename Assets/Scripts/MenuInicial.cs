using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private GameObject panelControls;
    [SerializeField] private GameObject panelCredits;

    [SerializeField] private GameObject buttonPlay;
    [SerializeField] private GameObject buttonControls;
    [SerializeField] private GameObject buttonCredits;
    [SerializeField] private GameObject buttonExit;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Controls()
    {
        panelControls.SetActive(true);
        SetButtonsActive(false);
    }

    public void Exit()
    {
        // Debug.Log("Exit...");
        Application.Quit();
    }

    public void ButtonExit()
    {
        panelControls.SetActive(false);
        panelCredits.SetActive(false);
        SetButtonsActive(true);
    }

    public void ButtonCredits()
    {
        panelCredits.SetActive(true);
        SetButtonsActive(false);
    }
    private void SetButtonsActive(bool active)
    {
        buttonPlay.SetActive(active);
        buttonControls.SetActive(active);
        buttonCredits.SetActive(active);
        buttonExit.SetActive(active);
    }
}
