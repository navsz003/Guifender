using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelCursor : MonoBehaviour
{

	private int CurPosX;
	private static int[] CurPosY = { 70, -30, -130, -230, -330, -430 }; // 固定的指针Y轴坐标
	private int YIndex;



    // Start is called before the first frame update
    void Start()
    {
		CurPosX = 900;
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
		if (Input.GetKeyDown(KeyCode.Return)) {
			if (YIndex == 0)
			{
				Debug.Log("Next");
			}
			else if (YIndex == 1)
			{
				Debug.Log("Preious");
			}
			else if (YIndex == 2)
			{
				SceneManager.LoadScene("01_02_GamePlayScenes");
			}
			else if (YIndex == 3)
			{
				Debug.Log("Difficulty");
			}
			else if (YIndex == 4)
			{
				Debug.Log("Favourite");
			}
			else if (YIndex == 5)
			{
				SceneManager.LoadScene("00_MenuScenes");
			}
			else
			{
				Debug.Log("Out of range");
			}

		}
			
		

	}
}
