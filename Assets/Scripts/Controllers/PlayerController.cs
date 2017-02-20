using UnityEngine;

public class PlayerController : MonoBehaviour {

	[RangeAttribute(1, 50)]
	public float speed;
	
	public Transform shotSpawn;
	public GameObject shot;
	public float fireRate;
	
	
	private CharacterController cc;
	private float currentJump;

	private float nextFire;

	void Start () {
		cc = GetComponent<CharacterController>();
	}
	
	void Update () {
		Vector3 moveDirection = new Vector3(Input.GetAxisRaw("MoveHorizontal"), 0.0f, Input.GetAxisRaw("MoveVertical")).normalized;
		Vector3 lookDirection = new Vector3(Input.GetAxisRaw("LookHorizontal"), 0.0f, Input.GetAxisRaw("LookVertical")).normalized;
		bool firing = lookDirection != Vector3.zero;

		cc.SimpleMove(moveDirection * speed);
		transform.rotation = Quaternion.LookRotation(lookDirection);

		if (firing && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}