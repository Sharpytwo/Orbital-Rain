using UnityEngine;
using System.Collections;
using ORBITALRAIN;

public class Sphere : Building {

	protected override void Start () {
		base.Start();
		actions = new string[] {"Sphere","Cube"};
	}
	
	public override void PerformAction(string actionToPerform) {
		base.PerformAction(actionToPerform);
		//CreateBuilding(actionToPerform);
	}
}
