using UnityEngine;
using System.Collections;

public class PlayerModelController : GenericModelController {
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip attackAnimation;
	public AnimationClip woundAnimation;
	public AnimationClip deathAnimation;

	private Animation _animation;
	private Vector3 baseSize;
	private GameObject slime;
	private Color baseColor;
	private bool doubleAttackBool;
	
	public void Start(){
		_animation = gameObject.GetComponent<Animation>();
		baseSize=gameObject.transform.localScale;
		slime=GameObject.Find("slimelesser");
		baseColor=slime.renderer.material.color;
	}
	
	public override void bigger(){
		gameObject.transform.localScale=new Vector3(gameObject.transform.localScale.x*1.25f,gameObject.transform.localScale.y*1.25f,gameObject.transform.localScale.z*1.25f);
	}
	
	public override void resetSize(){
		gameObject.transform.localScale=baseSize;
	}

	public override void specialAttack(){
		doubleAttack();
	}
	
	public void enrage(){
		slime.renderer.material.color=new Color(1f,0.2f,0.2f);
	}
	
	public void shiv(){
		slime.renderer.material.color=baseColor;
	}
	
	public void andar(){
		_animation[walkAnimation.name].speed = 1.5f;
		_animation[walkAnimation.name].wrapMode = WrapMode.Default;
		_animation.CrossFade(walkAnimation.name);
		_animation.CrossFadeQueued(idleAnimation.name);
	}
	
	public void correr(){
		_animation[runAnimation.name].speed = 1.5f;
		_animation[runAnimation.name].wrapMode = WrapMode.Default;
		_animation.CrossFade(runAnimation.name);
		_animation.CrossFadeQueued(idleAnimation.name);
	}
	
	override public void attack(){
			_animation[attackAnimation.name].speed = 1.5f;
			_animation[attackAnimation.name].wrapMode = WrapMode.Default;
			_animation.CrossFade(attackAnimation.name);
			_animation.CrossFadeQueued(idleAnimation.name);
	}
	
	public void doubleAttack(){
		_animation[attackAnimation.name].speed = 3f;
		_animation[attackAnimation.name].wrapMode = WrapMode.Default;
		_animation.CrossFade(attackAnimation.name);
		_animation.CrossFadeQueued(attackAnimation.name);
		_animation.CrossFadeQueued(idleAnimation.name);
	}
	
	override public void wound(){
		_animation[woundAnimation.name].speed = 1.5f;
		_animation[woundAnimation.name].wrapMode = WrapMode.Default;
		_animation.CrossFade(woundAnimation.name);
		_animation.CrossFadeQueued(idleAnimation.name);
	}
	
	override public void death(){
		_animation[deathAnimation.name].speed = 1.5f;
		_animation[deathAnimation.name].wrapMode = WrapMode.ClampForever;
		_animation.CrossFade(deathAnimation.name);
		_animation.CrossFadeQueued(idleAnimation.name);
	}
}