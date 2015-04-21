using UnityEngine;
using System.Collections;
using ORBITALRAIN;

using UnityEngine.UI;

public class UserInput : MonoBehaviour {

	//** Look out for backdrop "Ground", the 2D collider and placing buildings.
    [HideInInspector]
    public Camera activeCamera;
	public bool clampCamera;
	public GameObject pipPrefab, pipPanel; //Needs to find this automatically
    public float[] zoomIntervall = new float[5];
        public float[] zoomBoost = new float[6];
    public float[] panIntervall = new float[5]; 
        public float[] panBoost = new float[6];

    private float zoomWeight, scrollWeight, panWeight, arrowScrollWeight;
    private Player player;
    private GameObject trackObject = null;
	private bool doOnce = true, tracking = false;

	// Use this for initialization
	void Start () {
		player = transform.root.GetComponent<Player>();
		activeCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if(player && player.human) {
			CameraControl();
			MouseActivity();
            KeyboardActivity();
		}
	}
	
	//////*** Camera Control ***\\\\\\
	/// 					       \\\
	/// 						   \\\
	///////////////  \\\\\\\\\\\\\\\\\

	private void CameraControl() {
		//Debug.Log ("UserInput.cs " + activeCamera.name);
		float xPos = Input.mousePosition.x;
		float yPos = Input.mousePosition.y;
		Vector2 movement = new Vector2 (0, 0);
        Vector2 panSpeed = new Vector2 (0, 0);

		//horizontal camera movement
		if(xPos >= 0 && xPos < ResourceManager.ScrollWidth) { 
			movement.x -= ResourceManager.ScrollSpeed;
            panSpeed.x = Mathf.Abs((ResourceManager.ScrollWidth - xPos) / ResourceManager.ScrollWidth)+1;
		} else if(xPos <= Screen.width && xPos > Screen.width - ResourceManager.ScrollWidth) { 
			movement.x += ResourceManager.ScrollSpeed;
            panSpeed.x = (xPos - Screen.width) / ResourceManager.ScrollWidth+2;
		}
		
		//vertical camera movement
		if(yPos >= 0 && yPos < ResourceManager.ScrollWidth) { 
			movement.y -= ResourceManager.ScrollSpeed;
            panSpeed.y = Mathf.Abs((ResourceManager.ScrollWidth - yPos) / ResourceManager.ScrollWidth) + 1;
		} else if(yPos <= Screen.height && yPos > Screen.height - ResourceManager.ScrollWidth) {
			movement.y += ResourceManager.ScrollSpeed;
            panSpeed.y = (yPos - Screen.height) / ResourceManager.ScrollWidth + 2;
		}

		//calculate desired camera position based on received input
		Vector3 origin = Camera.main.transform.position;
		Vector3 destination = origin;
		destination.x += movement.x;
		destination.y += movement.y;        

		//if a change in position is detected perform the necessary update
		if(destination != origin) {
            if (tracking) {tracking = !tracking;}
            scrollWeight = WorkManager.PanBoost(activeCamera.orthographicSize, panIntervall, panBoost, scrollWeight);
            int scrollBoost = ResourceManager.ScrollBoost/ResourceManager.ScrollBoost;
            if (Input.GetKey(KeyCode.LeftShift)) {scrollBoost = ResourceManager.ScrollBoost;}
			Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed * scrollBoost * zoomWeight * scrollWeight);
		}
        
        //clamps camera position
        if (clampCamera) {
            Vector2 camPos = activeCamera.transform.position;
            camPos.x = Mathf.Clamp(camPos.x, ResourceManager.HorizontalCameraClamp.x, ResourceManager.HorizontalCameraClamp.y);
            camPos.y = Mathf.Clamp(camPos.y, ResourceManager.VerticalCameraClamp.x, ResourceManager.VerticalCameraClamp.y);
            activeCamera.transform.position = camPos;}


