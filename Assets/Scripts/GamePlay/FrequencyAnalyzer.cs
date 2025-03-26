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
		// 获取默认麦克风设备名称
		micDevice = Microphone.devices[0];

		// 开始录音
		micClip = Microphone.Start(micDevice, true, 1, sampleRate);

		// 初始化数组
		audioSamples = new float[sampleRate];
		spectrum = new float[256]; // FFT 的频谱数据点数
	}

	void Update()
	{
		// 获取实时音频样本
		micClip.GetData(audioSamples, 0);

		// 获取频谱数据
		AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

		// 输出频谱的前几个频率值（例如，第一个频率成分）
		if (spectrum != null && spectrum.Length > 0)
		{
			float frequency = GetDominantFrequency(spectrum);
			Debug.Log("Dominant frequency: " + frequency + " Hz");
		}
	}

	// 获取频谱中最强的频率成分
	private float GetDominantFrequency(float[] spectrum)
	{
		float maxMagnitude = 0f;
		int maxIndex = 0;

		// 找到频谱中最大幅度的索引
		for (int i = 0; i < spectrum.Length; i++)
		{
			if (spectrum[i] > maxMagnitude)
			{
				maxMagnitude = spectrum[i];
				maxIndex = i;
			}
		}

		// 根据 FFT 的频率分辨率计算实际频率
		float dominantFrequency = maxIndex * (sampleRate / 2) / spectrum.Length;
		return dominantFrequency;
	}
}
