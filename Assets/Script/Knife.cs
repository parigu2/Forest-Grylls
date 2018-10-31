using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {
	public AudioClip swingSound;

	public string knife;
	public float range;

	public int damage;

	public float workSpeed;
	public float attackDelay;
	public float attackDelayActive;
	public float attackDelayDisactive;

	void Update()
	{
		if(Input.GetButtonDown("Fire1") && !GetComponent<Animation>().isPlaying)
		{
			Swing();
		}
	}

	void Swing()
	{
		GetComponent<Animation>().CrossFade("knifeSwing");
	}
}
