using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	public Transform target;
	public float OrbitDegrees = 1f;
	void Update () {
		transform.RotateAround(target.position, Vector3.up, OrbitDegrees);
	}
}
