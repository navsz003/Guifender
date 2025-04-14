using UnityEngine;
using NAudio.Wave;
using NAudio.Dsp;
using System;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	private WaveInEvent waveIn;
	private float rawFrequency; // 原始检测到的频率
	public float DetectedFrequency { get; private set; } // 平滑后的频率

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // 确保对象不会在场景切换时被销毁
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		waveIn = new WaveInEvent
		{
			WaveFormat = new WaveFormat(44100, 1), // 设置为44.1kHz，单声道
			BufferMilliseconds = 50 // 设置缓冲区大小为50ms
		};
		waveIn.DataAvailable += OnDataAvailable;
		waveIn.StartRecording();
	}

	void OnDataAvailable(object sender, WaveInEventArgs e)
	{
		int bytesPerSample = 2; // 16位音频 = 2字节
		int sampleCount = e.BytesRecorded / bytesPerSample;

		if (sampleCount == 0) return;

		float[] samples = new float[sampleCount];
		for (int i = 0; i < sampleCount; i++)
		{
			samples[i] = BitConverter.ToInt16(e.Buffer, i * bytesPerSample) / 32768f;
		}

		// 计算原始频率
		rawFrequency = CalculateFrequency(samples);
	}

	void Update()
	{
		// 平滑频率变化
		DetectedFrequency = Mathf.Lerp(DetectedFrequency, rawFrequency, Time.deltaTime * 10);
		Debug.Log($"Frequency: {DetectedFrequency} Hz");
	}

	float CalculateFrequency(float[] samples)
	{
		// 实现FFT计算频率的逻辑
		int fftSize = Mathf.NextPowerOfTwo(samples.Length);
		Complex[] fftBuffer = new Complex[fftSize];

		for (int i = 0; i < samples.Length; i++)
		{
			fftBuffer[i].X = (float)(samples[i] * FastFourierTransform.HannWindow(i, fftSize));
			fftBuffer[i].Y = 0;
		}

		for (int i = samples.Length; i < fftSize; i++) // 补零填充
		{
			fftBuffer[i].X = 0;
			fftBuffer[i].Y = 0;
		}

		FastFourierTransform.FFT(true, (int)Mathf.Log(fftSize, 2), fftBuffer);

		// 找到幅值谱中的最大值对应的频率
		float[] magnitudes = new float[fftSize / 2];
		for (int i = 0; i < fftSize / 2; i++)
		{
			magnitudes[i] = Mathf.Sqrt(fftBuffer[i].X * fftBuffer[i].X + fftBuffer[i].Y * fftBuffer[i].Y);
		}

		int maxIndex = 0;
		for (int i = 1; i < magnitudes.Length; i++)
		{
			if (magnitudes[i] > magnitudes[maxIndex]) maxIndex = i;
		}

		return maxIndex * 44100f / fftSize; // 计算主频率
	}
}