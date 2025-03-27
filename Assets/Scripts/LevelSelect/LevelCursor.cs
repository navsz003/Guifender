using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelCursor : MonoBehaviour
{

	private int curPosX;
	private static int[] curPosY = { 70, -30, -130, -230, -330, -430 }; // 固定的指针Y轴坐标
	private int yIndex;

	[SerializeField]
	private SongMove songMove; // 关联 SongsCollect物体


    // Start is called before the first frame update
    void Start()
    {
		curPosX = 900;
		yIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
		// 菜单指针上下移动
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			yIndex = (yIndex + 1) % curPosY.Length;
			this.gameObject.transform.localPosition = new Vector3(curPosX, curPosY[yIndex], 0);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			yIndex = ((yIndex - 1)+curPosY.Length)%curPosY.Length;
			this.gameObject.transform.localPosition = new Vector3(curPosX, curPosY[yIndex], 0);
		}

		// 选中选项
		if (Input.GetKeyDown(KeyCode.Return)) {
			if (yIndex == 0)		// NEXT
				songMove.MoveAP(true);
			else if (yIndex == 1)	// PREIOUS
				songMove.MoveAP(false);
			else if (yIndex == 2)	// 
				SceneManager.LoadScene("01_02_GamePlayScenes");
			else if (yIndex == 3)
				Debug.Log("Difficulty");
			else if (yIndex == 4)
				Debug.Log("Favourite");
			else if (yIndex == 5)
				SceneManager.LoadScene("00_MenuScenes");
			else
				Debug.Log("Out of range");

		}
			
		

	}
}
