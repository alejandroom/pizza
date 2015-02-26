using UnityEngine;
using System.Collections;

public class Box {
	
	private GameObject box;
	private Quaternion baseQuaternion;
	
	private int maxSpeed;
	private int minSpeed;
	
	public Box (GameObject box, Quaternion baseQuaternion, int maxSpeed, int minSpeed) {
		this.box = box;
		this.baseQuaternion = baseQuaternion;
		
		this.maxSpeed=maxSpeed;
		this.minSpeed=minSpeed;
	}
	
	public void acelerar(bool enable)
	{
		if(enable) {
			box.transform.rotation = baseQuaternion;
			box.GetComponent<Rotator>().speed=maxSpeed;
			box.GetComponent<Rotator>().restart();
		} else {
			box.GetComponent<Rotator>().speed=minSpeed;
			box.transform.rotation = new Quaternion(0,0,0,0);
		}		
	}
	
	public void apagar(bool enable)
	{
		Renderer[] quads = box.GetComponentsInChildren<Renderer>();
		for(int i=0 ; i< quads.Length ; i++)
			quads[i].enabled=enable;
	}
}
