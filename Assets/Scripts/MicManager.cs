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
        // 오디오 소스가 설정되지 않았다면 현재 객체에서 AudioSource 컴포넌트를 가져옵니다.
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // 마이크를 시작하고 오디오 소스에 할당합니다.
        audioSource.clip = Microphone.Start(microphoneName, true, 10, 44100);
        audioSource.loop = true;

        // 마이크가 시작될 때까지 대기합니다.
        while (!(Microphone.GetPosition(microphoneName) > 0)) { }

        audioSource.Play();
    }

    void Update()
    {
        // 마이크 음량 측정
        float volume = GetMaxVolume();
        Debug.Log("Mic Volume: " + volume);
    }

    float GetMaxVolume()
    {
        float maxVolume = 0f;
        float[] waveData = new float[sampleWindow];

        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1); // 마이크 위치를 가져옵니다.
        if (micPosition < 0) return 0;

        audioSource.clip.GetData(waveData, micPosition);

        // 각 샘플 데이터의 절대값 중 최대값을 찾습니다.
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
