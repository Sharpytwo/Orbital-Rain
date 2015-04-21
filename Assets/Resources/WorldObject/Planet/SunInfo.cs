using UnityEngine;
using System.Collections;

public class SunInfo : CelestialBody {

	void Awake() {
		mass = 1000000000;
	}

	// Use this for initialization
	void Start () {
        gameObject.tag = "SunGwTrgt";

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
