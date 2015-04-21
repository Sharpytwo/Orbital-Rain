using UnityEngine;
using System.Collections;
using ORBITALRAIN;

public class bulletScript : WorldObject {
	
	protected float damage;	
	public GameObject explosionPrefab;

	// Use this for initialization
	protected virtual void Start () {
		objectType = "bullet";
		InvokeRepeating ("SelfdestructIfOutOfBounds", 10, ResourceManager.searchOrigoFrequency);	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	}
	
	protected virtual void OnCollisionEnter2D(Collision2D collision) {
		WorldObject col = collision.gameObject.GetComponent<WorldObject> ();
		if(col.isDestructable) {
			col.Damage(damage);
			SelfDestruct(col);
		} else {
			SelfDestruct(col);
		}
	}

	protected virtual void SelfDestruct(WorldObject col) { //Needs procedure for moving, destroyed target. Maybe delay destruction by particle.duration and disable all functions. 
		//ParticleEffect
		GameObject particle = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		particle.transform.parent = col.transform;
		//Sound
		Destroy (gameObject);
	}

	protected virtual void SelfdestructIfOutOfBounds() { //private? non-virtual?
		if(gameObject.transform.position.sqrMagnitude > ResourceManager.systemOuterLimit) {
			Destroy(gameObject);}
	}
}