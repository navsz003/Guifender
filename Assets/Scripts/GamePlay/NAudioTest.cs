using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using NAudio.Wave;
using NAudio.Dsp;
using UnityEngine.UIElements;


public class NAudioTest : MonoBehaviour
{

	static NAudio.Wave.WaveFormat waveFormat;
	WaveInEvent waveIn = new WaveInEvent();

	// Start is called before the first frame update
	void Start()
    {

		waveFormat = waveIn.WaveFormat;
		waveIn.DataAvailable += OnDataAvailable;

		// 开始录音
		waveIn.StartRecording();


	}

    // Update is called once per frame
    void Update()
    {
		// 指定处理音频数据的回调方法
		waveIn.DataAvailable += OnDataAvailable;

	}

	// 处理音频数据的回调方法
	static void OnDataAvailable(object sender, WaveInEventArgs e)
	{
		// 将字节数据转换为浮点数组
		int bytesPerSample = 2; // 16位音频 = 2字节
		int sampleCount = e.BytesRecorded / bytesPerSample;
		float[] samples = new float[sampleCount];
		for (int i = 0; i < sampleCount; i++)
		{
			samples[i] = BitConverter.ToInt16(e.Buffer, i * bytesPerSample) / 32768f;
		}

		// 使用NAudio.Dsp.FFT进行快速傅里叶变换
		Complex[] fftBuffer = new Complex[samples.Length];
		for (int i = 0; i < samples.Length; i++)
		{
			fftBuffer[i].X = (float)(samples[i] * FastFourierTransform.HannWindow(i, samples.Length));
			fftBuffer[i].Y = 0;
		}

		FastFourierTransform.FFT(true, (int)Math.Log(samples.Length, 2.0), fftBuffer);

		// 计算幅值谱
		float[] magnitudes = new float[fftBuffer.Length / 2];
		for (int i = 0; i < fftBuffer.Length / 2; i++)
		{
			magnitudes[i] = (float)Math.Sqrt(fftBuffer[i].X * fftBuffer[i].X + fftBuffer[i].Y * fftBuffer[i].Y);
		}

		// 找到幅值谱中的最大值对应的频率
		int maxIndex = 0;
		for (int i = 1; i < magnitudes.Length; i++)
		{
			if (magnitudes[i] > magnitudes[maxIndex]) maxIndex = i;
		}

		// 计算主频率
		float frequency = maxIndex * waveFormat.SampleRate / samples.Length;
		Debug.Log(frequency);
	}

	public void OnApplicationQuit()
	{
		// unity停止时，收音进程不会自动停止，从这里强制停止
		waveIn.StopRecording();
	}
}
