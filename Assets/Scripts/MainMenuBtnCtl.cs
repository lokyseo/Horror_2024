using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuBtnCtl : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Button Btn;
    public TextMeshProUGUI BtnText;
    void Start()
    {
        Btn=GetComponent<Button>();
        if(BtnText==null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned");
            return;
        }
        else
        {
            Debug.Log("check");
        }

    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(BtnText!=null)
        {
            BtnText.color = new Color32(230, 0, 0, 255);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (BtnText != null)
        {
            BtnText.color = Color.white;
        }
    }
}
