using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Camera camera;
	private AudioSource audioSource;
	public AudioClip sound;
	private bool use_gun = false;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;

		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.loop = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetMouseButtonDown (0)) {
		//	Shot ();
		//}

		if (Input.GetMouseButton (0) && use_gun == false) {
			audioSource.PlayOneShot (sound);
			use_gun = true;
			StartCoroutine ("GunTimer");
		}
	}

	void Shot() {
		int distance = 1000;
		Vector3 center = new Vector3 (Screen.width / 2, Screen.height / 2, 0);
		Ray ray = camera.ScreenPointToRay (center);
		RaycastHit hitInfo;

		if (Physics.Raycast (ray,out hitInfo,distance)) {
			Debug.DrawLine (ray.origin, hitInfo.point, Color.red);
		}

		if (Physics.Raycast (ray,out hitInfo,distance)) {
			if (hitInfo.collider.tag == "Enemy") {
				Destroy (hitInfo.collider.gameObject);
			}
		}
	}

	IEnumerator GunTimer () {
		yield return new WaitForSeconds (0.1f);
		use_gun = false;
	}
}
