using UnityEngine;
using System.Collections;
using ORBITALRAIN;

namespace ORBITALRAIN {
	public static class ResourceManager {
		/*** Camera Controls ***/
		public static int ScrollWidth { get { return 15; } } //Screenborder for moving camera
		public static float ScrollSpeed { get { return 5; } } //How fast camera is moving
        public static int ScrollBoost { get { return 3; } } //Camera scroll speed boost when pressing shift
		public static float ZoomSpeed { get { return 500; } }   //How fast zooming is
		public static float MinCameraDistance { get { return 2; } }
		public static float MaxCameraDistance { get { return 200; } }       
       
        public static Vector2 HorizontalCameraClamp = new Vector2 (- 80, 80);
        public static Vector2 VerticalCameraClamp = new Vector2(-80, 80);



		/*** Clicking  ***/
		private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
		public static Vector3 InvalidPosition { get { return invalidPosition; } }

		/*** Selection ***/
		private static GUISkin selectBoxSkin;
		public static GUISkin SelectBoxSkin { get { return selectBoxSkin; } }

		public static void StoreSelectBoxItems(GUISkin skin) {selectBoxSkin = skin;}
		// Set bounds for invalid selection
		private static Bounds invalidBounds = new Bounds(new Vector3(-99999, -99999, -99999), new Vector3(0, 0, 0));
		public static Bounds InvalidBounds { get { return invalidBounds; } }

		/*** GameObjectList ***/
		/*public static void SetGameObjectList(GameObjectList objectList) {
			gameObjectList = objectList;}
		//Wrapper for accessing methods in GameObjectList;
		private static GameObjectList gameObjectList;
		public static GameObject GetBuilding(string name) {
			return gameObjectList.GetBuilding(name);}		
		public static GameObject GetWorldObject(string name) {
			return gameObjectList.GetWorldObject(name);}		
		public static GameObject GetPlayerObject() {
			return gameObjectList.GetPlayerObject();}		
		public static Texture2D GetBuildImage(string name) {
			return gameObjectList.GetBuildImage(name);}*/

		/*** Buildings ***/
		public static float distanceFromCenterLimit { get { return 100; } }

		/*** Planets ***/
		//Search closest planet
		public static float searchPlanetFrequency { get { return 0.5f; } }

		/*** Bullets ***/
		//Search distance from sun
		public static float searchOrigoFrequency { get { return 10f; } }
		public static float systemOuterLimit { get { return 100000f; } }

		/*** PiP ***/
		public static float margin { get { return 10f; } }
	}
}