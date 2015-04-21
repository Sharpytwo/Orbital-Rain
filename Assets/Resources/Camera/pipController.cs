using UnityEngine;
using System.Collections;
using ORBITALRAIN;

public class pipController : MonoBehaviour {

	masterCamera masterCamera;

	Vector2 viewPortSize = new Vector2 (0.2f, 0.3f); //Get from pipPanelController
	float margin = 10.0f;
	public Texture2D cursorTopBot, cursorLeftRight, cursorDiagonalPos, cursorDiagonalNeg; 
	bool top, bottom, left, right, normal = false;
	bool lockedPos = true;
	string dragDirection;
	public HUD hud;
	
	// Use this for initialization
	void Start () {
		masterCamera = Camera.main.GetComponent<masterCamera> ();
		GameObject HUDObject = GameObject.Find ("HUD");
		hud = (HUD)HUDObject.GetComponent (typeof(HUD));
	}
	
	// Update is called once per frame
	void Update () {
		MoveViewport ();
	
		float topBorder = GetComponent<Camera>().pixelRect.yMax;
		float bottomBorder = GetComponent<Camera>().pixelRect.yMin;
		float leftBorder = GetComponent<Camera>().pixelRect.xMin;
		float rightBorder = GetComponent<Camera>().pixelRect.xMax;
		
		Vector2 pos = Input.mousePosition; //mousePosition on screen
		//Debug.Log ("pipController.cs withinPip " + camera.name + " " + camera.pixelRect.Contains(pos));
		if(GetComponent<Camera>().pixelRect.Contains(pos)) {
			masterCamera.WithinBounds(true);
			masterCamera.SetActiveCamera(GetComponent<Camera>());
		}else {masterCamera.WithinBounds (false);}

		/*if (pos.y < topBorder + margin*2 && pos.y > bottomBorder - margin*2 && pos.x > leftBorder - margin*2 && pos.x < rightBorder + margin*2) {
			dragDirection = WorkManager.CheckDragDir(pos, topBorder, bottomBorder, leftBorder, rightBorder);
			hud.ChangeCursor(dragDirection);
			if (camera.pixelRect.Contains(Input.mousePosition)) {
				Debug.Log ("PiP active");}*/
	}	
		
		
	/* Code for translating string dragDirection to vector
		if (top) {dragDirection = new Vector2 (0,1);
			Cursor.SetCursor(cursorTopBot, new Vector2 (4,13), CursorMode.ForceSoftware);}			
		if (bottom) {dragDirection = new Vector2 (0,-1);
			Cursor.SetCursor(cursorTopBot, new Vector2 (4,13),CursorMode.ForceSoftware);}
		if(left){dragDirection = new Vector2 (-1,0);
			Cursor.SetCursor(cursorLeftRight, new Vector2 (5,13),CursorMode.ForceSoftware);}
		if (right){dragDirection = new Vector2 (1,0);
			Cursor.SetCursor(cursorLeftRight, new Vector2 (5,13),CursorMode.ForceSoftware);}
		if (top && left){dragDirection = new Vector2 (1,-1);
			Cursor.SetCursor(cursorDiagonalNeg, new Vector2 (4,13), CursorMode.ForceSoftware);}
		if (top && right){dragDirection = new Vector2 (1,1);
			Cursor.SetCursor(cursorDiagonalPos, new Vector2 (4,13), CursorMode.ForceSoftware);}
		if (bottom && left){dragDirection = new Vector2 (-1,-1);
			Cursor.SetCursor(cursorDiagonalPos, new Vector2 (4,13), CursorMode.ForceSoftware);}
		if (bottom && right){dragDirection = new Vector2 (-1,1);
			Cursor.SetCursor(cursorDiagonalNeg, new Vector2 (4,13), CursorMode.ForceSoftware);}
		if (!top && !bottom && !left && !right) {
			Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);}
		return dragDirection;
	} */
	

	void MoveViewport() {
		Vector2 panelScreenPos = transform.parent.transform.GetComponentInChildren<pipPanelController>().RectTransformScreenPos();
		Vector2 cameraViewPortOffset = new Vector2 ((0.5f * viewPortSize.x * Screen.width) / Screen.width, (0.5f * viewPortSize.y * Screen.height) / Screen.height);
		GetComponent<Camera>().rect = new Rect (panelScreenPos.x - cameraViewPortOffset.x, panelScreenPos.y - cameraViewPortOffset.y, viewPortSize.x, viewPortSize.y);
	}

	public void Close() {
		Destroy (gameObject);
	}

	/*void OnGUI() {
		Vector2 pos = Camera.main.WorldToViewportPoint (gameObject.transform.parent.transform.position);
		float xPos = pos.x * Screen.width;
		float yPos = Screen.height - (pos.y * Screen.height);
		if (GUI.Button(new Rect(xPos + viewPortSize/1.7f * Screen.width * 0.5f - 55, yPos - viewPortSize * Screen.height * 0.5f + 5, 50, 30), "Close"))
			Destroy(gameObject);
	}*/
}
