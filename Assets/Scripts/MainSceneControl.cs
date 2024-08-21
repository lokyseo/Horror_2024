using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainSceneControl : MonoBehaviour,IPointerEnterHandler
{
    void Start()
    {

    }

    public void GamePlayBtn()
    {
        Debug.Log("Check");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnMouseExit()
    {
        Debug.Log(this.gameObject.name);
    }
}
