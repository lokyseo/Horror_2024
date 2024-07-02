using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControll : MonoBehaviour
{
    public GameObject[] map_Prefab;
    public Transform nowLocate;
    GameObject player_Object;
    public bool isSceneClear;
    int currentNumber;

    void Start()
    {
        currentNumber = 0;
        player_Object = GameObject.FindWithTag("Player");
        map_Prefab[currentNumber].transform.localPosition = nowLocate.localPosition + new Vector3(0, 0, 30);
        map_Prefab[currentNumber].SetActive(true);
        isSceneClear = false;
    }

    void Update()
    {
        if(isSceneClear)
        {
            currentNumber++;
            map_Prefab[currentNumber].transform.localPosition = nowLocate.localPosition + new Vector3(0, 0, 30);
            map_Prefab[currentNumber].SetActive(true);
            map_Prefab[0].transform.localPosition = nowLocate.localPosition + new Vector3(0, 0, -30);
            map_Prefab[0].SetActive(true);
            isSceneClear = false;
        }
        else
        {
            currentNumber = 0;
            map_Prefab[currentNumber].transform.localPosition = nowLocate.localPosition + new Vector3(0, 0, -30);
            map_Prefab[currentNumber].SetActive(true);
        }
        
    }
}
