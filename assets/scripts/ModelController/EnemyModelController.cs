using UnityEngine;
using System.Collections;

public class EnemyModelController : GenericModelController {
	public Transform target;
	public Vector3 rotation=Vector3.left;
	public float speed=1f;
	
	private bool attackOn=false;
	private bool deathOn=false;
	private bool woundOn=false;
	private bool specialOn=false;

	private float baseTime;
	private Vector3 basePosition;
	private Quaternion baseRotation;
	private Vector3 inverseRotation;
	private float rotationSpeed=40f;

	private Vector3 baseScale;
	private Transform enemy;
	private bool encojiendo=false;
	private Color baseColor;
	private float baseTimeEncojiendo;

	private Transform baseFalling;

	void Start(){
		basePosition=transform.position;
		baseRotation=transform.rotation;
		inverseRotation=new Vector3(-rotation.x,-rotation.y,-rotation.z);

		enemy=GameObject.Find("Enemy").transform;
		baseScale=enemy.localScale;
		baseColor=GameObject.Find("Enemy_Face").renderer.material.color;
		baseFalling=GameObject.Find ("Enemy_Falling").transform;
	}

	public override void bigger(){
		transform.position=basePosition;
		transform.rotation=baseRotation;
		enemy.localScale=new Vector3(enemy.localScale.x*1.5f,enemy.localScale.y*1.5f,enemy.localScale.z*1.5f);
		attackOn=false;
		woundOn=false;
		deathOn=false;
		GameObject.Find("Enemy_Face").renderer.material.color=new Color(1f,0.4f,0.4f);
	}

	public override void resetSize(){
		encojiendo=true;
		baseTimeEncojiendo=Time.realtimeSinceStartup;
		GameObject.Find("Enemy_Face").renderer.material.color=baseColor;
	}

	public override void specialAttack(){
		transform.position=baseFalling.position;
		transform.rotation=baseFalling.rotation;
		attackOn=false;
		woundOn=false;
		deathOn=false;
		specialOn=true;
		baseTime=Time.realtimeSinceStartup;
	}
	
	public override void attack(){
		transform.position=basePosition;
		transform.rotation=baseRotation;
		attackOn=true;
		woundOn=false;
		deathOn=false;
		specialOn=false;
		baseTime=Time.realtimeSinceStartup;
		rotationSpeed=40f;
	}

	public override void death(){
		transform.position=basePosition;
		transform.rotation=baseRotation;
		attackOn=false;
		woundOn=false;
		deathOn=true;
		specialOn=false;
		baseTime=Time.realtimeSinceStartup;
		rotationSpeed=40f;
	}

	public override void wound(){
		transform.position=basePosition;
		transform.rotation=baseRotation;
		attackOn=false;
		woundOn=true;
		deathOn=false;
		specialOn=false;
		baseTime=Time.realtimeSinceStartup;
		rotationSpeed=40f;
	}

	void Update () {
		if(encojiendo && Time.realtimeSinceStartup-baseTimeEncojiendo>2){
			encojiendo=false;
			enemy.localScale=baseScale;
		}
		if(specialOn){
			if(Time.realtimeSinceStartup-baseTime<0.5){	
				transform.Translate(Vector3.back);
			}else{
				transform.position=basePosition;
				transform.rotation=baseRotation;
				specialOn=false;
			}
		}else if(attackOn){
			if(Time.realtimeSinceStartup-baseTime<0.2){
				transform.position=GameObject.Find ("Enemy_Attack").transform.position;
			}else if(Time.realtimeSinceStartup-baseTime<0.5){	
				transform.Translate(new Vector3(0.05f,0,0.2f));			
			}else{
				transform.position=basePosition;
				transform.rotation=baseRotation;
				attackOn=false;
			}
		}else if(deathOn){
			if(Time.realtimeSinceStartup-baseTime<0.5){
				transform.RotateAround(target.position, rotation, speed/6);				
			}else if(Time.realtimeSinceStartup-baseTime<0.8){			
				transform.RotateAround(target.position, rotation, speed/10);	
			}else if(Time.realtimeSinceStartup-baseTime<1.8){
				transform.RotateAround(target.position, inverseRotation, speed/6);			
			}else if(Time.realtimeSinceStartup-baseTime<2.1){		
				transform.RotateAround(target.position, inverseRotation, speed/10);			
			}else if(Time.realtimeSinceStartup-baseTime<3.1){
				transform.RotateAround(target.position, rotation, speed/6);				
			}else if(Time.realtimeSinceStartup-baseTime<3.4){		
				transform.RotateAround(target.position, rotation, speed/10);		
			}else if(Time.realtimeSinceStartup-baseTime<4.4){
				transform.RotateAround(target.position, inverseRotation, speed/6);					
			}else if(Time.realtimeSinceStartup-baseTime<5){
				transform.RotateAround(target.position, inverseRotation, speed);				
			}else{
				transform.rotation=GameObject.Find("Dead_Cube").transform.rotation;
				transform.position=GameObject.Find("Dead_Cube").transform.position;
				deathOn=false;
			}
		}else if(woundOn){
			if(rotationSpeed>20){
				transform.Rotate(Vector3.up,rotationSpeed);
				rotationSpeed-=0.6f;	
			}else if(rotationSpeed>0){
				transform.Rotate(Vector3.up,rotationSpeed);
				rotationSpeed-=1f;	
			}else{
				transform.position=basePosition;
				transform.rotation=baseRotation;
				woundOn=false;
			}
		}
	}
}
