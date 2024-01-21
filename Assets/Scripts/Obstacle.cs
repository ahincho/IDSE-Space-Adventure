using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    [SerializeField] private float points;
    [SerializeField] private Score score;

    AudioSource audiosource;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("spaceship"))
        {
            score.subtractScore(points);
            Destroy(gameObject);
        }
  
    }
}
