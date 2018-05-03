using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCameraOnline : MonoBehaviour {
	void  Start (){
		Camera cam = gameObject.GetComponent<Camera> ();
		if (gameObject != null) {
			this.gameObject.GetComponentInParent<PlayerControllerOnline> ().setCamera1 (cam);
		}
	}
}