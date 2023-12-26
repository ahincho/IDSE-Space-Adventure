using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeNave : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    Transform transform;
    AudioSource audiosource;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audiosource = GetComponent<AudioSource>();
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
        Propulsion();
        Rotacion();
        
    }

    private void Propulsion()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.freezeRotation = true;
            //print("Propulsor ...");
            rigidbody.AddRelativeForce(Vector3.up);

            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
        }
        else
        {
            audiosource.Stop();
        }
        rigidbody.freezeRotation = false;
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
        }

        else if (Input.GetKey(KeyCode.A))
        {
            //print("Rotar Izquierda...");
            //transform.Rotate(Vector3.forward);
            var rotarIzquierda = transform.rotation;
            rotarIzquierda.z += Time.deltaTime * 1;
            transform.rotation = rotarIzquierda;
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

    //Static
    /*

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerSeguro"))
        {
            print("Entrada segura...");
        }
        else if (other.CompareTag("TriggerPeligroso"))
        {
            print("Entrada peligrosa...");
        }
    }
    */

    //RigidBody
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
    
}
