using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainSceneControl : MonoBehaviour
{
    public PlayableDirector playableDirector;
    void Awake()
    {
        playableDirector.Play();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GamePlayBtnClick()
    {
        Debug.Log("Test");
    }

    public void GameExitClick()
    {
        Debug.Log("exit");
    }
}
