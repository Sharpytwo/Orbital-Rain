using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using DG.Tweening;

public class pipPanelController : MonoBehaviour {

	private RectTransform rectTransform, viewPortRect;
	private float initCameraOrthographicSize = 20.0f;
	private float localScale, yPosOffset, prevCameraOrthographicSize;
	private float zoomRate = 1.0f;
	private bool lockedOnParent;

	Button snapButton;

	void Awake() {
		lockedOnParent = true;
		
		snapButton = transform.FindChild ("snapButton").GetComponent<Button>();
		snapButton.interactable = false;

		initCameraOrthographicSize = Camera.main.orthographicSize;
		prevCameraOrthographicSize = initCameraOrthographicSize;

		rectTransform = GetComponent <RectTransform> (); 
		viewPortRect = rectTransform.transform.FindChild ("ViewPortRect").GetComponent<RectTransform> ();
	}

	void Start () {
		rectTransform.anchoredPosition = new Vector2 (0, 0);
		zoomRate = Camera.main.orthographicSize / 20;
		rectTransform.localScale = new Vector2 (0.11f*zoomRate + zoomRate, zoomRate);
		
		//Tweener tweener = transform.DOMove (planet.transform.position, 1).SetSpeedBased (); 
		//tweener.OnUpdate (() => tweener.ChangeEndValue (planet.transform.position, true));
	}

	void Update () {
		if (lockedOnParent) {	snapButton.interactable = false;}
		else { snapButton.interactable = true;}

		if (Camera.main.orthographicSize != prevCameraOrthographicSize) {

			//** Changes size of rectTransform when zooming
			zoomRate = Camera.main.orthographicSize / 20;
			rectTransform.localScale = new Vector2(0.11f*zoomRate+zoomRate,zoomRate);

			/*//** Places center of ViewPortRect on followObject
			float yPosOffset = viewPortRect.anchoredPosition.y * rectTransform.localScale.y;
			rectTransform.anchoredPosition = new Vector2 (0, -yPosOffset);*/
		}
		prevCameraOrthographicSize = Camera.main.orthographicSize;
	}

	public void ToggleLockedToParent (){
		lockedOnParent = !lockedOnParent;
		if (lockedOnParent) rectTransform.anchoredPosition = Vector2.zero;
	}

	public bool IsLockedToParent() {
		return lockedOnParent;
	}


	//*** Methods for pipCamera.cs ***\\
	/// 						     \\\
	/// 						     \\\
	///////////////    \\\\\\\\\\\\\\\\\
	
	public Vector2 RectTransformScreenPos() {
		Vector2 screenPos = Camera.main.WorldToViewportPoint(viewPortRect.position);
		return screenPos;
	}

	public Vector2 ViewPortRectSize() {	
		Vector2 viewPortRectSize = new Vector2 (viewPortRect.rect.width * viewPortRect.localScale.x, viewPortRect.rect.height * viewPortRect.localScale.y);
		return viewPortRectSize;			
	}
	
	public void Close() {
		transform.parent.parent.transform.GetComponentInChildren<pipController>().Close();
		Destroy (gameObject);
	}

}
