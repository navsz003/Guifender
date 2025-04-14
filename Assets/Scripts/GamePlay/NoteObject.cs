using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using NAudio.Wave;
using NAudio.Dsp;

public class NoteObject : MonoBehaviour
{

	private StringVariable fatherStr;
	private NoteScroller fatherNS;

	public bool canBeHits;		// 音符到达正确的位置
	public KeyCode keyToPress;		// 键盘触发
	public int noteNum;
	public float feq2Play;          // 吉他触发
	public float detectedFeq;
	public float feqTolerance = 5.0f;	// 允许误差

	public Sprite hitsSprite;


	// Start is called before the first frame update
	void Start()
    {
		fatherStr = transform.parent.GetComponent<StringVariable>();
		fatherNS = GetComponentInParent<NoteScroller>();
		feq2Play = fatherNS.Pos2Feq(fatherStr.strNum, noteNum);

		hitsSprite = Resources.Load<Sprite>("Img/notes/NoteHits");
	}

    // Update is called once per frame
    void Update()
    {

		// 音符在正确的位置击中
		if (canBeHits)
		{
			detectedFeq = AudioManager.Instance.DetectedFrequency;
			// Debug.Log(detectedFeq);
			if (Input.GetKeyDown(keyToPress) || (Mathf.Abs(detectedFeq-feq2Play)< feqTolerance))
			{
				
				SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
				spriteRenderer.sprite = hitsSprite;

				StartCoroutine(DestroyAfterDelay(1.0f));	// 显示1秒击中图片

				//gameObject.SetActive(false);
				//GameManager.instance.NoteHit();
			}
		}
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Activator")
		{
			canBeHits = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Activator")
		{
			canBeHits = false;
			gameObject.SetActive(false);

			Debug.Log("missed");
			// GameManager.instance.NoteMissed();
		}
	}

	private IEnumerator DestroyAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
	
}


