using UnityEngine;
using System.Collections;
using ORBITALRAIN;

using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class CelestialBody : WorldObject, IBoxSelectable{

    #region Implemented members of IBoxSelectable
    bool _selected = false;
    public bool selected { get { return _selected; } set { _selected = value; } }
    bool _preSelected = false;
    public bool preSelected { get { return _preSelected; } set { _preSelected = value; } }
    #endregion 

    public bool gravity = true;
    public double mass = 10000;
    public Vector2 startForce;




    protected virtual void Awake(){
        if (gravity){
            if (!gameObject.GetComponent<Rigidbody2D>()){
                gameObject.AddComponent<Rigidbody2D>();
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;}            
            gameObject.AddComponent<HiggsBoson>();
        }
    }

	// Use this for initialization
	void Start () {
        /*//Ottis
        switch (objectIdentity)
        {
            case "Sun_Sprite":
                gameObject.tag = "SunGwTrgt";
                break;
            case "Planet_Sprite":
                gameObject.tag = "PlntGwTrgt";
                break;
        }
        if (isTarget)
        {
            planet = Instantiate(Resources.Load("Prefabs/" + objectIdentity), transform.position, Quaternion.identity) as GameObject;
            planet.GetComponent<targetFollow>().trackingTarget = gameObject;
            finishedSpawning = true;
        }*/


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
