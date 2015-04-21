using UnityEngine;
using System.Collections;

public class targetFollow : MonoBehaviour {

	public GameObject trackingTarget;
	Vector2 curPos;
	Vector2 newPos;


	float timerTarget;

	public float tickWait;



	// Use this for initialization
	void Start () {



		curPos = new Vector2(transform.position.x, transform.position.y);
		newPos = new Vector2(trackingTarget.transform.position.x, trackingTarget.transform.position.y);
		timerTarget = Time.time + tickWait;
	}
	
	// Update is called once per frame
	void Update () {

			timerTarget = Time.time + tickWait;
			curPos = new Vector2(transform.position.x, transform.position.y);
			newPos = new Vector2(trackingTarget.transform.position.x, trackingTarget.transform.position.y);
		
		//transform.position = Vector2.Lerp(curPos,newPos, 1 - ((timerTarget - Time.time)*(1/tickWait)));
		transform.position = Vector3.MoveTowards(curPos, newPos, .01f);
	}
}
