using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class globalScript : NetworkBehaviour {
	private int players = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addPlayer(){
		players++;
	}
	public int getPlayers(){
		Debug.Log (Network.connections.Length);	
		return players;
	}

}
