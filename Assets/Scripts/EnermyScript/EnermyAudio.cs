﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAudio : MonoBehaviour {
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip scream_Clip, die_Clip;

    [SerializeField]
    private AudioClip[] attack_Clips;

	void Awake () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play_ScreamSound()
    {
        audioSource.clip = scream_Clip;
        audioSource.Play();
    }

    public void Play_AttackSound()
    {
        audioSource.clip = attack_Clips[Random.Range(0, attack_Clips.Length)];
        audioSource.Play();
    }

    public void Play_DeadSound()
    {
        audioSource.clip = die_Clip;
        audioSource.Play();
    }
}
