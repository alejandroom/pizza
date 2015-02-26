using UnityEngine;
using System.Collections;

public abstract class GenericModelController : MonoBehaviour {
	public abstract void attack();
	public abstract void wound();
	public abstract void death();
	
	public abstract void bigger();
	public abstract void resetSize();
	public abstract void specialAttack();
}
