using UnityEngine;
using System.Collections;
using ORBITALRAIN;

using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class PlanetInfo : CelestialBody { 

	[HideInInspector]
	public GameObject pivot, buildPoint;
	[HideInInspector]
    public bool hasConstruct = false, builderInPlace = false, activateConstructor = true;
	[HideInInspector]
	public Building[] buildingGrid;

    public int snapFactor = 18; //Needs auto-set based on plane size

    private string objectIdentity;
    private bool isTarget, finishedSpawning;
    private GameObject planet;
       
    // Use this for initialization
    public void Start(){
        gameObject.tag = "PlntGwTrgt";
        objectType = "Planet";
        isDestructable = false;
        int buildSlots = 360 / snapFactor;
        //float planetRadius = GetComponent<CircleCollider2D>().radius;
        //Vector3 planetPos = gameObject.transform.position;
        //buildPoint.transform.position = new Vector3(planetPos.x + planetRadius, planetPos.y,1);
        buildingGrid = new Building[buildSlots];
    }

    // Update is called once per frame
    public void Update()
    {
        //Debug.Log ("Position of " + gameObject.name + " is: " + Camera.main.WorldToViewportPoint(gameObject.transform.position));
        if (activateConstructor == true && hasConstruct == false)
        {
            InitiateLeprechaun();
            hasConstruct = true;
        }
        //if (selected) Debug.Log (gameObject.name);

        //Ottis
        //position = transform.position;
        if (finishedSpawning && planet == null)
            Object.Destroy(gameObject, 0.05f);
    }

    public void InitiateLeprechaun()
    {
        pivot = new GameObject();
        buildPoint = new GameObject();
        pivot.transform.parent = gameObject.transform;
        pivot.transform.position = gameObject.transform.position;
        buildPoint.transform.parent = pivot.transform;
        float planetRadius = GetComponent<CircleCollider2D>().radius;
        Vector3 planetPos = gameObject.transform.position;
        buildPoint.transform.position = new Vector3(planetPos.x + planetRadius + 2, planetPos.y, 1);
        pivot.name = "Pivot";
        buildPoint.name = "buildPoint";
        builderInPlace = true;
        Debug.Log("Builder Created");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SunGw")
        {
            Object.Destroy(gameObject, 0.05f);
        }
        if (collision.gameObject.tag == "PlntGw")
        {
            Destroy(collision.gameObject, 0.05f);
        }
    }

    public bool HasConstruct()
    {
        return hasConstruct;
    }
    public bool BuilderInPlace()
    {
        return builderInPlace;
    }
}
