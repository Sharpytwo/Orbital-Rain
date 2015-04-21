using UnityEngine;
using System.Collections;
using ORBITALRAIN;

public class Constructor : Building {
	public bool activated = false;
	PlanetInfo planet;
	//tempBuilding.transform.parent = buildings.transform;
	
	
	protected override void Awake() {
		base.Awake();
	}
	
	protected override void Start () {
		base.Start();
		
	}			
	
	protected override void Update () {
		base.Update();
		if(activated) {
			planet = GetComponentInParent<PlanetInfo>();
			Debug.Log("Activated");
			if(!planet.hasConstruct){
				Debug.Log ("Creating Construct");
				planet.hasConstruct = true;
				if(!planet.BuilderInPlace()) {
					planet.InitiateLeprechaun();
				}
			}
		}	
	}
}