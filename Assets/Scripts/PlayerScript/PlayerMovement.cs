using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 5f;
    public float gravity = 10f;
    private float vertical_speed = 0f;
    public float jump_force = 10;

    private CharacterController characterController;
    private Vector3 move_direction = Vector3.zero;
	void Start () {
        characterController = this.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        move_direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0, Input.GetAxis(Axis.VERTICAL));
        move_direction = transform.TransformDirection(move_direction);
        move_direction *= moveSpeed * Time.deltaTime;
        //
        ApplyGravity();
        //Debug.Log("y=" + move_direction.y);
        characterController.Move(move_direction);
    }

    void ApplyGravity()
    {
        vertical_speed -= gravity * Time.deltaTime;
        if (characterController.isGrounded)
        {  
            Jump();
        }
        move_direction.y = vertical_speed * Time.deltaTime;
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            vertical_speed = jump_force;
        }
    }
}
