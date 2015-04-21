using UnityEngine;
using System.Collections;
using ORBITALRAIN;

public class GameObjectList : MonoBehaviour {

	/*** BUILDINGS ***/

	//public static string[] actions = {"Sphere","Cube","Cannon","Constructor","mf"}; //Note: needs auto-naming script

	public GameObject[] buildings;
	public GameObject[] worldObjects;
	public GameObject player;

	private static bool created = false; //Prevents double creation of GameObjectList

	void Awake() {
		/*** Prevents multiple loads of script ***/
		if(!created) {
			DontDestroyOnLoad(transform.gameObject); //GameObjectList only needs to be initialised once.
			//ResourceManager.SetGameObjectList(this);
			created = true;
		} else {
			Destroy(this.gameObject);  //IF double creation of GameObjectList happens, then it is destroyed.
		}
	}

	/*//GetBuilding
	public GameObject GetBuilding(string name) {
		for(int i = 0; i < buildings.Length; i++) { //Checks for all values of i+1 element in < 0, numberOfBuildings >
			Building building = buildings[i].GetComponent< Building >(); //building[i] is building number i in an array.
			if(building && building.name == name) return buildings[i];} //returns building[i] if check is ok.
		return null;}													//else returns null
	//GetWorldObject
	public GameObject GetWorldObject(string name) {
		foreach(GameObject worldObject in worldObjects) {
			if(worldObject.name == name) return worldObject;}
		return null;}	
	//GetPlayerObjet
	public GameObject GetPlayerObject() {
		return player;}	
	//GetBuildImage
	public Texture2D GetBuildImage(string name) {
		for(int i = 0; i < buildings.Length; i++) {
			Building building = buildings[i].GetComponent< Building >();
			if(building && building.name == name) return building.buildImage;}
		return null;}*/
}
