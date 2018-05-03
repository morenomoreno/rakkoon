using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (false);
		if (gameObject != null) {
			this.gameObject.GetComponentInParent<PlayerControllerOnline> ().setProtection (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = this.gameObject.GetComponentInParent<PlayerControllerOnline> ().transform.position;
	}
}
