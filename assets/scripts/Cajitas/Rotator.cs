using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	public float speed=1;
	public float time=2;
	
	private float baseTime;
	
	public float X_axis=0;
	public float Z_axis=5;
	public float Y_axis=0;
	public float desplazamiento=0;
	
	void Start () {
		baseTime=Time.realtimeSinceStartup;
	}
	
	void Update () {
		if((Time.realtimeSinceStartup-baseTime)%(time*2) < time){
        	transform.Rotate(new Vector3(X_axis, Y_axis, Z_axis) * Time.deltaTime * speed);
			transform.Translate(Vector3.right * desplazamiento * Time.deltaTime * speed);
		}
		else{
        	transform.Rotate(new Vector3(-X_axis, -Y_axis, -Z_axis) * Time.deltaTime * speed);
			transform.Translate(Vector3.left * desplazamiento * Time.deltaTime * speed);
		}
	}
	
	public void restart() {
		baseTime=Time.realtimeSinceStartup;
	}
}
