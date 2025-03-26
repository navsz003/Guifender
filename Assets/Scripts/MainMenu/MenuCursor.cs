using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuCursor : MonoBehaviour
{

	private int CurPosX;
	private static int[] CurPosY = { 100, 0, -100, -200, -300, -400 }; // 固定的指针Y轴坐标
	private int YIndex;

	private static string[] SelectScene = {
		"01_01_LevelSelectScenes",
		"01_01_LevelSelectScenes",
		"02_GuitarTune",
		"03_Option",
		"04_DLC",
		"05_Exit"
	};



    // Start is called before the first frame update
    void Start()
    {
        CurPosX = 0;
		YIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
		// 菜单指针上下移动
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			YIndex = (YIndex + 1) % CurPosY.Length;
			this.gameObject.transform.localPosition = new Vector3(CurPosX, CurPosY[YIndex], 0);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			YIndex = ((YIndex - 1)+CurPosY.Length)%CurPosY.Length;
			this.gameObject.transform.localPosition = new Vector3(CurPosX, CurPosY[YIndex], 0);
		}

		// 选中选项
		if (Input.GetKeyDown(KeyCode.Return))
			SceneManager.LoadScene(SelectScene[YIndex]);
			
			
		

	}
}
