using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyManager : MonoBehaviour {

    public static EnermyManager _instance;

    [SerializeField]
    private GameObject canibal_Prefab;

    [SerializeField]
    private GameObject boar_Prefab;

    [SerializeField]
    private Transform[] canibal_SpawnPos, boar_SpawnPos;

    [SerializeField]
    private int canibal_Spawn_Count; //将要生成的僵尸的数量

    [SerializeField]
    private int boar_Spawn_Count;   //野猪的数量

    private int canibal_Init_Count = 0; //现存僵尸数量
    private int boar_Init_Count = 0;

    public float wait_Before_Enermy_Spawn = 10f;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
	void Start () {
        canibal_Init_Count = canibal_Spawn_Count;
        boar_Init_Count = boar_Spawn_Count;
        SpawnEnermies();

        StartCoroutine("CheckToSpawnEnermies");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnBoar()
    {
        int index = 0;
        for(int i = 0; i < boar_Spawn_Count; i++)
        {
            index = (index > boar_SpawnPos.Length -1) ? 0 : index;
            GameObject.Instantiate(boar_Prefab, boar_SpawnPos[index].position, Quaternion.identity);
            index++;
        }
        boar_Spawn_Count = 0;
    }

    void SpawnCannibal()
    {
        int index = 0;
        for (int i = 0; i < canibal_Spawn_Count; i++)
        {
            index = (index > canibal_SpawnPos.Length - 1) ? 0 : index;
            GameObject.Instantiate(canibal_Prefab, canibal_SpawnPos[index].position, Quaternion.identity);
            index++;
        }
        canibal_Spawn_Count = 0;
    }

    public void EnermyDie(bool isCanibal)
    {
        if(isCanibal)
        {
            canibal_Spawn_Count++;
        }
        else
        {
            boar_Spawn_Count++;
        }
    }

    void SpawnEnermies()
    {
        SpawnCannibal();
        SpawnBoar();
    }

    IEnumerator CheckToSpawnEnermies()
    {
        yield return new WaitForSeconds(wait_Before_Enermy_Spawn);
        SpawnEnermies(); //每10秒调用生成敌人的方法,但是未必会生成敌人
        StartCoroutine("CheckToSpawnEnermies");
    }

    public void StopSpawnEnermies()
    {
        StopCoroutine("CheckToSpawnEnermies");
    }
}
