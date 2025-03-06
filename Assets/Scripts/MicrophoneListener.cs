using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneListener : MonoBehaviour
{
	private AudioSource audioSource;
	private string microphoneDevice;
	private const int sampleRate = 44100; // ������
	private const int fftSize = 1024; // FFT ��С
	private float[] spectrum; // ���ڴ洢Ƶ������

	void Start()
	{
		// ����Ƿ��п��õ���˷��豸
		if (Microphone.devices.Length == 0)
		{
			Debug.LogError("No microphone found!");
			return;
		}

		// ��ȡĬ����˷��豸
		microphoneDevice = Microphone.devices[0];
		Debug.Log("Using microphone: " + microphoneDevice);

		// ��ʼ��AudioSource
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = Microphone.Start(microphoneDevice, true, 1, sampleRate);
		audioSource.loop = true;

		// ��������ⷢ������
		//audioSource.volume = 0;

		// �ȴ���˷��ʼ��
		while (Microphone.GetPosition(microphoneDevice) <= 0) { }
		Debug.Log("Microphone initialized.");

		// ��ʼ��Ƶ������
		spectrum = new float[fftSize];
	}

	void Update()
	{
		// ��ȡƵ������
		audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

		// ������ҪƵ��
		float dominantFrequency = GetDominantFrequency();

		// ���Ƶ�ʵ�����̨
		Debug.Log("Dominant Frequency: " + dominantFrequency + " Hz");
	}

	private float GetDominantFrequency()
	{
		float maxValue = 0;
		int maxIndex = 0;

		// �ҵ�Ƶ���е����ֵ
		for (int i = 0; i < spectrum.Length; i++)
		{
			if (spectrum[i] > maxValue)
			{
				maxValue = spectrum[i];
				maxIndex = i;
			}
		}

		// �����Ӧ��Ƶ��
		float frequency = maxIndex * sampleRate / fftSize;
		return frequency;
	}

	void OnDestroy()
	{
		// ֹͣ��˷�¼��
		if (Microphone.IsRecording(microphoneDevice))
		{
			Microphone.End(microphoneDevice);
			Debug.Log("Microphone stopped.");
		}
	}
}