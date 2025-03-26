using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScroller : MonoBehaviour
{

	// ����ƶ����ٶ�
	public float beatTempo;

	public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
		//beatTempo = beatTempo / 60f;
		beatTempo = beatTempo / 30f;	// �ٶȡ�2����������ļ��
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
