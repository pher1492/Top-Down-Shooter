using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]

public class Gun : MonoBehaviour {

	// Handling
	public enum GunType {Semi, Burst, Auto}
	public GunType gunType;
	public float rpm;

	// System
	private float secondsBetweenShots;
	private float nextPossibleShootTime;

	// Components
	public Transform shotSpawn;
	public Transform shellEjectionPoint;
	private AudioSource gunSound;
	private LineRenderer tracer;
	public Rigidbody shell;

	void Start () {
		gunSound = GetComponent<AudioSource>();
		secondsBetweenShots = 60 / rpm;

		if (GetComponent<LineRenderer> ()) {
			tracer = GetComponent<LineRenderer> ();
		}
	}

	void Update () {
	
	}

	public void Shoot () {
		if (CanShoot ()) {
			Ray ray = new Ray (shotSpawn.position, shotSpawn.forward);
			RaycastHit hit;

			float shotDistance = 20;
			if (Physics.Raycast (ray, out hit, shotDistance)) {
				shotDistance = hit.distance;
			}

			nextPossibleShootTime = Time.time + secondsBetweenShots;

			gunSound.Play ();

			if (tracer) {
				StartCoroutine ("RenderTracer", ray.direction * shotDistance);
			}

			Rigidbody newShell = Instantiate (shell, shellEjectionPoint.position, shellEjectionPoint.rotation) as Rigidbody;
			newShell.AddForce(shellEjectionPoint.right * Random.Range(150f, 200f) + shellEjectionPoint.forward * Random.Range(-10f, 10f));

			// Debug Draw Ray
			Debug.DrawRay (ray.origin, ray.direction * shotDistance, Color.red, 1);
		}
	}

	public void ShootContinous () {
		if (gunType == GunType.Auto) {
			Shoot ();
		}
	}

	private bool CanShoot() {
		bool canShoot = true;

		if (Time.time < nextPossibleShootTime) {
			canShoot = false;
		}

		return canShoot;
	}

	IEnumerator RenderTracer(Vector3 hitPoint) {
		tracer.enabled = true;
		tracer.SetPosition (0, shotSpawn.position);
		tracer.SetPosition (1, shotSpawn.position + hitPoint);

		yield return null;

		tracer.enabled = false;
	}
}
