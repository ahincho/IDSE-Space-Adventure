using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f; // Horizontal movement speed
    [SerializeField] float leftBoundary = -5f;
    [SerializeField] float rightBoundary = 5f;

    private float initialXPosition;
    void Start()
    {
        initialXPosition = transform.position.x;
    }

    void Update()
    {
        MoveAutomatically();
    }
    void MoveAutomatically()
    {
        Vector3 movement = new Vector3(speed, 0f, 0f) * Time.deltaTime;

        transform.Translate(movement);

        if (transform.position.x < leftBoundary || transform.position.x > rightBoundary)
        {
            speed = -speed;
        }
    }
}
