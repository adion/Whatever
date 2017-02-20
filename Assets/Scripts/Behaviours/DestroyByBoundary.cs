using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
	public void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
