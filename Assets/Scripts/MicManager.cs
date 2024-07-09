using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public int sampleWindow = 128;
    public Slider test;

    void Start()
    {

        audioSource.clip = Microphone.Start(Microphone.devices[0].ToString(), true, 10, 44100);
        audioSource.loop = true;

        while (!(Microphone.GetPosition(Microphone.devices[0].ToString()) > 0)) { }

        //audioSource.Play();
    }
    private void FixedUpdate()
    {
        float volume = GetMaxVolume();

        if (volume > test.maxValue)
        {
            test.value = 0.5f;
            Debug.Log("Mic Volume: " + volume);

        }
        else
        {
            test.value = volume;

        }
    }


    float GetMaxVolume()
    {
        float maxVolume = 0f;
        float[] waveData = new float[sampleWindow];

        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1); // 마이크 위치를 가져옵니다.
        if (micPosition < 0) return 0;

        audioSource.clip.GetData(waveData, micPosition);

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
