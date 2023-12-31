using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour
{
    private float maxFuel = 100f;
    private float fuel;
    private Text fuelText;

    void Start()
    {
        fuel = maxFuel; // Inicializar el combustible al 100%
        fuelText = GetComponent<Text>();
    }

    void Update()
    {

        // Mostrar el combustible como porcentaje
        fuelText.text = ((fuel / maxFuel) * 100f).ToString("0") + "%";

        fuelText.color = Color.green;


  
        if (fuel <= maxFuel * 0.50f) // Si está en el 50%
        {
            fuelText.color = Color.yellow;
        }
        if (fuel <= maxFuel * 0.25f) // Si está en el 25%
        {
            fuelText.color = Color.red;
        }
       
    }
    public void UseFuel(float amount)
    {
        fuel -= amount;
        fuel = Mathf.Clamp(fuel, 0f, maxFuel); // Asegurar que el combustible no sea menor que 0 ni mayor que maxFuel
    }
}
