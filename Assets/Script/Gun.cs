using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gun : NetworkBehaviour {
	public AudioClip flareShotSound;
	public AudioClip noAmmoSound;
	public AudioClip reloadSound;

	public Transform barrelEnd;
	public ParticleSystem muzzleFlash;

	public string gunName;
	public float range;

	public int spareBulletCount;
	public int currentBulletCount;
	public int reloadBulletCount;
	public int checkCount;

	private Vector3 originPos;
	public Camera theCam;

	private RaycastHit hitInfo;
	public GameObject hitEffect;
	public GameObject bullet;

	void Start()
	{
		originPos = Vector3.zero;
	}

	void Update ()
	{
		if(Input.GetButtonDown("Fire1") && !GetComponent<Animation>().isPlaying)
		{
			if(currentBulletCount > 0){
				Shoot();
			}else{
				GetComponent<AudioSource>().PlayOneShot(noAmmoSound);
			}
		}

		if(Input.GetKeyDown(KeyCode.R) && !GetComponent<Animation>().isPlaying)
		{
			Reload();
		}
	}

	void Shoot()
	{
			currentBulletCount--;
		if(currentBulletCount <= 0){
			currentBulletCount = 0;
		}
			GetComponent<Animation>().CrossFade("ShootShotGun");
			GetComponent<AudioSource>().PlayOneShot(flareShotSound);
			var muzzel = Instantiate(muzzleFlash, barrelEnd.position,barrelEnd.rotation);
			Destroy(muzzel, 2f);
			Hit();
	}

	private void Hit()
	{
		if(Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, range))
		{
			var hitclone = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
			Debug.Log(hitInfo.transform.tag);
			if(hitInfo.transform.tag.ToString() == "Animal")
			{
				checkCount++;
				Debug.Log("hit the animal");
			}

			GameObject bulletclone = (GameObject)Instantiate(bullet, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

			NetworkServer.SpawnWithClientAuthority(bulletclone, connectionToClient);
			bulletclone.transform.Translate(-Vector3.forward * 20 * Time.deltaTime);
			Destroy(hitclone, 2f);
			Destroy(bulletclone, 5f);
		}
	}

	void Reload()
	{
		if(spareBulletCount >= 1 && currentBulletCount == 0){
			GetComponent<AudioSource>().PlayOneShot(reloadSound);
			spareBulletCount--;
			currentBulletCount+=reloadBulletCount;
			GetComponent<Animation>().CrossFade("Reload");
		}
		if(spareBulletCount == 0)
		{
			reloadBulletCount = 0;
		}
	}
}
