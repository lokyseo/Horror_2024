using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControll : MonoBehaviour
{
    GameObject player_Object;

    public GameObject[] map_Prefab;

    GameObject currentMap;
    GameObject frontMap;
    GameObject backMap;

    public bool isSceneClear;
    int currentNumber;

    void Start()
    {
        currentNumber = 0;
        isSceneClear = false;
        player_Object = GameObject.FindWithTag("Player");

        currentMap = map_Prefab[currentNumber];
        frontMap = map_Prefab[currentNumber + 1];
        backMap = map_Prefab[8];

        frontMap.transform.localPosition = currentMap.transform.localPosition + new Vector3(0, 0, 30);
        frontMap.SetActive(true);
    }

    void Update()
    {
        if (!isSceneClear)
        {
            currentMap = map_Prefab[1];

        }

    }

    void LoadingMap()
    {
        frontMap = map_Prefab[currentNumber + 1];
        frontMap.transform.localPosition = currentMap.transform.localPosition + new Vector3(0, 0, 30);
        frontMap.SetActive(true);

        if(currentNumber != 0)
        {
            backMap = map_Prefab[currentNumber - 1];
            backMap.transform.localPosition = currentMap.transform.localPosition - new Vector3(0, 0, 30);
            backMap.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Map" && other.transform.parent.gameObject == frontMap)
        {
            backMap.SetActive(false);

            currentMap = other.transform.parent.gameObject;
            currentNumber++;

            LoadingMap();
        }

        if (other.tag == "Map" && other.transform.parent.gameObject == backMap)
        {
            frontMap.SetActive(false);

            currentMap = other.transform.parent.gameObject;
            currentNumber--;

            LoadingMap();
        }
    }
}
