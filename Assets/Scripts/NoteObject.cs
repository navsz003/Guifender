using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

	public bool canBePressed;

	public KeyCode keyToPress;      // 后续要替换成NAudio识别的频率
	//public KeyCode leftKey;		// 左手数字
	//public KeyCode rightKey;		// 右手字母
	//public double frequencyToPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(keyToPress))
		{
			if (canBePressed)
			{
				gameObject.SetActive(false);    // 音符击中后消失
				GameManager.instance.NoteHit();
			}
		}
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Activator")
		{
			canBePressed = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Activator")
		{
			canBePressed = false;
			gameObject.SetActive(false);
			GameManager.instance.NoteMissed();
		}
	}

}


