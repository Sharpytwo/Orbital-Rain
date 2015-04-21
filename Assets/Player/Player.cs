using UnityEngine;
using System.Collections;
using ORBITALRAIN;

using System;
using System.Globalization;

public class Player : MonoBehaviour {
    
    [HideInInspector]
	public Camera activeCamera;
	public string username;
	public bool human, withinRange = true;
	[HideInInspector]
    public HUD hud;
    [HideInInspector]
    public PlanetInfo closestPlanet;
	public WorldObject SelectedObject { get; set; } //Saves the info of which item is selected
	public Material AllowedMaterial, notAllowedMaterial, someMaterial;
    [HideInInspector]
    public int gridSlot;
	private Building tempBuilding;
	private bool findingPlacement = false;	
	private float timerTarget;

	void Start () {
		activeCamera = Camera.main;
		hud = GetComponentInChildren<HUD>();
		timerTarget = Time.time + ResourceManager.searchPlanetFrequency;
	}

	// Update is called once per frame
	void Update () {
		if(findingPlacement) {
			tempBuilding.CalculateBounds();
			if(CanPlaceBuilding()) tempBuilding.GetComponent<Renderer>().material = AllowedMaterial;
			else tempBuilding.GetComponent<Renderer>().material = notAllowedMaterial;
		}
	}

	public void CreateBuilding(GameObject building) {
		Vector3 mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		//InvokeRepeating ("FindClosestPlanet", 0, ResourceManager.searchFrequency);
		GameObject newBuilding = (GameObject)Instantiate (building, mousePos, new Quaternion()); //Where the new building is made		
		tempBuilding = newBuilding.GetComponent<Building>();
		if (tempBuilding) {
			//tempCreator = creator;
			findingPlacement = true;
		    tempBuilding.SetColliders(false);
			//tempBuilding.SetPlayingArea(playingArea);
		} else Destroy(newBuilding);
	}  

	public bool IsFindingBuildingLocation() {return findingPlacement;}

	public void FindBuildingLocation() { 		
		if( Time.time >= timerTarget){ //call FindClosestPlanet every few seconds
			closestPlanet = FindClosestPlanet ().GetComponent<PlanetInfo> ();
			timerTarget = Time.time + ResourceManager.searchPlanetFrequency;}
		Vector2 planetPos = closestPlanet.transform.position;
		/**/Vector2 relPos = activeCamera.ScreenToWorldPoint (Input.mousePosition);
		/**/float angle = Mathf.Atan2 (planetPos.y - relPos.y, planetPos.x - relPos.x) * Mathf.Rad2Deg;
		/**/if (angle < 0) {angle = angle + 360;} //Sets angle range from 0-360 instead of -180-180
		/**/int angleSnap = Mathf.RoundToInt (angle / closestPlanet.snapFactor) * closestPlanet.snapFactor;
		closestPlanet.pivot.transform.rotation = Quaternion.AngleAxis (angleSnap + 180, Vector3.forward);
		int buildAngle = angleSnap + 90;
		//Quaternion buildAngle = closestPlanet.pivot.transform.rotation * Quaternion.Euler (0, 0, - 90);	
		Vector3 deltaPos = relPos - planetPos;
		float distanceFromCenter = deltaPos.sqrMagnitude; //Previously: Vector2.Distance (relPos, planetPos);
			if (distanceFromCenter >= ResourceManager.distanceFromCenterLimit || !closestPlanet.builderInPlace)
				{ Vector3 newLocation = WorkManager.FindHitPoint (Input.mousePosition);
				newLocation.z = 1;
				tempBuilding.transform.position = newLocation;
				withinRange = false;
			} else if (closestPlanet.builderInPlace) { //Avoid closestPlanet with Get Set?
				//tempBuilding.transform.rotation.eulerAngles.z = buildAngle;
				tempBuilding.transform.rotation = Quaternion.Euler(0.0f,0.0f, buildAngle);
				tempBuilding.transform.position = closestPlanet.buildPoint.transform.position;
				gridSlot = angleSnap / closestPlanet.snapFactor;
				if (gridSlot == closestPlanet.buildingGrid.Length) gridSlot = 0; //Prevents array index out of range
				withinRange = true;
			}
	}

	public bool CanPlaceBuilding() {
		bool canPlace = true; 
		if(!closestPlanet.HasConstruct())
			canPlace = false;
		if(!withinRange) 
			canPlace = false;
		if(!(closestPlanet.buildingGrid [gridSlot] == null))
			canPlace = false;
		//*** Additional PlacementRestrictions goes here //
		return canPlace;}

	public string ErrorMessage() {
		string errorMessage = null;
		if(closestPlanet.hasConstruct == false)   //Should be switch? Also needs some way of sorting by priority. 
			errorMessage = "No construct available";
		if (withinRange == false)
			errorMessage = "Invalid build location";
		if(!(closestPlanet.buildingGrid [gridSlot] == null))
			errorMessage = "Building space already occupied";
		return errorMessage;
	}

	public void StartConstruction() {
		tempBuilding.transform.gameObject.GetComponent<Renderer>().material = someMaterial;
		//WorldObject script = tempBuilding.GetComponent<WorldObject>()
		tempBuilding.GetComponent<WorldObject>().enabled = true;
		findingPlacement = false;
		//Buildings buildings = GetComponentInChildren< Buildings >();
		PlanetInfo buildings = closestPlanet;
		if(buildings) tempBuilding.transform.parent = buildings.transform;
		tempBuilding.SetPlayer();
		//tempBuilding.SetColliders();
		closestPlanet.buildingGrid [gridSlot] = tempBuilding;
		tempBuilding.StartConstruction();
		tempBuilding.Position (gridSlot, closestPlanet.snapFactor);
	}

	public void CancelBuildingPlacement() {
		findingPlacement = false;
		Destroy(tempBuilding.gameObject);
		tempBuilding = null;
		//tempCreator = null;
	}

	public GameObject FindClosestPlanet() {
		Debug.Log ("public GameObject FindClosestPlanet()");
		GameObject[] planetTag;
		planetTag = GameObject.FindGameObjectsWithTag("PlntGwTrgt");
		GameObject closestPlanet = null;
		float closestDistance = Mathf.Infinity; 
		Vector2 pos = activeCamera.WorldToScreenPoint (transform.position);
		Vector3 position = new Vector3 (Input.mousePosition.x - pos.x,Input.mousePosition.y - pos.y,0); //relativeMousePos
		foreach (GameObject planet in planetTag) {
			Vector3 deltaPos = planet.transform.position - position;
			float curDistance = deltaPos.sqrMagnitude;
			if (curDistance < closestDistance) {
				closestPlanet = planet;
				closestDistance = curDistance;
			} 
		} 
		return closestPlanet;		
	}

	public Camera SetActiveCamera(Camera setActiveCamera) {
		activeCamera = setActiveCamera;
		//Debug.Log (activeCamera + activeCamera.transform.root.name);
		return activeCamera;
	}
	//void OnGUI (){GUI.Box (new Rect (10, 40, 200, 25), "Closest planet: " + FindClosestPlanet().name);} //Debugtool



}