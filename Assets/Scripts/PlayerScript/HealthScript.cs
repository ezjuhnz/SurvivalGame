using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour {

    private EnermyAnimator enermy_Anim;
    private NavMeshAgent navAgent;
    private EnermyController enermy_Controller;

    public float health = 100f;
    public bool is_Player, is_Boar, is_Cannibal;
    private bool is_Dead;

    
    private EnermyAudio enermyAudio;
    private PlayerState playerState;
	void Awake () {
        
        if (is_Boar || is_Cannibal)
        {
            enermy_Anim = GetComponent<EnermyAnimator>();
            enermy_Controller = GetComponent<EnermyController>();
            navAgent = GetComponent<NavMeshAgent>();

            enermyAudio = GetComponentInChildren<EnermyAudio>();
            
        }
        if(is_Player)
        {
            playerState = GetComponent<PlayerState>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyDamage(float damage)
    {
        
        if (is_Dead) return;
        health -= damage;
        if(is_Player)
        {
            playerState.Display_HealthState(health);
        }
        if(is_Boar || is_Cannibal)
        {
            enermy_Anim.GetHit();
            if (enermy_Controller.Enermy_State == EnermyState.PATROL)
            {
                enermy_Controller.chase_Distance = 50f;
            }
        }
        if (health <= 0)
        {
            TargetDie();
            is_Dead = true;
        }
    }

    //不只是玩家
     void TargetDie()
    {
        if(is_Cannibal)
        {
            //GetComponent<Animator>().enabled = false;
            //GetComponent<CapsuleCollider>().isTrigger = false;
           
            enermy_Controller.enabled = false;
            navAgent.enabled = false;
            enermy_Anim.enabled = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 5);

            //play sound
            StartCoroutine("DeadSound");
            EnermyManager._instance.EnermyDie(true);
        }
        if(is_Boar)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enermy_Controller.enabled = false;

            enermy_Anim.Dead();
            //enermy_Anim.enabled = false;
            StartCoroutine("DeadSound");
            EnermyManager._instance.EnermyDie(false);
        }
        if(is_Player)
        {
            GameObject[] enermies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            for(int i = 0; i< enermies.Length; i++)
            {
                enermies[i].GetComponent<EnermyController>().enabled = false;
            }

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentWeapon().gameObject.SetActive(false);

            EnermyManager._instance.StopSpawnEnermies();
        }

        if(tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObejct", 3f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void TurnOffGameObejct()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enermyAudio.Play_DeadSound();
    }
}
