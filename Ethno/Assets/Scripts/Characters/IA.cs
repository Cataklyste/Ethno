using UnityEngine;
using System.Collections;

public class IA : CharacterMove
{
	public enum Status
	{
		NONE = 0,
		TALK,
		LISEN
	}

	private Vector3 StartPos;

	public float TimerMove = 5.0f;
	private float RealTimer;

	public float RadiusMove = 5.0f;
	private Vector3 newPos;

	[Range(0.0f, 100.0f)]
	public float ChanceDeBouger;
	[Range(0.0f, 100.0f)]
	public float ChanceDeParler;

	public float DistanceRetour = 1.0f;

    public Language language;

    public int iaValue;

	public Status status = Status.NONE;
	private IA _targetTalke;
	private int _index = 0;
	private bool _canAske = true;

	public override void Start()
	{
		base.Start();

		RealTimer = TimerMove;
		StartPos = transform.position;
	}

	public override void Update() 
	{
		if (status == Status.NONE)
		{
			RandomMove();
			base.Update();
		}
		else if (status == Status.TALK)
		{
			askeQuestion();
		}
	}

	void RandomMove()
	{
		if (RealTimer <= 0.0f)
		{
			float randTMP = Random.Range(0.0f, 100.0f);
			if (randTMP <= ChanceDeBouger)
			{
				if (Vector3.Distance(transform.position, StartPos) <= DistanceRetour)
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
	}



	protected override void DoAction(CharacterMove Charac)
	{
		IA targetIA = Charac as IA;

		if (targetIA == null) return;

		if (status != Status.NONE || targetIA.status != Status.NONE)
			return;

		float randParler = Random.Range(0.0f, 100.0f);

		if (randParler <= ChanceDeParler)
		{
			StopMove();
			_targetTalke = targetIA;

			if (gameObject.GetInstanceID() > targetIA.GetInstanceID())
			{
				status = Status.TALK;
				targetIA.status = Status.LISEN;
			}
			else
			{
				status = Status.LISEN;
				targetIA.status = Status.TALK;
			}

			Debug.Log(name + " " + status.ToString());
			Debug.Log(targetIA.name + " " + targetIA.status.ToString());
		}
	}

	

	void askeQuestion()
	{
		if (!_canAske) return;

		if (_index == 0)
			iaValue = language.salut;
		else if (_index == 1)
			iaValue = language.insulte;
		else if (_index == 2)
			iaValue = language.oui;
		else if (_index == 3)
			iaValue = language.non;
		else
		{
			EndTalk();
			return;
		}

		Debug.Log(iaValue);

		_canAske = false;
		float randParler = Random.Range(1f, 3.0f);
		StartCoroutine(WaitInteration(randParler));
	}

	public void  Answer(int question)
	{
		int answer = language.getAnswer(question);
		Debug.Log(answer);
	}

	public void EndTalk()
	{
		_index = 0;
		status = Status.NONE;
		_targetTalke.status = Status.NONE;
		_targetTalke = null;
	}

	IEnumerator WaitInteration(float rand)
	{
		yield return new WaitForSeconds(rand);

		_targetTalke.Answer(iaValue);
		++_index;
		_canAske = true;
	}
}
