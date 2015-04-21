 using UnityEngine;
using System.Collections;
using ORBITALRAIN;

public class cannonScript : Building {
	
	public bool parallellBarrelAim = true;
	string aimMode = "Parallell Aim Mode";
	public GameObject pivot, bulletSpawn, projectile;
	protected float rightLimit, leftLimit, rotationSpeed, bulletForce, fireRate, lastShot;
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(selected || 5 ==5) {SelectedBehaviour();} //Disable when being built

	}
	protected virtual void SelectedBehaviour() {
		Aim ();
		Fire ();
	}

	protected void Aim() {
		if (parallellBarrelAim) {
			Vector3 relPos = Camera.main.ScreenToWorldPoint (Input.mousePosition) - pivot.transform.parent.parent.position;
			Quaternion targetRotation = Quaternion.LookRotation(relPos.normalized, Vector3.forward);
			targetRotation.x = 0;
			targetRotation.y = 0;
			pivot.transform.rotation = Quaternion.RotateTowards (pivot.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed * 200);
			Vector3 angle = pivot.transform.localEulerAngles;
			angle.z = Mathf.Clamp (angle.z, 180 - leftLimit, 180 + rightLimit);
			pivot.transform.localEulerAngles = angle;

		} else {
			//ConvergenceAiming
			//Note: Not complete, needs a slight rotation compensation so that the cannons aim at a point in further away from mousePointer, not at it.
			Vector3 relPos = Camera.main.ScreenToWorldPoint (Input.mousePosition) - pivot.transform.position;
			Quaternion targetRotation = Quaternion.LookRotation(relPos.normalized, Vector3.forward);
			targetRotation.x = 0;
			targetRotation.y = 0;
			pivot.transform.rotation = Quaternion.RotateTowards (pivot.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed * 200);
			Vector3 angle = pivot.transform.localEulerAngles;
			angle.z = Mathf.Clamp (angle.z, 180 - leftLimit, 180 + rightLimit);
			pivot.transform.localEulerAngles = angle;			
		}
	}

	protected virtual void Fire() {
		/// *** Add base.Fire() to inherited script to enable basic shooting
		if(Input.GetKey(KeyCode.Space)/*Input.GetMouseButton(0)*/ && Time.time > fireRate + lastShot) {
			//particle.gameObject.particleSystem.Play();
			GameObject bullet = (GameObject)Instantiate(projectile, bulletSpawn.transform.position, transform.rotation) as GameObject;
			bullet.GetComponent<Rigidbody2D>().AddForce(pivot.transform.up * bulletForce * -100 );
			lastShot = Time.time;
			gameObject.GetComponent<AudioSource>().Play();
			transform.Find ("Pivot/turretHead/Barrel/bulletSpawn/ToonFire - Explosion 4 +Glow PS").gameObject.GetComponent<ParticleSystem>().Play();
			gameObject.GetComponent<Animation>().Play();
		} 
	}


	protected override void OnGUI() {
		if (GUI.Button (new Rect (390, 450, 170, 30), aimMode)) {
			if(parallellBarrelAim == true) {
				parallellBarrelAim = false;
				aimMode = "Convergence Aim Mode";
			} else { 
				parallellBarrelAim = true;
				aimMode = "Parallell Aim Mode";}
			
		}
	}
}