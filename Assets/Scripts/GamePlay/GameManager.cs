using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public AudioSource theMusic;

	public bool startPlaying;

	public NoteScroller noteScroller;

	public static GameManager instance;

	public int currentScore;
	public int scorePerNote = 100;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
		currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
		{
			if (Input.anyKeyDown)
			{
				startPlaying = true;
				noteScroller.hasStarted = true;
				// theMusic.Play();
			}
		}
    }

	public void NoteHit()
	{
		Debug.Log("Hit");

		// if (音符x坐标 - 判定线x坐标 > 1)
		//		HitEarly();
		// else if (音符x坐标 - 判定线x坐标 > -1)
		//		HitPerfect();				
		// else
		//		HitLate();

	}

	public void NoteMissed()
	{
		Debug.Log("Miss");
	}

}
