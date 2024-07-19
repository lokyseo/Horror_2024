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
        isSceneClear = true;
        player_Object = GameObject.FindWithTag("Player");

        currentMap = map_Prefab[currentNumber];
        frontMap = map_Prefab[currentNumber + 1];
        backMap = map_Prefab[8];

        frontMap.transform.localPosition = currentMap.transform.localPosition + new Vector3(0, 0, currentMap.GetComponentInChildren<MeshRenderer>().bounds.size.z);
        frontMap.SetActive(true);

        RenderSettings.ambientLight = Color.black;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneFailed();
           // isSceneClear = true;
        }

    }

    void SceneFailed()
    {
        currentNumber = 0;

        backMap.SetActive(false);
        currentMap.SetActive(false);
        frontMap.SetActive(false);

        backMap = map_Prefab[0];
        backMap.SetActive(true);
        currentMap = map_Prefab[1];
        currentMap.SetActive(true) ;
        frontMap = map_Prefab[2];
        frontMap.SetActive(true) ;

        transform.position = 
            new Vector3(backMap.transform.position.x, transform.position.y + 1.5f, backMap.transform.position.z);
    }

    void LoadingMap()
    {
        frontMap = map_Prefab[currentNumber + 1];
        frontMap.transform.localPosition = currentMap.transform.localPosition + new Vector3(0, 0, currentMap.GetComponentInChildren<MeshRenderer>().bounds.size.z);
        frontMap.SetActive(true);

        if(currentNumber != 0)
        {
            backMap = map_Prefab[currentNumber - 1];
            backMap.transform.localPosition = currentMap.transform.localPosition - new Vector3(0, 0, currentMap.GetComponentInChildren<MeshRenderer>().bounds.size.z);
            backMap.SetActive(true);
        }

    }

    void EventGenerate(int curNum)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Map" && other.transform.parent.gameObject == frontMap)
        {
            backMap.SetActive(false);

            currentMap = other.transform.parent.gameObject;
            
            currentNumber++;

            LoadingMap();
            EventGenerate(currentNumber);
        }

        if (other.tag == "Map" && other.transform.parent.gameObject == backMap)
        {
            frontMap.SetActive(false);

            currentMap = other.transform.parent.gameObject;
            currentNumber--;

            LoadingMap();
            EventGenerate(currentNumber);
        }
    }
}
