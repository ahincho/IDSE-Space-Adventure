using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    [SerializeField] private float points;
    [SerializeField] private Score score;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("spaceship"))
        {
            score.subtractScore(points);
            Destroy(gameObject);
        }

    }
}
