using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScroller : MonoBehaviour
{

	// 音符移动的速度
	public float beatTempo;

	public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
		//beatTempo = beatTempo / 60f;
		beatTempo = beatTempo / 30f;	// 速度×2，拉大音符的间距
    }

    // Update is called once per frame
    void Update()
    {
		if (!hasStarted)
		{
			if (Input.anyKeyDown)
			{
				hasStarted = true;
			}
		}
		else
		{
			transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
		}
    }
}
