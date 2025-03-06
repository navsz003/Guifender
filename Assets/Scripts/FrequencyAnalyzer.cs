using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyAnalyzer : MonoBehaviour
{
	private AudioClip micClip;
	private string micDevice;
	private int sampleRate = 44100;
	private float[] audioSamples;
	private float[] spectrum;

	void Start()
	{
		// ��ȡĬ����˷��豸����
		micDevice = Microphone.devices[0];

		// ��ʼ¼��
		micClip = Microphone.Start(micDevice, true, 1, sampleRate);

		// ��ʼ������
		audioSamples = new float[sampleRate];
		spectrum = new float[256]; // FFT ��Ƶ�����ݵ���
	}

	void Update()
	{
		// ��ȡʵʱ��Ƶ����
		micClip.GetData(audioSamples, 0);

		// ��ȡƵ������
		AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

		// ���Ƶ�׵�ǰ����Ƶ��ֵ�����磬��һ��Ƶ�ʳɷ֣�
		if (spectrum != null && spectrum.Length > 0)
		{
			float frequency = GetDominantFrequency(spectrum);
			Debug.Log("Dominant frequency: " + frequency + " Hz");
		}
	}

	// ��ȡƵ������ǿ��Ƶ�ʳɷ�
	private float GetDominantFrequency(float[] spectrum)
	{
		float maxMagnitude = 0f;
		int maxIndex = 0;

		// �ҵ�Ƶ���������ȵ�����
		for (int i = 0; i < spectrum.Length; i++)
		{
			if (spectrum[i] > maxMagnitude)
			{
				maxMagnitude = spectrum[i];
				maxIndex = i;
			}
		}

		// ���� FFT ��Ƶ�ʷֱ��ʼ���ʵ��Ƶ��
		float dominantFrequency = maxIndex * (sampleRate / 2) / spectrum.Length;
		return dominantFrequency;
	}
}
