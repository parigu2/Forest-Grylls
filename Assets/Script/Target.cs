using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
	private const float WALKING_FIRE = 0.08f, STANDING_FIRE = 0.04f;

	public Animator animator;
	public float accuracy;

	public GameObject crossHUD;

	// Update is called once per frame
	void Update () {

	}
}
