using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toggle : MonoBehaviour, IHittable
{
    public Sprite toggleOn;
    public Sprite toggleOff;
    public AudioClip hitSound;
    // we should hide this because we do not want other developers
    // attempting to connect this Unity Event in the editor
    [HideInInspector]
    public UnityEvent<bool> OnToggle;

    bool toggleState = false;
    SpriteRenderer spriteRenderer;
    Animator animator;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();//getting the audio, sprites and animator
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (OnToggle == null)
        {
            OnToggle = new UnityEvent<bool>();
        }


    }

    void UpdateState()//updates the state the button is on depending on if it is hit
    {
        spriteRenderer.sprite = toggleState ? toggleOn : toggleOff;
        animator.SetTrigger("StartHit");
    }

    public void Hit(GameObject gameObject) //if the button is hit by gameobject change the state and play a sound
    {
        toggleState = !toggleState;
        UpdateState();
  

        audioSource.PlayOneShot(hitSound); // Play the sound immediately once

        OnToggle.Invoke(toggleState);
    }
}
