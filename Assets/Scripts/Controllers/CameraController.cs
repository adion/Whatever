using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

    void Start() {
        offset = transform.position - target.position;
    }

    void FixedUpdate() {
        transform.position = Vector3.Lerp(
			transform.position,
			target.position + offset,
			smoothing * Time.deltaTime
		);
    }
}
