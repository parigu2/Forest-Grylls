using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAction : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1") && !GetComponent<Animation>().isPlaying)
		{
			Swing();
		}
	}

	void Swing()
	{
		GetComponent<Animation>().CrossFade("kh");
	}
}
