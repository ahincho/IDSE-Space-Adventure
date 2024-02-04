using System.Collections.Generic;
using UnityEngine;

public class ControlDeNave : MonoBehaviour
{

    new Rigidbody rigidbody;
    new Transform transform;
    public AudioSource audiosourcePropulsion;
    public AudioSource audiosource2;
    public AudioSource audioSourceCollision;
    public AudioSource audioSourceLevelCompleted;
    private float accelerationForce = 5f;
    private float decelerationForce = 8f;
    private float tiltSpeed = 50f;
    private float stabilizationSpeed = 5f;
    private float lateralForce = 8f;

    [SerializeField] private NextLevel nextLevel;

    private FuelController fuelController;
    private List<Checkpoint> checkpoints = new List<Checkpoint>();

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        fuelController = FindObjectOfType<FuelController>();
        checkpoints.AddRange(FindObjectsOfType<Checkpoint>());
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

            if (!audiosourcePropulsion.isPlaying)
            {
                audiosourcePropulsion.Play();
            }
            fuelController.UseFuel(Time.deltaTime);
        }
        else
        {
            audiosourcePropulsion.Stop();
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
            transform.Rotate(Vector3.up, Time.deltaTime * tiltSpeed);
            rigidbody.AddForce(transform.right * lateralForce, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -Time.deltaTime * tiltSpeed);
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
            
            int totalActivatedCheckpoints = GetTotalActivatedCheckpoints();
            audioSourceLevelCompleted.Play();
            print(totalActivatedCheckpoints);
            if (totalActivatedCheckpoints == checkpoints.Count)
            {
                nextLevel.ActivateMenu();
            }

        }
        if (other.CompareTag("checkpoint"))
        {
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();

            if (!checkpoint.CheckpointActive())
            {
                checkpoint.ActivateCheckpoint();
            }
        }

        if (other.CompareTag("Obstacle"))
        {
            print("Sonido");
            audioSourceCollision.Play();
        }

    }
    private int GetTotalActivatedCheckpoints()
    {
        int totalActivatedCheckpoints = 0;

        foreach (var checkpoint in checkpoints)
        {
            if (checkpoint.CheckpointActive())
            {
                totalActivatedCheckpoints++;
            }
        }

        return totalActivatedCheckpoints;


    }
}
