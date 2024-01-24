using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    [SerializeField] float angularSpeed = 2f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        MoveInCircularPath();
    }

    void MoveInCircularPath()
    {
        float angle = Time.time * angularSpeed;

        float x = initialPosition.x + radius * Mathf.Cos(angle);
        float y = initialPosition.y + radius * Mathf.Sin(angle);

        transform.position = new Vector3(x, y, initialPosition.z);
    }
}
