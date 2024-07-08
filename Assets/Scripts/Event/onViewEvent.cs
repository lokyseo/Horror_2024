using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class onViewEvent : MonoBehaviour
{
    public UnityEvent onBecameVisible;   // 뷰 안에 들어왔을 때 실행할 이벤트
    public UnityEvent onBecameInvisible; // 뷰 밖으로 나갔을 때 실행할 이벤트

    // 오브젝트가 카메라 뷰 안에 들어올 때 호출됨
    void OnBecameVisible()
    {
        onBecameVisible?.Invoke();
        Debug.Log("오브젝트가 카메라 뷰 안에 들어왔습니다.");
    }

    // 오브젝트가 카메라 뷰 밖으로 나갈 때 호출됨
    void OnBecameInvisible()
    {
        onBecameInvisible?.Invoke();
        Debug.Log("오브젝트가 카메라 뷰 밖으로 나갔습니다.");
    }
}
