using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCount : MonoBehaviour {
	public Gun theGun;

	public GameObject go_CountHUD;

	public Text[] text_Count;

	// Update is called once per frame
	void Update () {
		CheckCount();
	}

	private void CheckCount()
	{
		text_Count[0].text = theGun.checkCount.ToString();
	}
}
