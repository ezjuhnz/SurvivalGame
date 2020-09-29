using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootAudio : MonoBehaviour {

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private AudioSource footstep_Sound;

    private CharacterController character_controller;
    [HideInInspector]
    public float volumn_Min, volumn_Max;
    private float accumulated_Distance;
    [HideInInspector]
    public float step_Distance;


	void Awake () {
        footstep_Sound = GetComponent<AudioSource>();
        character_controller = GetComponentInParent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        if(!character_controller.isGrounded)
        {
            return;
        }
        if(character_controller.velocity.sqrMagnitude > 0)
        {
            accumulated_Distance += Time.deltaTime;
            if(accumulated_Distance > step_Distance)
            {
                footstep_Sound.volume = Random.Range(volumn_Min, volumn_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();

                accumulated_Distance = 0f;
            }
        }
        else
        {
            accumulated_Distance = 0f;
        }
    }
}
