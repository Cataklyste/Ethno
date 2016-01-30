using UnityEngine;
using System.Collections;

public class IA : CharacterMove
{

	public EEthni ethni;

	private Vector3 StartPos;

	public float TimerMove = 5.0f;
	private float RealTimer;

	public float RadiusMove = 5.0f;
	private Vector3 newPos;

	[Range(0.0f, 100.0f)]
	public float ChanceDeBouger;

	public float DistanceRetour = 1.0f;

	public override void Start()
	{
		base.Start();

		RealTimer = TimerMove;
		StartPos = transform.position;
	}

	public override void Update() 
	{
		Debug.Log(RealTimer);
		if(RealTimer <= 0.0f)
		{
			float randTMP = Random.Range(0.0f, 100.0f);
			if (randTMP <= ChanceDeBouger)
			{
				if(Vector3.Distance(transform.position, StartPos) <= DistanceRetour)
				{
					Vector3 tmp = Random.insideUnitCircle * RadiusMove;
					tmp.z = tmp.y;
					tmp.y = 0;
					Vector3 michou = transform.position + tmp;
					MovePosition(michou);
				}
				else
				{
					MovePosition(StartPos);
				}
			}
				RealTimer = TimerMove;
		}
		else
		{
			RealTimer -= Time.deltaTime;
		}

		base.Update();
	}

	public void OnTriggerEnter(Collider collid)
	{
	}
}
