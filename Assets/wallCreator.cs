using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallCreator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//this.transform.localScale = Vector3 (1.0, 1.0, 1.0);
		GameObject  ob = GameObject.CreatePrimitive(PrimitiveType.Cube);
		this.transform.position = new Vector3(this.transform.position.x+1.5f,0.0f,0.0f);
		this.transform.localScale = new Vector3 (1.0f, 2.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
