using UnityEngine;
using System.Collections.Generic;

namespace ORBITALRAIN {
	public static class WorkManager {
		static string dragDirection;

		public static Rect CalculateSelectionBox(Bounds selectionBounds, Rect playingArea) {
			//shorthand for the coordinates of the centre of the selection bounds
			float cx = selectionBounds.center.x;
			float cy = selectionBounds.center.y;
			//shorthand for the coordinates of the extents of the selection bounds
			float ex = selectionBounds.extents.x;
			float ey = selectionBounds.extents.y;
			
			//Determine the screen coordinates for the corners of the selection bounds
			List< Vector3 > corners = new List< Vector3 >();
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy+ey)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy-ey)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy+ey)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy-ey)));
			corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy-ey)));
			
			//Determine the bounds on screen for the selection bounds
			Bounds screenBounds = new Bounds(corners[0], Vector3.zero);
			for(int i = 1; i < corners.Count; i++) {
				screenBounds.Encapsulate(corners[i]);}
			
			//Screen coordinates start in the bottom left corner, rather than the top left corner
			//this correction is needed to make sure the selection box is drawn in the correct place
			float selectBoxTop = playingArea.height - (screenBounds.center.y + screenBounds.extents.y);
			float selectBoxLeft = screenBounds.center.x - screenBounds.extents.x;
			float selectBoxWidth = 2 * screenBounds.extents.x;
			float selectBoxHeight = 2 * screenBounds.extents.y;
			
			return new Rect(selectBoxLeft, selectBoxTop, selectBoxWidth, selectBoxHeight);}
	
		public static GameObject FindHitObject(Vector3 origin) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(origin), Vector2.zero);
			if(hit.collider != null) return hit.collider.gameObject;
			return null;}
		public static Vector3 FindHitPoint(Vector3 origin) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(origin), Vector2.zero);
			if(hit.collider != null) return hit.point;
			return ResourceManager.InvalidPosition;}

		public static string CheckDragDir(Vector2 pos, float topBorder, float bottomBorder, float leftBorder, float rightBorder) {
			bool top, bottom, left, right, topLeft, topRight, bottomLeft, bottomRight;

			float margin = ResourceManager.margin;
			//top
			if (pos.y >= topBorder - margin/2 && pos.y <= topBorder + margin/2 && pos.x > leftBorder + margin/2 && pos.x < rightBorder - margin/2) { top = true;}
			else {top = false;}
			//bottom
			if (pos.y >= bottomBorder - margin/2 && pos.y <= bottomBorder + margin/2 && pos.x > leftBorder + margin/2 && pos.x < rightBorder - margin/2) { bottom = true;}
			else {bottom = false;}	
			//left
			if (pos.x >= leftBorder - margin/2 && pos.x <= leftBorder + margin/2 && pos.y < topBorder - margin/2 && pos.y > bottomBorder + margin/2) { left = true;}
			else {left = false;}
			//right
			if (pos.x >= rightBorder - margin/2 && pos.x <= rightBorder + margin/2 && pos.y < topBorder - margin/2 && pos.y > bottomBorder + margin/2) { right = true;} 
			else {right = false;}
			//topLeft
			if (pos.y >= topBorder - margin/2 && pos.y <= topBorder + margin/2 && pos.x >= leftBorder - margin/2 && pos.x <= leftBorder + margin/2) { topLeft = true;}
			else {topLeft = false;}
			//topRight
			if (pos.y >= topBorder - margin/2 && pos.y <= topBorder + margin/2 && pos.x >= rightBorder - margin/2 && pos.x <= rightBorder + margin/2) {	topRight = true;}
			else { topRight = false;}
			//bottomLeft
			if (pos.y >= bottomBorder - margin/2 && pos.y <= bottomBorder + margin/2 && pos.x >= leftBorder - margin/2 && pos.x <= leftBorder + margin/2) {	bottomLeft = true;}
			else { bottomLeft = false;}
			//bottomRight
			if (pos.y >= bottomBorder - margin/2 && pos.y <= bottomBorder + margin/2 && pos.y >= topBorder - margin/2 && pos.y <= topBorder + margin/2) {	bottomRight = true;}
			else { bottomRight = false;}
			
			if (top) {dragDirection = "up";}
			if (bottom) {dragDirection = "down";}
			if (left){dragDirection = "left";}
			if (right){dragDirection = "right";}
			if (topLeft){dragDirection = "topLeft";}
			if (topRight){dragDirection = "topRight";}
			if (bottomLeft){dragDirection = "bottomLeft";}
			if (bottomRight){dragDirection = "bottomRight";}
			if (!top && !bottom && !left && !right && !topLeft && !topRight && !bottomLeft && !bottomRight) {dragDirection = null;}
			
			return dragDirection;
		}

		public static void InstantiatePiP(GameObject target) {
			Debug.Log ("InstantiatePiP on " + target.name);
		}

        public static float ZoomBoost(float zoomLevel, float[] zoomIntervall, float[] zoomBoost, float zoomBost){
            if (zoomLevel >= 0 && zoomLevel <= zoomIntervall[0]) { zoomBost = zoomBoost[0]; }
            else if (zoomLevel > zoomIntervall[0] && zoomLevel <= zoomIntervall[1]) { zoomBost = zoomBoost[1]; }
            else if (zoomLevel > zoomIntervall[1] && zoomLevel <= zoomIntervall[2]) { zoomBost = zoomBoost[2]; }
            else if (zoomLevel > zoomIntervall[2] && zoomLevel <= zoomIntervall[3]) { zoomBost = zoomBoost[3]; }
            else if (zoomLevel > zoomIntervall[2] && zoomLevel <= zoomIntervall[3]) { zoomBost = zoomBoost[4]; }
            else if (zoomLevel > zoomIntervall[3] && zoomLevel <= zoomIntervall[4]) { zoomBost = zoomBoost[4]; }
            else if (zoomLevel > zoomIntervall[4]) { zoomBost = zoomBoost[5]; }
            return zoomBost;
        }

        public static float PanBoost(float zoomLevel, float[] panIntervall, float[] panBoost, float panBost)
        {
            if (zoomLevel >= 0 && zoomLevel <= panIntervall[0]) { panBost = panBoost[0]; }
            else if (zoomLevel > panIntervall[0] && zoomLevel <= panIntervall[1]) { panBost = panBoost[1]; }
            else if (zoomLevel > panIntervall[1] && zoomLevel <= panIntervall[2]) { panBost = panBoost[2]; }
            else if (zoomLevel > panIntervall[2] && zoomLevel <= panIntervall[3]) { panBost = panBoost[3]; }
            else if (zoomLevel > panIntervall[2] && zoomLevel <= panIntervall[3]) { panBost = panBoost[4]; }
            else if (zoomLevel > panIntervall[3] && zoomLevel <= panIntervall[4]) { panBost = panBoost[4]; }
            else if (zoomLevel > panIntervall[4]) { panBost = panBoost[5]; }
            return panBost;
        }
    }
}