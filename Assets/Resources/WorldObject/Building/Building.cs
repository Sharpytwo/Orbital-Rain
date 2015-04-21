using UnityEngine;
using System.Collections.Generic;
using ORBITALRAIN;

using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Building : WorldObject, IBoxSelectable {

	#region Implemented members of IBoxSelectable
	bool _selected = false;
	public bool selected {get {return _selected;} set {_selected = value;}}	
	bool _preSelected = false;
	public bool preSelected {get {return _preSelected;}	set {_preSelected = value;}}
	#endregion 	

	protected bool needsBuilding = false;
	protected float angle;

	protected override void Awake() {
		base.Awake();
	}
	
	protected override void Start () {
		base.Start();
		objectType = "Building";
	}
	
	protected override void Update () {
		base.Update ();
	}
	
	protected override void OnGUI() {
		base.OnGUI();
		if(needsBuilding) DrawBuildProgress();
	}

	public void StartConstruction() {
		Debug.Log ("public void StartConstruction()");
		needsBuilding = true;
		CalculateBounds();	
	}

	public bool UnderConstruction() {
		return needsBuilding;}

	/*public void Construct(int amount) {
		hitPoints += amount;
		if(hitPoints >= maxHitPoints) {
			hitPoints = maxHitPoints;
			needsBuilding = false;		   
			gameObject.tag = null;} 
		}*/
	
	/*public void SpecialDamage(string type, float damage, float time, float rate) {
		if(type == "dot"); {
			//damage over time 
		}
		if(currentHealth <= 0) Death();
	}*/

	private void DrawBuildProgress() {
		//GUI.skin = ResourceManager.SelectBoxSkin;
		//Rect selectBox = WorkManager.CalculateSelectionBox(selectionBounds, playingArea);
		//Draw the selection box around the currently selected object, within the bounds of the main draw area
		GUI.BeginGroup(playingArea);
		//CalculateCurrentHealth(0.5f, 0.99f);
		//DrawHealthBar(selectBox, "Building ...");
		GUI.EndGroup();
	}

	public float Position(int slot, int snapFactor) {
		angle = slot * snapFactor;
		if (angle >= 0 && angle <= 180) {angle = angle + 180;}
		else if (angle >= 180 && angle <= 360) {angle = angle - 180;} 
		if (angle == 360) {angle = 0;}
		return angle;}
}

