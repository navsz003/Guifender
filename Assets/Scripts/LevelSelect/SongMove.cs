using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SongMove : MonoBehaviour
{

	[SerializeField]
	private Sprite[] albumList;
	private string[] albumName = { 
		"Discovery", 
		"In The Court Of The Crimson King",
		"KID A",
		"Relayer",
		"The Dark Side Of The Moon",
		"Velocity: Design: Comfort",
		"Wish You Were Here"
	};
	private int albumPointer;
	private bool apChanged = false;

	private SpriteRenderer[] songHolder = new SpriteRenderer[5];        // 存放子物体的SpriteRenderer
	[SerializeField]
	private TMP_Text songIfo;


	// 让LevelCursor类修改 AlbumPointer
	public void MoveAP(bool direction)
	{
		if (direction)
			albumPointer = (albumPointer + 1) % albumList.Length;					// 右移
		else
			albumPointer = ((albumPointer - 1) + albumList.Length) % albumList.Length;	// 左移
		apChanged = true;
	}

	private void SetSprite()
	{
		songHolder[0].sprite = albumList[((albumPointer - 2) + albumList.Length) % albumList.Length];
		songHolder[1].sprite = albumList[((albumPointer - 1) + albumList.Length) % albumList.Length];
		songHolder[2].sprite = albumList[albumPointer];
		songHolder[3].sprite = albumList[(albumPointer + 1) % albumList.Length];
		songHolder[4].sprite = albumList[(albumPointer + 2) % albumList.Length];
		songIfo.text = albumName[albumPointer];
		apChanged = false;
	}


	// Start is called before the first frame update
	void Start()
    {
		albumPointer = 0;

		// 找到子物体
		songHolder[0] = transform.Find("Song_L2")?.GetComponent<SpriteRenderer>();
		songHolder[1] = transform.Find("Song_L1")?.GetComponent<SpriteRenderer>();
		songHolder[2] = transform.Find("Song_Mid")?.GetComponent<SpriteRenderer>();
		songHolder[3] = transform.Find("Song_R1")?.GetComponent<SpriteRenderer>();
		songHolder[4] = transform.Find("Song_R2")?.GetComponent<SpriteRenderer>();

		// 第一次为 songHoder 添加图片
		SetSprite();
	}

    // Update is called once per frame
    void Update()
    {

        // 只有在 albumPointer 改变后才重新赋值
		if (apChanged)
			SetSprite();

    }
}
