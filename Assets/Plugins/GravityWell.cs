using UnityEngine;
using System.Collections;
using System;


public class GravityWell
{
	public string name;
	public Vector3 position;
	public double gWMass;
	public GameObject connectedObject;

	public GravityWell(string newName, Vector3 newPosition, double newGWMass, GameObject newObject){
		name = newName;
		position = newPosition;
		gWMass = newGWMass;
		connectedObject = newObject;

	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
