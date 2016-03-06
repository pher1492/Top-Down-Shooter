using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]

public class PlayerController : MonoBehaviour {
	
	// Handling
	public float rotationSpeed = 450;
	public float walkSpeed = 5;
	public float runSpeed = 8;
	public float acceleration = 5;

	// System
	private Quaternion targetRotation;
	private Vector3 currentVelocityMod;

	// Components
	private CharacterController controller;
	private Camera cam;
	public Gun gun;

	void Start () {
		controller = GetComponent<CharacterController>();
		cam = Camera.main;
	}

	void Update () {
		ControlMouse();

		if (Input.GetButtonDown ("Shoot")) {
			gun.Shoot ();
		} else if (Input.GetButton ("Shoot")) {
			gun.ShootContinous ();
		}
	}

	void ControlMouse () {
		Vector3 mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint(new Vector3 (mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));

		targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		currentVelocityMod = Vector3.MoveTowards (currentVelocityMod, input, acceleration * Time.deltaTime);
		Vector3 motion = currentVelocityMod;
		motion *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.z) == 1) ? .7f : 1;
		motion *= (Input.GetButton ("Run")) ? runSpeed : walkSpeed;
		motion += Vector3.up * -8; // Gravity
		controller.Move(motion * Time.deltaTime);
	}
}
