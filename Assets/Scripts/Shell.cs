using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

	// Handling
	public float lifetime = 5f;

	// System
	private Color originalCol;
	private float fadePercent;
	private float deathTime;
	private bool fading;

	// Components
	private Material mat;

	void Start () {
		mat = GetComponent<Renderer> ().material;
		originalCol = mat.color;
		deathTime = Time.time + lifetime;

		StartCoroutine ("Fade");
	}

	IEnumerator Fade() {
		while (true) {
			yield return new WaitForSeconds (.2f);

			if (fading) {
				fadePercent += Time.deltaTime;
				mat.color = Color.Lerp (originalCol, Color.clear, fadePercent);

				if (fadePercent >= 1) {
					Destroy (gameObject);
				}
			}
			else if (Time.time > deathTime) {
				fading = true;
			}
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "Ground") {
			GetComponent<Rigidbody>().Sleep ();
		}
	}
}
