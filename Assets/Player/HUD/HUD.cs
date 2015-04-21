using UnityEngine;
using System.Collections;
//using UnityEngine.UI;
using ORBITALRAIN;

public class HUD : MonoBehaviour {

	/*public Texture2D selectionHighLight = null;
	public static Rect selection = new Rect(0, 0, 0, 0);
	private Vector3 startClick = -Vector3.one; //Change for 2D?

	private void Update()
	{
		CheckCamera ();
	}

	private void CheckCamera() {
		if (Input.GetMouseButtonDown (0))
			startClick = Input.mousePosition;
		else if (Input.GetMouseButtonUp(0)){
			if (selection.width < 0){
				selection.x += selection.width;
				selection.width = -selection.width;}
			if (selection.height < 0){
				selection.y +=selection.height;
				selection.height = -selection.height;}
			startClick = -Vector3.one
	}*/

	public GUISkin resourceSkin, ordersSkin, selectBoxSkin;
	public Texture2D buttonHover, buttonClick, cursorTopBot, cursorLeftRight, cursorDiagonalPos, cursorDiagonalNeg;
	public GameObject building; //Needs rework for security level
	//public Text hitObject;
	//public GameObject hit;

	private WorldObject lastSelection;
	private float sliderValue;	
	//private Player player;


	/*private const int ORDERS_BAR_HEIGHT = 150, RESOURCE_BAR_HEIGHT = 40;
	private const int SELECTION_NAME_HEIGHT = 15;
	private const int BUILD_IMAGE_WIDTH = 64, BUILD_IMAGE_HEIGHT = 64;
	private const int BUTTON_SPACING = 7;
	private const int SCROLL_BAR_WIDTH = 22;*/

	// Use this for initialization
	void Start () {
		//player = tra nsform.root.GetComponent < Player > ();
		ResourceManager.StoreSelectBoxItems (selectBoxSkin);
		//buildAreaHeight = Screen.height - ORDERS_BAR_HEIGHT - SELECTION_NAME_HEIGHT - 2 * BUTTON_SPACING; 
	}


	// Update is called once per frame
	void OnGUI () {
		/*if(player && player.human) {
			//DrawOrdersBar();
		}*/
	}

	//Checks if mouse is within a defined area, also prevents unwanted UI interaction
	//Possible omit when UI is changed.
	public bool MouseInBounds() {
		Vector3 mousePos = Input.mousePosition;
		bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.width;
		bool insideHeight = mousePos.y >= 0 && mousePos.y <= Screen.height;
		return insideWidth && insideHeight;
	}

	public Rect GetPlayingArea() {
		return new Rect(0, 0, Screen.width, Screen.height);
	}

	public void ChangeCursor(string condition) {
		if (condition == null) {Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
		} else {
			if (condition == "up" || condition == "down") {
				Cursor.SetCursor(cursorTopBot, new Vector2 (4,13), CursorMode.ForceSoftware);}			
			if(condition == "left" || condition == "right"){
				Cursor.SetCursor(cursorLeftRight, new Vector2 (5,13),CursorMode.ForceSoftware);}
			if (condition == "topLeft" || condition == "bottomRight"){
				Cursor.SetCursor(cursorDiagonalNeg, new Vector2 (4,13), CursorMode.ForceSoftware);}
			if (condition == "topRight" || condition == "bottomLeft"){
				Cursor.SetCursor(cursorDiagonalPos, new Vector2 (4,13), CursorMode.ForceSoftware);}
		}
	}









	
	//Possible omit when UI is changed.
	/*private void DrawOrdersBar() { 
		GUI.skin = ordersSkin;
		GUI.BeginGroup(new Rect(Screen.width-ORDERS_BAR_HEIGHT-BUILD_IMAGE_WIDTH,0,ORDERS_BAR_HEIGHT + BUILD_IMAGE_WIDTH,Screen.height));
		GUI.Box(new Rect(BUILD_IMAGE_WIDTH+SCROLL_BAR_WIDTH,0 , ORDERS_BAR_HEIGHT, Screen.height),"");
		//Needs ownercheck
		DrawActions(GameObjectList.actions);
		GUI.EndGroup(); 
	}*/

	//Draws buttons for building
	/*public void DrawActions(string[] actions) {
		GUIStyle buttons = new GUIStyle();
		buttons.hover.background = buttonHover;
		buttons.active.background = buttonClick;
		GUI.skin.button = buttons;
		int numActions = actions.Length;
		//define the area to draw the actions inside
		GUI.BeginGroup(new Rect(BUILD_IMAGE_WIDTH,0,ORDERS_BAR_HEIGHT,buildAreaHeight));
		//display possible actions as buttons and handle the button click for each
		for(int i=0; i<numActions; i++) {
			int column = i % 2;
			int row = i / 2;
			Rect pos = GetButtonPos(row, column);
			Texture2D action = ResourceManager.GetBuildImage(actions[i]);
			if(action) {
				//create the button and handle the click of that button
				if(GUI.Button(pos, action)) {
					//print(GameObjectList.actions[i]);
					//print(ResourceManager.GetBuilding(actions[i]));
					building = (ResourceManager.GetBuilding(actions[i]));
					player.CreateBuilding(building);
					//build.PerformAction(actions[i]);
					//if(player.SelectedObject) player.SelectedObject.PerformAction(actions[i]);
				}
			}
		}
		GUI.EndGroup();
	}*/

	/*private int MaxNumRows(int areaHeight) {
		return areaHeight / BUILD_IMAGE_HEIGHT;}
	private Rect GetButtonPos(int row, int column) {
		int left = SCROLL_BAR_WIDTH + column * BUILD_IMAGE_WIDTH;
		float top = row * BUILD_IMAGE_HEIGHT - sliderValue * BUILD_IMAGE_HEIGHT;
		return new Rect(left, top, BUILD_IMAGE_WIDTH, BUILD_IMAGE_HEIGHT);}
	private void DrawSlider (int groupHeight, float numRows) {
		sliderValue = GUI.VerticalSlider(GetScrollPos(groupHeight), sliderValue, 0.0f, numRows - MaxNumRows(groupHeight));}
	private Rect GetScrollPos(int groupHeight) {
		return new Rect(BUTTON_SPACING, BUTTON_SPACING, SCROLL_BAR_WIDTH, groupHeight - 2 * BUTTON_SPACING);}*/
}
