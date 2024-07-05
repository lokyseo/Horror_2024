using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicManager : MonoBehaviour
{
    public string microphoneName = null;
    public AudioSource audioSource;
    public int sampleWindow = 128;

    void Start()
    {
        // ����� �ҽ��� �������� �ʾҴٸ� ���� ��ü���� AudioSource ������Ʈ�� �����ɴϴ�.
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // ����ũ�� �����ϰ� ����� �ҽ��� �Ҵ��մϴ�.
        audioSource.clip = Microphone.Start(microphoneName, true, 10, 44100);
        audioSource.loop = true;

        // ����ũ�� ���۵� ������ ����մϴ�.
        while (!(Microphone.GetPosition(microphoneName) > 0)) { }

        audioSource.Play();
    }

    void Update()
    {
        // ����ũ ���� ����
        float volume = GetMaxVolume();
        Debug.Log("Mic Volume: " + volume);
    }

    float GetMaxVolume()
    {
        float maxVolume = 0f;
        float[] waveData = new float[sampleWindow];

        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1); // ����ũ ��ġ�� �����ɴϴ�.
        if (micPosition < 0) return 0;

        audioSource.clip.GetData(waveData, micPosition);

        // �� ���� �������� ���밪 �� �ִ밪�� ã���ϴ�.
        for (int i = 0; i < sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (maxVolume < wavePeak)
            {
                maxVolume = wavePeak;
            }
        }

        return Mathf.Sqrt(maxVolume);
    }
}
