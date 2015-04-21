using UnityEngine;
using System.Collections;

public class masterCamera : MonoBehaviour {
	
	Camera activeCamera;
	UserInput userInput;
	Player player;
	bool withinPiP;
	
	// Use this for initialization
	void Start () {
		activeCamera = Camera.main;
		userInput = GameObject.Find ("Player").GetComponent<UserInput> ();
		player = GameObject.Find ("Player").GetComponent<Player> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!withinPiP) {
			activeCamera = Camera.main;
			userInput.SetActiveCamera(activeCamera);
			player.SetActiveCamera(activeCamera);}
	}
	
	public void SetActiveCamera(Camera newCurActiveCamera)  {
		activeCamera = newCurActiveCamera;
		userInput.SetActiveCamera (activeCamera);
		player.SetActiveCamera (activeCamera);
	}		
	
	public Camera GetActiveCamera() {
		return activeCamera;
	}

	public bool WithinBounds(bool within) {
		withinPiP = within;
		return withinPiP;
	}
}