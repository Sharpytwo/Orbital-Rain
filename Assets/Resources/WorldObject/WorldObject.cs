using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ORBITALRAIN;

using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class WorldObject : MonoBehaviour { //Cleanup needed, considering planetInfo inheritance (and bulletScript). Might fix itself after post UI fix.

	//Public variables
	//public string objectName;
	//public Texture2D buildImage;
	//public int cost, sellValue, hitPoints, maxHitPoints;

	//Variables accessible by subclass
	protected Player player;
	[HideInInspector]
	public string objectType; //probably should be int for efficiency
	protected string[] actions = {};
	protected bool currentlySelected = false;
	protected Bounds selectionBounds;
	protected Rect playingArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
	protected float currentHealth, maxHealth, regenRate;
	public bool isDestructable;
	
	/*** Game engine Methods, all can be overridden by subclass ***/

	protected virtual void Awake (){
		selectionBounds = ResourceManager.InvalidBounds;
		CalculateBounds();
	}
	
	protected virtual void Start () {
		SetPlayer ();
	}

	protected virtual void Update () {
		/*gameObject.renderer.material.color = Color.white;
		if (preSelected) gameObject.renderer.material.color = Color.yellow;
		if (selected) gameObject.renderer.material.color = Color.green;	*/	
	}

	protected virtual void OnGUI() {
		//if(selected) DrawSelection();
	}

	protected virtual void DrawSelectionBox(Rect selectBox) {
		GUI.Box(selectBox, "");
	}
	
	/*** Public Methods ***/

	public void Damage (float damage) {
		currentHealth -= damage;
		if(currentHealth <= 0) Death();
	}

	protected virtual void Death () {
		//sound
		//animation
		Destroy (gameObject);
	}

	public void SetPlayer() {
		player = transform.root.GetComponentInChildren<Player>();
	}

	public virtual void SetSelection(bool selected, Rect playingArea) {
		currentlySelected = selected;
		if(selected) this.playingArea = playingArea;
	}

	public string[] GetActions() {
		return actions;
	}

	public virtual void PerformAction (string actionToPerform) {
		//it is up to children with specific actions to determine what to do with each of those actions
	}

	public virtual void SetHoverState(GameObject hoverObject) {
		//only handle input if owned by a human player and currently selected
		if(player && player.human && currentlySelected) {
			if(hoverObject.name != "Ground") print("Hello");
		}
	}
	//Decides what to do if a WorldObject is clicked. 
	public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller) {
		//only handle input if currently selected
		if(currentlySelected && hitObject && hitObject.name != "Ground") {
			WorldObject worldObject = hitObject.GetComponent< WorldObject >();
			//clicked on another selectable object
			if(worldObject && Input.GetKey(KeyCode.LeftShift)) {ChangeSelection(worldObject, controller, true);
			Debug.Log("hallo");} 
			else if (worldObject) {ChangeSelection(worldObject, controller, false);}		
		}
	}

	public void CalculateBounds() {
		selectionBounds = new Bounds(transform.position, Vector3.zero);
		foreach(Renderer r in GetComponentsInChildren< Renderer >()) {
			selectionBounds.Encapsulate(r.bounds);
		}
	}
	/*** Private Methods ***/
	private void ChangeSelection(WorldObject worldObject, Player controller, bool multiSelect) {
		SetSelection(multiSelect, playingArea);
		if(controller.SelectedObject) controller.SelectedObject.SetSelection(multiSelect, playingArea);
		controller.SelectedObject = worldObject;
		worldObject.SetSelection(true, controller.hud.GetPlayingArea());
	}

	private void DrawSelection() {
		GUI.skin = ResourceManager.SelectBoxSkin;
		Rect selectBox = WorkManager.CalculateSelectionBox(selectionBounds, playingArea);
		//Draw the selection box around the currently selected object, within the bounds of the playing area
		GUI.BeginGroup(playingArea);
		DrawSelectionBox(selectBox);
		GUI.EndGroup();
	}

	/*** Building placement (for now) ***/
	public void SetColliders(bool enabled) {
		Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
		foreach(Collider2D collider in colliders) collider.enabled = enabled;}	
	public void SetPlayingArea(Rect playingArea) {
		this.playingArea = playingArea;	}
	public Bounds GetSelectionBounds() {
		return selectionBounds;}
	public int Size(){
		int size = 5;
		return size;}
}
