using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodePS : MonoBehaviour {
	// Player object
	//    public GameObject p;

	// Energy ball associated to player
	public GameObject energyBall;

	// Rigid body for energy ball
	private Rigidbody rb;

	// Explosion particle system
	public GameObject explosion;
	private PlayerControllerOnline myplayer;

	// Use this for initialization
	// Use this for initialization
	void Start () {
	//	myplayer = this.gameObject.GetComponentInParent<PlayerControllerOnline>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "coin"  || col.gameObject.tag == "Shield" || col.gameObject.tag == "Protection" || col.gameObject.tag == "wall" ||  (col.gameObject.tag == "Player" && col.gameObject.GetComponentInParent<PlayerControllerOnline>().isLocalPlayer==false))
		{
			if (col.gameObject.tag == "Player" && col.gameObject.GetComponentInParent<PlayerControllerOnline> ().isLocalPlayer == false) {
				myplayer = col.gameObject.GetComponentInParent<PlayerControllerOnline> ();

				float nMonedas = Mathf.Ceil (myplayer.getMonedas() / 2);
				myplayer.attack(nMonedas,10.0f);
			
			}
			Destroy(energyBall);
			// Explosion instatiation
			GameObject explosion_handler_tmp;
			explosion_handler_tmp = Instantiate(explosion, col.transform.position, col.transform.rotation) as GameObject;

			ParticleSystem ps_tmp;
			ps_tmp = explosion_handler_tmp.GetComponent<ParticleSystem>();

			ps_tmp.Play();
			Destroy(explosion_handler_tmp, 2.0f);
		}
	

	}
}
