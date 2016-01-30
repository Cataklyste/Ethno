using UnityEngine;
using System.Collections;

public class IA : CharacterMove {

	public float TimerMove = 5.0f;
	private float RealTimer;

	public float RadiusMove = 5.0f;
	private Vector3 newPos;

	[Range(0.0f, 100.0f)]
	public float ChanceDeBouger;

	public override void Start() 
	{
		base.Start();

		RealTimer = TimerMove;

		Vector3 tmp = Random.insideUnitCircle * RadiusMove;
		tmp.z = tmp.y;
		tmp.y = 0;
		Vector3 michou = transform.position + tmp;

		MovePosition(michou);
	 }
	
	public override void Update() 
	{
		Debug.Log(RealTimer);
		if(RealTimer <= 0.0f)
		{
			float randTMP = Random.Range(0.0f, 100.0f);
			if (randTMP <= ChanceDeBouger)
			{
				Vector3 tmp = Random.insideUnitCircle * RadiusMove;
				tmp.z = tmp.y;
				tmp.y = 0;
				Vector3 michou = transform.position + tmp;
				MovePosition(michou);

			}
				RealTimer = TimerMove;
		}
		else
		{
			RealTimer -= Time.deltaTime;
		}

		base.Update();
	}
}
