using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    [SerializeField] private float points;
    [SerializeField] private float amount;
    [SerializeField] private Score score;
    [SerializeField] private FuelController fuel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("spaceship"))
        {
            fuel.UseFuel(amount);
            score.subtractScore(points);
            Destroy(gameObject);
        }
  
    }
}
