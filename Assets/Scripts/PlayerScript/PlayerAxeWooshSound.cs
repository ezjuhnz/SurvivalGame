using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeWooshSound : MonoBehaviour {

    private AudioSource axe_Audiosource;

    [SerializeField]
    private AudioClip[] clips;

    void Awake()
    {
        axe_Audiosource = GetComponent<AudioSource>();
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayWooshSound()
    {
        axe_Audiosource.clip = (clips[Random.Range(0, clips.Length)]);
        axe_Audiosource.Play();
    }
}
