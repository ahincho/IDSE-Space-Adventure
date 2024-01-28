using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float underBoundary = -5f;
    [SerializeField] float topBoundary = 5f;

    private float initialYPosition;
    void Start()
    {
        initialYPosition = transform.position.y;
    }

    void Update()
    {
        MoveAutomatically();
    }

    void MoveAutomatically()
    {
        Vector3 movement = new Vector3(0f, speed, 0f) * Time.deltaTime;

        transform.Translate(movement);

        if (transform.position.y < initialYPosition + underBoundary || transform.position.y > initialYPosition + topBoundary)
        {
            speed = -speed;
        }
    }
}
