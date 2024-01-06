using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    private float score = 100f;
    private Text scoreText;
    [SerializeField] private GameOver menuGameOver;
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }
    public void subtractScore(float scoreEntry)
    {
        score -= scoreEntry;
        scoreText.text = score.ToString("0");
        if (score <= 0)
        {
            menuGameOver.ActivateGameOver();
        }
    }


}
