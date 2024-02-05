using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlDeNave : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _transform;

    public AudioSource propulsionAudio;
    public AudioSource engineAudio;
    public AudioSource collisionAudio;
    public AudioSource levelCompletedAudio;

    private bool canMove = false;

    [SerializeField] private float accelerationForce = 5f;
    [SerializeField] private float decelerationForce = 8f;
    [SerializeField] private float tiltSpeed = 50f;
    [SerializeField] private float stabilizationSpeed = 5f;
    [SerializeField] private float lateralForce = 8f;

    [SerializeField] private NextLevel nextLevel;

    private FuelController fuelController;
    private List<Checkpoint> checkpoints = new List<Checkpoint>();

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        fuelController = FindObjectOfType<FuelController>();
        checkpoints.AddRange(FindObjectsOfType<Checkpoint>());
    }

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (canMove)
        {
            Accelerate();
            Decelerate();
            Rotate();
            Propulsion();
            Tilt();
            Stabilize();
        }
    }

    private void Propulsion()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.freezeRotation = true;
            _rigidbody.AddRelativeForce(Vector3.up);
            if (!engineAudio.isPlaying)
            {
                engineAudio.Play();
            }
            fuelController.UseFuel(Time.deltaTime);
        }
        else
        {
            engineAudio.Stop();
            _rigidbody.freezeRotation = false;
        }
    }

    private void Accelerate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(_transform.forward * accelerationForce, ForceMode.Acceleration);
            if (!propulsionAudio.isPlaying)
            {
                propulsionAudio.Play();
            }
            fuelController.UseFuel(Time.deltaTime);
        }
        else
        {
            propulsionAudio.Stop();
        }
    }

    private void Decelerate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(-_rigidbody.velocity.normalized * decelerationForce, ForceMode.Acceleration);
        }
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _transform.Rotate(Vector3.up, Time.deltaTime * tiltSpeed);
            _rigidbody.AddForce(_transform.right * lateralForce, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _transform.Rotate(Vector3.up, -Time.deltaTime * tiltSpeed);
            _rigidbody.AddForce(-_transform.right * lateralForce, ForceMode.Acceleration);
        }
    }

    private void Tilt()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _transform.Rotate(Vector3.right, Time.deltaTime * tiltSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _transform.Rotate(Vector3.left, Time.deltaTime * tiltSpeed);
        }
    }

    private void Stabilize()
    {
        if (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            Quaternion targetRotation = Quaternion.Euler(0f, _transform.rotation.eulerAngles.y, 0f);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * stabilizationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("platformEnd"))
        {
            int totalActivatedCheckpoints = GetTotalActivatedCheckpoints();
            levelCompletedAudio.Play();
            Debug.Log(totalActivatedCheckpoints);
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
            Debug.Log("Sound");
            collisionAudio.Play();
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
    public void setCanMove(bool c)
    {
        canMove = c;
    }
    public bool getCanMove(bool canMove)
    {
        return canMove;
    }
}
