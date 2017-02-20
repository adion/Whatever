using UnityEngine;

public class PlayerController : MonoBehaviour {

	[RangeAttribute(1, 50)]
	public float speed;
	
	public Transform shotSpawn;
	public GameObject shot;
	public float fireRate;
	
	
	private CharacterController controller;
	private float currentJump;

	private float nextFire;

	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	void Update () {
		Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;

		controller.SimpleMove(moveDirection * speed);
		transform.rotation = Quaternion.LookRotation(moveDirection);

		if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
	}
}