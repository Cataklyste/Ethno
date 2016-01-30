using UnityEngine;
using System.Collections;

public class IA : CharacterMove {

	public float TimerMove = 5.0f;
	private float RealTimer;

	public float RadiusMove = 5.0f;
	private Vector3 newPos;

	public override void Start() 
	{
		base.Start();

		Vector3 tmp = Random.insideUnitCircle * RadiusMove;
		tmp.z = tmp.y;
		tmp.y = 0;
		Debug.Log("TMP = " + tmp);
		Vector3 michou = transform.position + tmp;
		MovePosition(michou);
	}
	
	void Update () 
	{


		base.Update();
	}
}
