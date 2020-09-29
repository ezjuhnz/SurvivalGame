using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootViewManager : MonoBehaviour {


/*任务：控制摄像机视野的放大和缩小，望远镜功能
 
* 原理：放大事业：就是减小摄像机的垂直视野范围（减小FOV值）
 
*       缩小视野：就是增加摄像机的垂直视野范围（增加FOV值）
 
*/
    public int magnify = 10;//放大倍数
    public float magnifySpeed = 50f;//放大速度
    public float shrinkSpeed = 50f;//缩小速度
    public Camera m_camera;//指定的摄像机
    private float initFov;//摄像机垂直视野的范围的初始值

    private bool flag  =false; //是否处于放大状态

    private GameObject crossHair; //普通十字形瞄准

    private MouseLook mouseLook;
    private float originMouseSensity;
    private float snipeSensity;

    [SerializeField]
    private GameObject snipeBg;   //狙击瞄准
    void Start()

    {
        crossHair = GameObject.FindWithTag(Tags.CROSSHAIR);
        initFov = m_camera.fieldOfView;//设置视野的初始值
        mouseLook = transform.parent.parent.GetComponent<MouseLook>();
        originMouseSensity = mouseLook.sensity;
        snipeSensity = originMouseSensity / magnify;
    }

    void OnDisable()
    {
        ShrinkView();
    }

    void Update()

    {
        if (Input.GetMouseButtonUp(1))//按下右键放大视野
        {
            if(!flag)
            {
                MagnifyView();
            }
            else
            {
                ShrinkView();
            }
        }
       

    }
    /// <summary>
    /// 放大视野
    /// </summary>
    private void MagnifyView()//放大视野就是，减小FOV的值
    {
        flag = true;
        ShowSnipeBg();
        m_camera.fieldOfView = initFov / magnify;
        mouseLook.sensity = snipeSensity;
    }

    /// <summary>
    /// 缩小视野
    /// </summary>
    private void ShrinkView()
    {
        flag = false;
        HideSnipeBg();
        m_camera.fieldOfView = initFov;
        mouseLook.sensity = originMouseSensity;
    }

    private void ShowSnipeBg()
    {
        snipeBg.SetActive(true);
        crossHair.SetActive(false);
        //隐藏手和枪
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }

    private void HideSnipeBg()
    {
        if(snipeBg)
        {
            snipeBg.SetActive(false);
        }
        if(crossHair)
        {
            crossHair.SetActive(true);
        }

        //显示手和枪
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }
}