		//changes orthographic camera size (zoom)
		if (!Input.GetKey(KeyCode.LeftControl)) {
            zoomWeight = WorkManager.ZoomBoost(activeCamera.orthographicSize, zoomIntervall, zoomBoost, zoomWeight); 	
            activeCamera.orthographicSize = activeCamera.orthographicSize - (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ResourceManager.ZoomSpeed * zoomWeight);
            activeCamera.orthographicSize = Mathf.Clamp(activeCamera.orthographicSize, ResourceManager.MinCameraDistance, ResourceManager.MaxCameraDistance);
            if (Input.GetAxis("Mouse ScrollWheel") > 0) {
                if (tracking) { tracking = !tracking; }
                Vector3 zoomTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float multiplier = (1.0f / Camera.main.orthographicSize) * zoomWeight;
                activeCamera.transform.position += (zoomTowards - activeCamera.transform.position) * multiplier;}
        } else if (Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.DownArrow) || (Input.GetAxis("Mouse ScrollWheel") != 0)) || Input.GetKey(KeyCode.Q)) {
			if (doOnce) {
				GameObject hitObject = WorkManager.FindHitObject(Input.mousePosition);
				Vector2 hitPoint = WorkManager.FindHitPoint(Input.mousePosition);
				if(hitObject != null && hitObject.name != "Ground") {
					InstantiatePiP(hitObject, hitPoint);
			doOnce = false;	}}}
        if(Input.GetKeyUp(KeyCode.LeftControl) || (Input.GetAxis("Mouse ScrollWheel") == 0)) { // Fix scrollwheel thingy
			doOnce = true;}

        //Camera follows track object
        if (tracking){
            Vector3 trackPos = new Vector3(trackObject.transform.position.x, trackObject.transform.position.y, -12);
            Camera.main.transform.position = trackPos;}


        //Middle mouse button pan
        if( Input.GetMouseButton(2) ) {
            float panWeight = 0.2f * activeCamera.orthographicSize;
			float xAxis = -Input.GetAxis("Mouse X") * panWeight;
			float yAxis = -Input.GetAxis("Mouse Y") * panWeight;
            Camera.main.transform.position += new Vector3(xAxis, yAxis,0);
        }

        //keyboard scroll
        if(true){
            float arrowScrollWeight = 0.05f * activeCamera.orthographicSize;
            if(Input.GetKey(KeyCode.LeftArrow)) {
                Camera.main.transform.position += new Vector3(-1 * arrowScrollWeight,0,0);
                if (tracking) { tracking = !tracking; }}
            if(Input.GetKey(KeyCode.RightArrow)) {
                Camera.main.transform.position += new Vector3(1 * arrowScrollWeight,0,0);}
                if (tracking) { tracking = !tracking; }
            if(Input.GetKey(KeyCode.UpArrow)) {
                Camera.main.transform.position += new Vector3(0,1 * arrowScrollWeight,0);}
                if (tracking) { tracking = !tracking; }
            if(Input.GetKey(KeyCode.DownArrow)) {
                Camera.main.transform.position += new Vector3(0,-1 * arrowScrollWeight,0);}
                if (tracking) { tracking = !tracking; }
            }
        }
    
	public Camera SetActiveCamera(Camera setActiveCamera) {
		activeCamera = setActiveCamera;
		//Debug.Log (activeCamera + activeCamera.transform.root.name);
		return activeCamera;
	}


    

	//////*** Mouse Activity ***\\\\\\
	/// 					       \\\
	/// 						   \\\
	///////////////  \\\\\\\\\\\\\\\\\



	//Detects a mouseclick and calls its respective method
	private void MouseActivity() {
		if(Input.GetMouseButtonDown(0)) LeftMouseClick();
		else if(Input.GetMouseButtonDown(1)) RightMouseClick();
        if (Input.GetMouseButtonDown(2)) MiddleMouseClick();
		MouseHover ();
	}

	/*** Left MouseClick ***/
	private void LeftMouseClick(){ 
		if(player.hud.MouseInBounds()) {  //Prevents accidental in-world click when interacting with
			if(player.IsFindingBuildingLocation()) {
				if(player.CanPlaceBuilding()) { player.StartConstruction(); 
				} else {print(player.ErrorMessage());}
			} else {	
				GameObject hitObject = WorkManager.FindHitObject(Input.mousePosition); //What object is clicked
				Vector3 hitPoint = WorkManager.FindHitPoint(Input.mousePosition); //What point is clicked
				if(hitObject && hitPoint != ResourceManager.InvalidPosition) {
					if(player.SelectedObject) player.SelectedObject.MouseClick(hitObject, hitPoint, player);
					else if(hitObject.name!="Ground") {
						WorldObject worldObject = hitObject.GetComponent< WorldObject >(); 
						if(worldObject) {
							player.SelectedObject = worldObject;
							worldObject.SetSelection(true, player.hud.GetPlayingArea());
						}
					}
				}
			}
		} 
	}

	/*** MouseHover ***/
	private void MouseHover() { //Try to get this working, info about the Object
		if(player.hud.MouseInBounds()) {
			if(player.IsFindingBuildingLocation()) {
				player.FindBuildingLocation();
			} else { 
				GameObject hoverObject = WorkManager.FindHitObject(Input.mousePosition);
				if(hoverObject) {
					if(player.SelectedObject) player.SelectedObject.SetHoverState(hoverObject);
					else if(hoverObject.name != "Ground") {
						Player owner = hoverObject.transform.root.GetComponent <Player>();
						if(owner) {
							//Building building = hoverObject.transform.parent.GetComponent<Building>();
						}
					}
				}
			}
		} 
	} 
	
	/*** Right MouseClick ***/
	private void RightMouseClick() {
		if(player.hud.MouseInBounds())
			if(player.IsFindingBuildingLocation()) {
				player.CancelBuildingPlacement();
			} else {
				player.SelectedObject.SetSelection(false, player.hud.GetPlayingArea());
			player.SelectedObject = null;
		}
	}

    /*** Middle MouseClick ***/
    private void MiddleMouseClick() {
        if(player.hud.MouseInBounds()) {  //Prevents accidental in-world click when interacting with
		    GameObject hitObject = WorkManager.FindHitObject(Input.mousePosition); //What object is clicked
            if (hitObject) {
                tracking = true;
                trackObject = hitObject;
            }		   
	    }
    }











    //////*** Keyboard Activity ***\\\\\\
    /// 					           \\\
    /// 					            \\\
    //////////////////  \\\\\\\\\\\\\\\\\\\\



    //Detects a keyboard stroke and calls its respective method
    private void KeyboardActivity(){
        if (Input.GetKeyDown(KeyCode.Escape)) Escape();
    }

    /*** Escape ***/
    private void Escape(){
        tracking = false;
    }












	//////***   Additional   ***\\\\\\
	/// 					       \\\
	/// 						   \\\
	///////////////  \\\\\\\\\\\\\\\\\




	// Some clean-up needed
	public void InstantiatePiP(GameObject target, Vector2 hitPoint) {
			WorldObject worldObject = target.GetComponent<WorldObject>();
			if(!worldObject) {return; //don't be so picky -- search closest planet/building?
			} else if (worldObject.transform.root.transform.FindChild("Planet UI").FindChild("pipPanel(Clone)") == null)
			{ if (worldObject.objectType == "Planet") {		
					GameObject pipCamera = (GameObject)Instantiate(pipPrefab);
					pipCamera.transform.parent = target.transform.FindChild("Planet UI").transform;
					pipCamera.transform.position = new Vector3 (hitPoint.x, hitPoint.y, -2);
					GameObject planetUI = (GameObject)Instantiate(pipPanel);
					planetUI.transform.parent = target.transform.FindChild("Planet UI").transform;
					planetUI.transform.localPosition = new Vector3 (0,0,-10);
				} else if (worldObject.objectType == "Building") { //Currently not working
					GameObject pipCamera = (GameObject)Instantiate(pipPrefab);
					pipCamera.transform.parent = target.transform.parent.FindChild("Planet UI").transform;
					pipCamera.transform.position = new Vector3 (hitPoint.x, hitPoint.y, -2);
					GameObject planetUI = (GameObject)Instantiate(pipPanel);
					planetUI.transform.parent.parent = target.transform.FindChild("Planet UI").transform;
					planetUI.transform.localPosition = new Vector3 (0,0,-10);
				} else { Debug.Log("Unable to find valid PiP-parent");}}
		}
}

