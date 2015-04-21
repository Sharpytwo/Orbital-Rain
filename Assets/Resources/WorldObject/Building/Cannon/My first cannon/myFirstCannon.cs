using UnityEngine;
using System.Collections;
using ORBITALRAIN;

public class myFirstCannon : cannonScript {
	public GameObject explosionPrefab;
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
		leftLimit = 120.0f;
		rightLimit = 120.0f;
		rotationSpeed = 8.0f;
		bulletForce = 10;
		fireRate = GetComponent<Animation>().clip.length + 0.1f;
		maxHealth = 100.0f;
		currentHealth = 0.0f;
		isDestructable = true;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void OnGUI() {
		base.OnGUI ();
	}

	protected override void Fire() {
		base.Fire ();
	}
	protected override void Death() {
		//base.Death ();
		GameObject particle_explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		Debug.Log (gameObject.transform.parent);
		particle_explosion.transform.parent = gameObject.transform.parent;
		Destroy (gameObject);
	}

}