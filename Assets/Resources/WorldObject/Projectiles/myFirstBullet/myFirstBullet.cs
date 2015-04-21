using UnityEngine;
using System.Collections;

public class myFirstBullet : bulletScript {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		damage = 5.0f;
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}
	protected override void OnCollisionEnter2D(Collision2D collision) {
		base.OnCollisionEnter2D(collision); //Remove for custom Damage
	}
	protected override void SelfDestruct(WorldObject col) {
		base.SelfDestruct(col); //Remove for custom SelfDestruct effects
	}
	protected override void SelfdestructIfOutOfBounds() {
		base.SelfdestructIfOutOfBounds();
	}
}
