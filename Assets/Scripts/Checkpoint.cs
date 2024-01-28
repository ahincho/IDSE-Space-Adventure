using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private bool activated = false;
    [SerializeField] private FuelController fuel;

    public void ActivateCheckpoint()
    {
        if (!activated)
        {
            activated = true;
            fuel.Restore();
            gameObject.SetActive(false);
        }

    }

    public Boolean CheckpointActive() { return activated; }
}
