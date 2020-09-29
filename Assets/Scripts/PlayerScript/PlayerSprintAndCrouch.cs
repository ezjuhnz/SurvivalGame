using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour {


    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;

    private PlayerMovement playerMovement;
    private Transform lootRoot;
    private float stand_Heigh = 1.6f;
    private float crouch_Heigh = 1f;
    private bool is_Crouching;

    private PlayerFootAudio player_FootAudio;
    private float sprint_Volumn = 1f;
    private float crouch_Volumn = 0.1f;
    private float walk_Volumn_Min = 0.2f, walk_Volumn_Max = 0.6f;
    private float sprint_Step_Distance = 0.25f;
    private float walk_Step_Distance = 0.4f;
    private float crouch_Step_Distance = 0.5f;

    private PlayerState player_State;
    private float energy = 100f;
    private float energy_Consume_Persecond = 10f;
    private float energy_Restore_Persecond = 5f;
    private float currentEnergy;
    private float energy_Threhold = 10f;
    void Awake () {
        playerMovement = this.GetComponent<PlayerMovement>();
        lootRoot = this.transform.GetChild(0);
        player_FootAudio = this.GetComponentInChildren<PlayerFootAudio>();
        player_State = GetComponent<PlayerState>();
    }
	
	// Update is called once per frame
	void Update () {
        Sprint();
        Crouch();
    }

    void Start()
    {
        player_FootAudio.volumn_Min = walk_Volumn_Min;
        player_FootAudio.volumn_Max = walk_Volumn_Max;
        player_FootAudio.step_Distance = walk_Step_Distance;
        currentEnergy = energy;
    }

    void Sprint()
    {
        if(currentEnergy > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching)
            {
                playerMovement.moveSpeed = sprint_Speed;

                player_FootAudio.step_Distance = sprint_Step_Distance;
                player_FootAudio.volumn_Min = sprint_Volumn;
                player_FootAudio.volumn_Max = sprint_Volumn;
                //
                currentEnergy -= energy_Consume_Persecond * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftShift) && !is_Crouching)
            {
                playerMovement.moveSpeed = sprint_Speed;

                player_FootAudio.step_Distance = sprint_Step_Distance;
                player_FootAudio.volumn_Min = sprint_Volumn;
                player_FootAudio.volumn_Max = sprint_Volumn;
                //
                currentEnergy -= energy_Consume_Persecond * Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching)
            {
                playerMovement.moveSpeed = move_Speed;

                player_FootAudio.step_Distance = walk_Step_Distance;
                player_FootAudio.volumn_Min = walk_Volumn_Min;
                player_FootAudio.volumn_Max = walk_Volumn_Max;

                currentEnergy += energy_Restore_Persecond * Time.deltaTime;
            }
        }
        else
        {
            playerMovement.moveSpeed = move_Speed;

            player_FootAudio.step_Distance = walk_Step_Distance;
            player_FootAudio.volumn_Min = walk_Volumn_Min;
            player_FootAudio.volumn_Max = walk_Volumn_Max;
        }
        //restore energy 
        if(!Input.GetKey(KeyCode.LeftShift) && currentEnergy < energy)
        {
            playerMovement.moveSpeed = move_Speed;

            player_FootAudio.step_Distance = walk_Step_Distance;
            player_FootAudio.volumn_Min = walk_Volumn_Min;
            player_FootAudio.volumn_Max = walk_Volumn_Max;
            currentEnergy += energy_Restore_Persecond * Time.deltaTime;
        }
        currentEnergy = (currentEnergy < 0) ? 0 : currentEnergy;
        currentEnergy = (currentEnergy > energy) ? energy : currentEnergy;

        player_State.Display_EnergyState(currentEnergy);
    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(is_Crouching)
            {
                is_Crouching = false;
                lootRoot.localPosition = new Vector3(0, stand_Heigh, 0);
                playerMovement.moveSpeed = move_Speed;

                player_FootAudio.step_Distance = walk_Step_Distance;
                player_FootAudio.volumn_Min = walk_Volumn_Min;
                player_FootAudio.volumn_Max = walk_Volumn_Max;
            }
            else
            {
                is_Crouching = true;
                lootRoot.localPosition = new Vector3(0, crouch_Heigh, 0);
                playerMovement.moveSpeed = crouch_Speed;

                player_FootAudio.step_Distance = crouch_Step_Distance;
                player_FootAudio.volumn_Min = crouch_Volumn;
                player_FootAudio.volumn_Max = crouch_Volumn;
            }
        }
    }
}
