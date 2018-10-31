using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	public AudioClip switchSound;

	float moveSpeed = 10f;
	float turnSpeed = 90f;
	bool onGround = false;

	float lookSensitivity = 4;

	float cameraRotationLimit = 35;
	float currentCameraRotationX = 0;

	public GameObject theCamera;

	public GameObject bulletTable;
	public GameObject huntTable;
	public GameObject targetTable;

	public GameObject flare;
	public GameObject shot;
	public GameObject knife;

	private Rigidbody rb;
	Vector3 movement = new Vector3 (0f, 1000f, 0f);

	void Start ()
	{
			rb = GetComponent<Rigidbody>();

			if(isLocalPlayer == true)
				theCamera.SetActive(true);
			else
				theCamera.SetActive(false);
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "Terrain")
		onGround = true;
	}

	void Move()
	{
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			if(Input.GetKey(KeyCode.LeftShift))
				moveSpeed = 20f;

			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			if(Input.GetKey(KeyCode.LeftShift))
				moveSpeed = 20f;

			transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.Space) && onGround) {
			rb.AddForce(movement);
			onGround = false;
		}
		moveSpeed = 10f;

		if(Input.GetKey(KeyCode.Alpha1))
		{
			shot.SetActive(true);
			flare.SetActive(false);
			knife.SetActive(false);
			targetTable.SetActive(true);
			GetComponent<AudioSource>().PlayOneShot(switchSound);
		}
		if(Input.GetKey(KeyCode.Alpha2))
		{
			flare.SetActive(true);
			shot.SetActive(false);
			knife.SetActive(false);
			targetTable.SetActive(true);
			GetComponent<AudioSource>().PlayOneShot(switchSound);
		}
		if(Input.GetKey(KeyCode.Alpha3))
		{
			knife.SetActive(true);
			flare.SetActive(false);
			shot.SetActive(false);
			targetTable.SetActive(false);
			GetComponent<AudioSource>().PlayOneShot(switchSound);
		}

		if(Input.GetKeyDown(KeyCode.V))
			bulletTable.SetActive(true);
		if(Input.GetKeyDown(KeyCode.C))
			bulletTable.SetActive(false);

		if(Input.GetKeyDown(KeyCode.T))
			huntTable.SetActive(true);
		if(Input.GetKeyDown(KeyCode.G))
			huntTable.SetActive(false);
	}

	private void CameraRotation()
	{
			float xRotation = Input.GetAxisRaw("Mouse Y");
			float cameraRotationX = xRotation * lookSensitivity;
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

			theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
	}

	private void CharacterRotation()
	{
		float yRotation = Input.GetAxisRaw("Mouse X");
		Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
    rb.MoveRotation(rb.rotation * Quaternion.Euler(characterRotationY));
	}

	void FixedUpdate ()
	{
		if(isLocalPlayer == true)
		{
		Move();
		CameraRotation();
		CharacterRotation();
		}
	}
}
