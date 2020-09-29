using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;

    public float fireRate = 15f;
    private float nextTimeToFire;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;
    private GameObject crosshair;
    private GameObject snipeBg;
    
    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_prefab, spear_prefab;

    [SerializeField]
    private Transform arrow_Start_Position;

    public CameraShake cameraShake;
	// Use this for initialization
	void Start () {
        weapon_Manager = GetComponent<WeaponManager>();

        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT)
            .transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        snipeBg = GameObject.FindWithTag(Tags.SNIPEBG);
        mainCam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        float damage = weapon_Manager.GetCurrentWeapon().damage;
        if (weapon_Manager.GetCurrentWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                weapon_Manager.GetCurrentWeapon().ShootAnimation();
               
                BulletFire(damage);
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                //axe
                if (weapon_Manager.GetCurrentWeapon().tag == Tags.AXE_TAG)
                {
                    weapon_Manager.GetCurrentWeapon().ShootAnimation();
                }
                //shoot
                if(weapon_Manager.GetCurrentWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weapon_Manager.GetCurrentWeapon().ShootAnimation();
                    BulletFire(damage);
                    if(weapon_Manager.GetCurrentWeapon().weapon_Aim == WeaponAim.SELF_AIM)
                    {
                        StartCoroutine(cameraShake.Shake(0.1f, 0.4f));
                        
                    }
                }
                //arrow or spear
                else
                {
                    if(is_Aiming)
                    {
                        weapon_Manager.GetCurrentWeapon().ShootAnimation();
                        if(weapon_Manager.GetCurrentWeapon().bulletType ==
                            WeaponBulletType.ARROW)
                        {
                            //throw arrow
                            ThrowArrowOrSpear(true);
                        }
                        else if(weapon_Manager.GetCurrentWeapon().bulletType ==
                            WeaponBulletType.SPEAR)
                        {
                            //throw spear
                            ThrowArrowOrSpear(false);
                        }
                    }
                }
            }
        }
    }

    private void ThrowArrowOrSpear(bool throwArrow)
    {
        if(throwArrow)
        {
            GameObject arrow = Instantiate(arrow_prefab);
            arrow.transform.position = arrow_Start_Position.position;
            arrow.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
        else
        {
            GameObject spear = Instantiate(spear_prefab);
            spear.transform.position = arrow_Start_Position.position;
            spear.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
    }

    void ZoomInAndOut()
    {
        if(weapon_Manager.GetCurrentWeapon().weapon_Aim == WeaponAim.AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);
            }
            if(Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
            }
        }
        //if we need to zoom the weapon
        if(weapon_Manager.GetCurrentWeapon().weapon_Aim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weapon_Manager.GetCurrentWeapon().Aim(true);
                is_Aiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                weapon_Manager.GetCurrentWeapon().Aim(false);
                is_Aiming = false;
            }

        }
    }

    void BulletFire(float damage)
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward,out hit))
        {
            if(hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }

    
}
