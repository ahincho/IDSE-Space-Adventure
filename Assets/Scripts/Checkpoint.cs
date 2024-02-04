using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private AudioSource audioSource;

    private bool activated = false;
    private float dialogueDuration = 2f;

    [SerializeField] private FuelController fuel;
    [SerializeField] private float amount;
    [SerializeField] private GameObject dialogue;


    public void ActivateCheckpoint()
    {
        if (!activated)
        {
            activated = true;
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            fuel.Restore(amount);
            StartCoroutine(ShowDialogueForDuration());
            
        }
    }

    private IEnumerator ShowDialogueForDuration()
    {
        dialogue.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        dialogue.SetActive(false);
        gameObject.SetActive(false);
    }

    public bool CheckpointActive() { return activated; }
}
