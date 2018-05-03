using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cronometro : MonoBehaviour {
    private bool gameOver;
    public Text UIText;
    public float time = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        UIText.text = "Tiempo: " + time.ToString("f0");
        if (time<0)
        {
            Application.LoadLevel("GameOver");
            UIText.text = "FIN";
        }
	}
}
