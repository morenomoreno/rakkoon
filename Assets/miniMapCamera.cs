using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniMapCamera : MonoBehaviour {

	void Start () {
		//imagen = GameObject.Find("Image");
		Camera cam = gameObject.GetComponent<Camera> ();
		if (gameObject != null) {
			this.gameObject.GetComponentInParent<PlayerControllerOnline> ().setCamera2 (cam);
		}
	}

	// Update is called once per frame
	void Update () {
	}
}
