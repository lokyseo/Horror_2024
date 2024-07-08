using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class onViewEvent : MonoBehaviour
{
    public UnityEvent onBecameVisible;   // �� �ȿ� ������ �� ������ �̺�Ʈ
    public UnityEvent onBecameInvisible; // �� ������ ������ �� ������ �̺�Ʈ

    // ������Ʈ�� ī�޶� �� �ȿ� ���� �� ȣ���
    void OnBecameVisible()
    {
        onBecameVisible?.Invoke();
        Debug.Log("������Ʈ�� ī�޶� �� �ȿ� ���Խ��ϴ�.");
    }

    // ������Ʈ�� ī�޶� �� ������ ���� �� ȣ���
    void OnBecameInvisible()
    {
        onBecameInvisible?.Invoke();
        Debug.Log("������Ʈ�� ī�޶� �� ������ �������ϴ�.");
    }
}
