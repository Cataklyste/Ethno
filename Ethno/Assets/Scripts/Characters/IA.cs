using UnityEngine;
using System.Collections;

public class IA : CharacterMove
{
	public enum Status
	{
		NONE = 0,
		TALK,
		LISEN,
		FOLLOW
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
	private bool _isHumain = false;
	private Player _player = null;

    private SpriteRenderer word;

	public override void Start()
	{
		base.Start();

		RealTimer = TimerMove;
		StartPos = transform.position;

        word = transform.FindChild("GameObject/Word").gameObject.GetComponent<SpriteRenderer>();
        word.gameObject.SetActive(false);
	}

	public override void Update() 
	{
		if (status == Status.NONE || status == Status.FOLLOW)
		{
			if (status != Status.FOLLOW)
			{
				//RandomMove();
			}
			else
				MovePosition(_player.transform.position);

			base.Update();
		}
		else if (status == Status.TALK)
		{
			if (_canAske && !_isHumain)
				QuestionIA();
			else if (_canAske && _isHumain)
				QuestionPlayer(_player);
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

	void DoAction(IA targetIA)
	{
		if (status != Status.NONE || targetIA.status != Status.NONE)
			return;

		float randParler = Random.Range(0.0f, 100.0f);

		if (randParler <= ChanceDeParler)
		{
			StopMove();
			targetIA.StopMove();

			_targetTalke = targetIA;
			targetIA._targetTalke = this;

			int random = Random.Range(0, 1);

			if (random == 0)
			{
				status = Status.TALK;
				targetIA.status = Status.LISEN;
			}
			else
			{
				status = Status.LISEN;
				targetIA.status = Status.TALK;
			}
		}
	}

	void QuestionIA()
	{
		Debug.DrawRay(transform.position, Vector3.up, Color.red);
		Debug.DrawRay(_targetTalke.transform.position, Vector3.up, Color.red);

		_index = Random.Range(0, 3);
		AskeQuestion(_index);

		float randParler = Random.Range(1f, 3.0f);
		StartCoroutine(AskingIn(randParler));
	}

	public void QuestionPlayer(Player player)
	{
		_player = player;
		_isHumain = true;

		if (_index >= 4)
		{ 	
			EndTalk();
			_player = player;
			status = Status.FOLLOW;
			return;
		}

		status = Status.TALK;

		AskeQuestion(_index);
        StartCoroutine(AskingIn(0.0f));
	}

	void AskeQuestion(int index)
	{
		if (_index == 0)
			iaValue = language.salut;
		else if (_index == 1)
			iaValue = language.insulte;
		else if (_index == 2)
			iaValue = language.oui;
		else if (_index == 3)
			iaValue = language.non;



		Debug.Log("QUESTION " + _index + ": " + iaValue + "reponse: " + language.getAnswer(iaValue));
		_canAske = false;	
	}

	public void Question(int questionValue)
	{
		iaValue = language.getAnswer(questionValue); ;

		float randParler = Random.Range(1f, 2.0f);
		StartCoroutine(AnswerIn(randParler));
	}

	public void Answer(int reponse)
	{
		_canAske = true;
        word.gameObject.SetActive(false);

		if (language.PlayerAnswerMatch(iaValue, reponse))
		{
			Debug.Log("ANSWER " + _index + ": ok " + reponse);

			if (_isHumain)
			{
				++_index;
				_player._circulareMenu.SUPER();
				return;
			}

			EndTalk();
		}
		else
		{
			Debug.Log("ANSWER " + _index + ": bad " + reponse + " ici "+  language.getAnswer(iaValue));
			//TODO ANIMATION
			EndTalk();
		}
	}

	public void EndTalk()
	{
		Debug.Log("END");
		_index = 0;
		status = Status.NONE;

		if (!_isHumain)
		{
			_targetTalke.status = Status.NONE;
			_targetTalke._targetTalke = null;
			_targetTalke = null;
		}
		else
			_player.QuitConversation();

		_player = null;
		_isHumain = false;
	}

	void OnTriggerEnter(Collider other)
	{
		IA character = other.gameObject.GetComponent<IA>();

		if (character != null && character != this)
		{
			DoAction(character);
		}
	}

	IEnumerator AskingIn(float rand)
	{
		yield return new WaitForSeconds(rand);


        word.gameObject.SetActive(true);
        word.sprite = Resources.Load<Sprite>("Icons/Words/" + iaValue);

		if (!_isHumain)
			_targetTalke.Question(iaValue);
	}

	IEnumerator AnswerIn(float rand)
	{
		yield return new WaitForSeconds(rand);

		_targetTalke.Answer(iaValue);
    }
}
