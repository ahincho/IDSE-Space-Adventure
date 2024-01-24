using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private bool activated = false;

    public void ActivateCheckpoint()
    {
        if (!activated)
        {
            activated = true;
            gameObject.SetActive(false);
        }

    }

    public Boolean CheckpointActive() { return activated; }
}
