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
    private float accelerationForce = 5f;
    private float decelerationForce = 8f;
    private float tiltSpeed = 50f; // Velocidad de inclinaci�n
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

    // Update is called once per frame
    void Update()
    {
        // print("Hola...");
        //Debug.Log(Time.deltaTime + "seg. " + (1.0f / Time.deltaTime) + "FPS");
        ProcesarInput();
    }
    private void ProcesarInput()
    {
        Rotacion();
        Acelerar();
        Desacelerar();
        Propulsion();
        Inclinacion();
        EstabilizarInclinacion();
    }
    private void Propulsion()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.freezeRotation = true;
            //print("Propulsor ...");
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

    private void Acelerar()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Aplicar fuerza hacia adelante en la direcci�n global
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

    private void Desacelerar()
    {
        if (Input.GetKey(KeyCode.S))
        {
            // Aplicar una fuerza contraria al movimiento actual para desacelerar
            rigidbody.AddForce(-rigidbody.velocity.normalized * decelerationForce, ForceMode.Acceleration);
        }
    }
    private void Rotacion()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //print("Rotar Derecha...");
            //transform.Rotate(Vector3.back);
            var rotarDerecha = transform.rotation;
            rotarDerecha.z -= Time.deltaTime * 1;
            transform.rotation = rotarDerecha;
            rigidbody.AddForce(transform.right * lateralForce, ForceMode.Acceleration);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            //print("Rotar Izquierda...");
            //transform.Rotate(Vector3.forward);
            var rotarIzquierda = transform.rotation;
            rotarIzquierda.z += Time.deltaTime * 1;
            transform.rotation = rotarIzquierda;
            rigidbody.AddForce(-transform.right * lateralForce, ForceMode.Acceleration);
        }
    }
    private void Inclinacion()
    {
        if (Input.GetKey(KeyCode.E))
        {
            // Inclinar a la derecha
            transform.Rotate(Vector3.right, Time.deltaTime * tiltSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            // Inclinar a la izquierda
            transform.Rotate(Vector3.left, Time.deltaTime * tiltSpeed);
        }
    }

    private void EstabilizarInclinacion()
    {
        // Si no se presiona ninguna tecla de inclinaci�n, estabilizar la inclinaci�n
        if (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            // Ajusta la inclinaci�n a cero
            Quaternion targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * stabilizationSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ColisionSegura":
                print("OK...");
                break;
            case "ColisionPeligrosa":
                print("CHOQUE!!!");
                break;

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
            //audioSourceCollision.Play();
        }

    }

    //RigidBody
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GatilloSeguro"))
        {
            print("Gatillo seguro...");
        }
        else if (other.CompareTag("GatilloPeligroso"))
        {
            print("Gatillo peligroso...");
        }
    }
    */

}
