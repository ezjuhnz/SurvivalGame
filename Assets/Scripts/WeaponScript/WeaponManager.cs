using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

	// Use this for initialization
	void Start () {
        current_Weapon_Index = 0;
        weapons[current_Weapon_Index].gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TurnOnSelectWeapon(5);
        }
    }

    void TurnOnSelectWeapon(int weaponIndex)
    {
        if (weaponIndex == current_Weapon_Index)
            return;
        // turn of the current weapon
        weapons[current_Weapon_Index].gameObject.SetActive(false);

        // turn on the selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);

        // store the current selected weapon index
        current_Weapon_Index = weaponIndex;
    }

    public WeaponHandler GetCurrentWeapon()
    {
        return weapons[current_Weapon_Index];
    }
}
