using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScroller : MonoBehaviour
{

	public float beatTempo;
	public bool hasStarted;

	private static float[] notesFeq = {
		82.4f, 87.3f, 92.5f, 98.0f, 103.8f,
		110.0f, 116.5f, 123.4f, 130.8f, 138.6f,
		146.8f, 155.5f, 163.8f, 174.6f, 185.0f,
		196.0f, 207.6f, 220.0f, 233.0f,
		246.9f, 261.6f, 277.1f, 293.6f, 311.1f,
		329.6f, 349.2f, 270.0f, 392.0f, 415.3f,
		440.0f, 466.1f, 493.8f, 523.2f, 554.3f,
		587.3f, 622.2f, 659.2f, 698.4f, 740.0f,
		784.0f, 830.6f, 880.0f, 932.3f, 987.7f,
		1046.5f, 1108.7f, 1174.6f, 1244.5f, 1318.5f
	};

	public float Pos2Feq(int strPos, int noteNum) {
		if (strPos == 0)
			return notesFeq[0+noteNum];
		else if (strPos == 1)
			return notesFeq[5+noteNum];
		else if (strPos == 2)
			return notesFeq[10+noteNum];
		else if (strPos == 3)
			return notesFeq[15+noteNum];
		else if (strPos == 4)
			return notesFeq[19+noteNum];
		else if (strPos == 5)
			return notesFeq[24+noteNum];
		else
			return 0.0f;
	}

    // Start is called before the first frame update
    void Start()
    {
		beatTempo = beatTempo / 30f;
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
			transform.position -= new Vector3(beatTempo * Time.deltaTime * 100, 0, 0);
		}
    }
}
