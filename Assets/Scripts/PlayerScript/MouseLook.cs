using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    [SerializeField]
    public Transform playerTransfrom, rootTransform;

    [SerializeField]
    public bool invert;

    
    public float sensity = 5f;

    [SerializeField]
    private float smooth_weigh = 0.4f;
    [SerializeField]
    private float roll_angle = 10f;
    [SerializeField]
    private float roll_speed = 5f;

    [SerializeField]
    private Vector2 default_look_limits = new Vector2(-70f, 80f);

    private Vector2 look_angles;
    private Vector2 current_mouse_look;
    private Vector2 smooth_move;
    private float current_roll_angle;
    private int last_look_frame;
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        LockAndUnlockCursor();
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void LockAndUnlockCursor()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround()
    {
        current_mouse_look = new Vector2(
            Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));
       
        look_angles.x += current_mouse_look.x * sensity * (invert ? 1f : -1f);
        look_angles.y += current_mouse_look.y * sensity;

        look_angles.x = Mathf.Clamp(look_angles.x, default_look_limits.x, default_look_limits.y);

        
        rootTransform.localRotation = Quaternion.Euler(look_angles.x, 0, 0);
        playerTransfrom.localRotation = Quaternion.Euler(0f, look_angles.y, 0f);
    }
}
