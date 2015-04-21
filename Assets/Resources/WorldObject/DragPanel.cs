using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler {

	private Vector2 pointerOffset;
	private RectTransform canvasRectTransform, panelRectTransform;
	private pipPanelController pipPanel;
	
	void Start () {
		Canvas canvas = GetComponentInParent <Canvas>();
		if (canvas != null) {
			canvasRectTransform = canvas.transform as RectTransform;
			panelRectTransform = transform.parent as RectTransform;
		}
		pipPanel = transform.parent.GetComponent<pipPanelController> (); 
	}
	
	public void OnPointerDown (PointerEventData data) {
		panelRectTransform.SetAsLastSibling ();
		RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);
		if(pipPanel.IsLockedToParent()) {
			pipPanel.ToggleLockedToParent ();}
	}
	
	public void OnDrag (PointerEventData data) {
		if (panelRectTransform == null)
			return;

		Vector2 pointerPosition = ClampToWindow (data);
		
		Vector2 localPointerPosition = pointerPosition;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (canvasRectTransform, pointerPosition, data.pressEventCamera, out localPointerPosition)) 
		{
			panelRectTransform.localPosition = localPointerPosition - new Vector2 (pointerOffset.x - 10, pointerOffset.y + 92); //Find a better solution to offset!
		}
	}

	Vector2 ClampToWindow (PointerEventData data) {
		/*Vector2 rawPointerPosition = data.position;
		
		Vector3[] canvasCorners = new Vector3[4];
		canvasRectTransform.GetWorldCorners (canvasCorners);
		
		float clampedX = Mathf.Clamp (rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
		float clampedY = Mathf.Clamp (rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);
		
		Vector2 newPointerPosition = new Vector2 (clampedX, clampedY);*/
		Vector2 newPointerPosition = Input.mousePosition;
		return newPointerPosition;
	}
}
