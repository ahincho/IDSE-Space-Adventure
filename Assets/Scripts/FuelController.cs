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
    [SerializeField] private GameOver menuGameOver;

    void Start()
    {
        fuel = maxFuel; // Initialize fuel to 100%
        fuelText = GetComponent<Text>();
    }

    void Update()
    {

        // Show fuel as a percentage
        fuelText.text = ((fuel / maxFuel) * 100f).ToString("0") + "%";

        fuelText.color = Color.green;


  
        if (fuel <= maxFuel * 0.50f)
        {
            fuelText.color = Color.yellow;
        }
        if (fuel <= maxFuel * 0.25f)
        {
            fuelText.color = Color.red;
        }
       
    }
    public void UseFuel(float amount)
    {
        fuel -= amount;
        fuel = Mathf.Clamp(fuel, 0f, maxFuel); // Ensure fuel is not less than 0 or greater than maxFuel
        if (fuel <= 0)
        {
            menuGameOver.ActivateGameOver();
        }
    }
}
