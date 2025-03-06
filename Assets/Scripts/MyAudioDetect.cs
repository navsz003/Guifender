using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
//using MathNet.Numerics.IntegralTransforms;


public class MyAudioDetect : MonoBehaviour
{

	private AudioClip micRecord;
	string mic;

	private const int sampleRate = 44100;		// ������
	private const int fftSize = 1024;			// FFT �������ڴ�С

	private float[] samples = new float[fftSize]; // ���ڴ洢Ƶ������


	// Start is called before the first frame update
	void Start()
    {
		// ��ȡ��˷��豸
		mic = Microphone.devices[0];
		micRecord = Microphone.Start(mic, true, 1, sampleRate);
	}

    // Update is called once per frame
    void Update()
    {



	}
	/*

	private float GetDominantFrequency(float[] samples, int sampleRate)
	{
		// Ӧ�ú���������Ƶ��й©
		ApplyHanningWindow(samples);

		// ����FFT��������FFT�������ʵ�֣�
		Complex[] complexSamples = new Complex[samples.Length];
		for (int i = 0; i < samples.Length; i++)
		{
			complexSamples[i] = new Complex(samples[i], 0);
		}
		FFT.Forward(output, (FourierOptions)complexSamples); // ʹ�õ�����FFT��


		// �ҵ���������Ƶ������
		float maxAmplitude = 0;
		int maxIndex = 0;
		for (int i = 0; i < complexSamples.Length / 2; i++)
		{
			float amplitude = (float)complexSamples[i].Magnitude;
			if (amplitude > maxAmplitude)
			{
				maxAmplitude = amplitude;
				maxIndex = i;
			}
		}

		// ����ʵ��Ƶ��
		float frequency = maxIndex * sampleRate / samples.Length;
		return frequency;
	}



	private void ApplyHanningWindow(float[] data)
	{
		for (int i = 0; i < data.Length; i++)
		{
			data[i] *= 0.5f * (1 - Mathf.Cos(2 * Mathf.PI * i / (data.Length - 1)));
		}
	}

	float GetFreq() { 
		

		return GetDominantFrequency(samples, 44100);
	}
	*/
}
