using UnityEngine;

public class PlayerController : MonoBehaviour {

	[RangeAttribute(1, 50)]
	public float speed;
	
	public Transform shotSpawn;
	public GameObject shot;
	public float fireRate;
	
	
	private Animator animator;
	private CharacterController cc;
	private float currentJump;

	private Vector3 lastLookDirection;
	private float nextFire;

	void Start () {
		animator = GetComponent<Animator>();
		cc = GetComponent<CharacterController>();
	}
	
	void Update () {
		Vector3 moveDirection = new Vector3(Input.GetAxisRaw("MoveHorizontal"), 0.0f, Input.GetAxisRaw("MoveVertical")).normalized;
		Vector3 lookDirection = new Vector3(Input.GetAxisRaw("LookHorizontal"), 0.0f, Input.GetAxisRaw("LookVertical")).normalized;
		
		bool firing = lookDirection != Vector3.zero;

		if (firing) {
			lastLookDirection = lookDirection;
		}

		// move and look
		cc.SimpleMove(moveDirection * speed);
		transform.rotation = Quaternion.LookRotation(lastLookDirection);
		
		// animate
		if (moveDirection == Vector3.forward || moveDirection == Vector3.right || moveDirection == -Vector3.right) {
			animator.SetBool("Walk Backward", false);
			animator.SetBool("Walk Forward", true);			
		} else if (moveDirection == -Vector3.forward && moveDirection != lookDirection) {
			animator.SetBool("Walk Forward", false);
			animator.SetBool("Walk Backward", true);
		} else {
			animator.SetBool("Walk Forward", false);
			animator.SetBool("Walk Backward", false);
			animator.Play("Idle");
		}
		
		// shoot?
		if (firing && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}