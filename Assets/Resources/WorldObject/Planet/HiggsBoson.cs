using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HiggsBoson : MonoBehaviour{
    
    //array used as a temporary store for all the gameobjects that should act as gravity wells
    GameObject[] gravityArray;

    //creates a list of the class "GravityWell"
    List<GravityWell> gravityWells = new List<GravityWell>();

    Vector3 gravityVector;
    Vector3 towardWell;
    Vector3 wellRadius;

    //calculated force
    float gravityA;

    //the gravitational constant
    double G = (6.67 * System.Convert.ToDouble(Mathf.Pow(10, -11)));

    // Use this for initialization
    void Awake()
    {
        //duh.
        RefreshGravity();
    }

    //goes through every single object tagged for being a gravitywell, and adds them to the list. with name, size, owner etc.
    [RPC]


    public void RefreshGravity()
    {

        //clears the entire list, and readies it up for another round
        gravityWells.Clear();

        //searches for all objects with... well duh!
        gravityArray = GameObject.FindGameObjectsWithTag("PlntGwTrgt");

        //safetyprecaution, to stop it from doing anything if it does not find any PlntGwTrgt
        if (gravityArray.Length > 0)
        {
            for (int i = 0; i < gravityArray.Length; i++)
            {
                //adds to the list, and sets the name, position, mass and gameobject of the GravityWell
                gravityWells.Add(new GravityWell(gravityArray[i].gameObject.name, gravityArray[i].transform.position, gravityArray[i].GetComponent<CelestialBody>().mass, gravityArray[i].gameObject));
            }
        }

        //this bit is for surveillance of the NSA database
        gravityArray = GameObject.FindGameObjectsWithTag("SunGwTrgt");
        //same as above, but for suns
        if (gravityArray.Length > 0)
        {

            for (int h = 0; h < gravityArray.Length; h++)
            {
                gravityWells.Add(new GravityWell(gravityArray[h].gameObject.name, gravityArray[h].transform.position, gravityArray[h].GetComponent<CelestialBody>().mass, gravityArray[h].gameObject));
            }
        }

        //starts the planet with the startforce specified before
        if (gameObject.tag == "SunGwTrgt" || gameObject.tag == "PlntGwTrgt") {
            GetComponent<Rigidbody2D>().AddForce(GetComponent<CelestialBody>().startForce);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //add the force vectors and apply them


        for (int i = gravityWells.Count - 1; i >= 0; i--)
        {

            //simple check to avoid null arguments, and removes the list entry that is null
            if (gravityWells[i].connectedObject == null)
            {
                gravityWells.RemoveAt(i);
            }
            else
            {

                //first updates the gravitywells position
                gravityWells[i].position = gravityWells[i].connectedObject.transform.position;

                //simple check to specify that a planet does not attract itself, no matter how attractive
                if (gravityWells[i].position != transform.position)
                {
                    wellRadius = gravityWells[i].position - transform.position;
                    towardWell = Vector3.Normalize(wellRadius);
                    //towardWell = Vector3.Normalize(gravityWells[i].GetComponent<PlanetInfo>().position - transform.position);

                    gravityA = (float)((G * gravityWells[i].gWMass / (wellRadius.magnitude * wellRadius.magnitude)));
                    //gravityF = (float)gravityA;			

                    // needs to be added to the config file for balancing, but adds the force.
                    GetComponent<Rigidbody2D>().AddForce(towardWell * gravityA * Time.fixedDeltaTime * 100000);
                }


            }
        }
    }
}
