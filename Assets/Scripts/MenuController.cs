using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	public double guitarStringFrequency;            // 当前的吉他输入频率
	public double volume;
	
	public static double ACHIVE_VOLUME = 60f;       // 音量达到这个值才能被激活
	public static double[] STRING_FREQUENCY = {1,2,3,4,5,6,7,8,9,9,11,12,13,14,15,16,17,18,19,20,21,22,23};
	public static double TOLERANT_FREQUENCY = 20f;  // 屏率误差
	
	
	int MoveRight() {
		return 0;
	}
	
	int MoveLeft() { 
		return 0;
	}


	// 滑音控制
	int SlideSelect() 
	{
		double lastFrequency = guitarStringFrequency;

		while (volume >= ACHIVE_VOLUME) {
			// 检测向哪个方向滑
			if (guitarStringFrequency - lastFrequency > TOLERANT_FREQUENCY)
				MoveRight();
			else if (lastFrequency - guitarStringFrequency > TOLERANT_FREQUENCY)
				MoveLeft();
			else;
			
			lastFrequency = guitarStringFrequency;
		}
		return 0;
	}


	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}


