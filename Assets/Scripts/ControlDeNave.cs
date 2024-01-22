using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ControlDeNave : MonoBehaviour
{

    new Rigidbody rigidbody;
    new Transform transform;
    public AudioSource audiosource;
    public AudioSource audiosource2;
    public AudioSource audioSourceCollision;
    private float accelerationForce = 5f; 
    private float decelerationForce = 8f;
    private float tiltSpeed = 50f;
    private float stabilizationSpeed = 5f;
    private float lateralForce = 8f;

    [SerializeField] private NextLevel nextLevel;

    private FuelController fuelController;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        fuelController = FindObjectOfType<FuelController>();
    }

    void Update()
    {
        ProcesarInput();
    }
    private void ProcesarInput()
    {
        Accelerate();
        Decelerate();
        Rotation();
        Propulsion();
        Tilt();
        Stabilize();
    }
    // Handles the propulsion mechanics for a spaceship
    private void Propulsion()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.freezeRotation = true;
            rigidbody.AddRelativeForce(Vector3.up);
            if (!audiosource2.isPlaying)
            {
                audiosource2.Play();
            }
            fuelController.UseFuel(Time.deltaTime);
        }
        else
        {
            audiosource2.Stop();
        }
        rigidbody.freezeRotation = false;
    }

    private void Accelerate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(transform.forward * accelerationForce, ForceMode.Acceleration);
        
            if (!audiosource.isPlaying)
            {
                audiosource.Play(); 
            }
            fuelController.UseFuel(Time.deltaTime);
        }
        else
        {
            audiosource.Stop();
        }
    }

    private void Decelerate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(-rigidbody.velocity.normalized * decelerationForce, ForceMode.Acceleration);
        }
    }
    private void Rotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            var rotateRight = transform.rotation;
            rotateRight.z -= Time.deltaTime * 1;
            transform.rotation = rotateRight;
            rigidbody.AddForce(transform.right * lateralForce, ForceMode.Acceleration);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            var rotateLeft = transform.rotation;
            rotateLeft.z += Time.deltaTime * 1;
            transform.rotation = rotateLeft;
            rigidbody.AddForce(-transform.right * lateralForce, ForceMode.Acceleration);
        }
    }
    private void Tilt()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.right, Time.deltaTime * tiltSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.left, Time.deltaTime * tiltSpeed);
        }
    }

    private void Stabilize()
    {
        if (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            Quaternion targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * stabilizationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("platformEnd"))
        {
            nextLevel.ActivateMenu();
        }
        if (other.CompareTag("Obstacle"))
        {
            print("Sonido");
            audioSourceCollision.Play();
        }

    }

}
