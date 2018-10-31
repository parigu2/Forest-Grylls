using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public Gun theGun;

	public GameObject go_BulletHUD;

	public Text[] text_Bullet;

	// Update is called once per frame
	void Update () {
		CheckBullet();
	}

	private void CheckBullet()
	{
		text_Bullet[0].text = theGun.spareBulletCount.ToString();
		text_Bullet[1].text = theGun.currentBulletCount.ToString();
		text_Bullet[2].text = theGun.reloadBulletCount.ToString();
	}
}
