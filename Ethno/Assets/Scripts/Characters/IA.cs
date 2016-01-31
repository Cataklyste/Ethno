using UnityEngine;
using System.Collections;
using System.IO;

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

	private bool questionPosser = false;
	private float _deltaQuestion = 0f;
	private float _deltaAnswer = 0f;

	private bool _GetQuestion = false;

	private SpriteRenderer word;

	public float DistanceSound = 5f;

	public float TimeQuestion = 2f;
	public float TimeAnswern = 2f;

	private bool Fear = false;
	private bool haveIA = false;

	private float td = 0;

    public float offset;


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
		if (Fear) return;

		if (status == Status.NONE)
		{
			RandomMove();
				
			base.Update();
		}
		else if (status == Status.TALK)
		{
			if (_canAske && !_isHumain)
				QuestionIA();
			else if (_canAske && _isHumain)
				QuestionPlayer(_player);

			if (!questionPosser)
				AskingQuestion();

			if (!_isHumain)
			{
				td += Time.deltaTime;
				if (td >= TimeAnswern + TimeQuestion)
				{
					status = Status.NONE;
					td = 0f;
				}
			}
		}
		else if (status == Status.LISEN)
		{
			if (_GetQuestion)
				ListenQuestion();
		}
		else if (status == Status.FOLLOW)
		{
				Vector3 direction = _player.transform.position - transform.position;
				direction.Normalize();
				MovePosition(_player.transform.position - direction * offset);
				base.Update();
		}
	}

	public void YouAreMind()
	{
		Fear = true;
		EndTalk();
		StopMove();
	}

	public void RealiseYOU()
	{
		Fear = false;
		EndTalk();
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
			haveIA = true;
		}
	}

	void QuestionIA()
	{
		_index = Random.Range(0, 4);

		AskeQuestion(_index);
	}

	void AskingQuestion()
	{
		_deltaQuestion += Time.deltaTime;

		if (_deltaQuestion >= TimeQuestion)
		{
			questionPosser = true;
			_targetTalke.Question(iaValue);
			word.gameObject.SetActive(false);
		}
	}

	void ListenQuestion()
	{
		_deltaAnswer += Time.deltaTime;

		if (_deltaAnswer >= TimeAnswern)
		{
			_targetTalke.Answer(iaValue);
			word.gameObject.SetActive(false);
		}
	}

	public void QuestionPlayer(Player player)
	{
		_player = player;
		Fear = false;
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
		questionPosser = true;
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

		word.sprite = Resources.Load<Sprite>("Icons/Words/" + iaValue);
		word.gameObject.SetActive(true);

		_canAske = false;

		if (_index == 0)
			SoundManager.Instance.PlaySfx(gameObject, "Sfx_Sign_Hello", 1f, true, DistanceSound, false);
		else if (_index == 1)
			SoundManager.Instance.PlaySfx(gameObject, "Sfx_Sign_Injure", 1f, true, DistanceSound, false);
		else if (_index == 2)
			SoundManager.Instance.PlaySfx(gameObject, "Sfx_Sign_Yes", 1f, true, DistanceSound, false);
		else if (_index == 3)
			SoundManager.Instance.PlaySfx(gameObject, "Sfx_Sign_No", 1f, true, DistanceSound, false);
	}

	public void Question(int questionValue)
	{
		iaValue = language.getAnswer(questionValue);

		_GetQuestion = true;

		word.gameObject.SetActive(true);
		word.sprite = Resources.Load<Sprite>("Icons/Words/" + iaValue);

		int tmp = language.getIndexAnswer(iaValue);

		if (tmp == 0)
			SoundManager.Instance.PlaySfx(gameObject, "Sfx_Sign_Hello", 1f, true, DistanceSound, false);
		else if (tmp == 1)
			SoundManager.Instance.PlaySfx(gameObject, "Sfx_Sign_Injure", 1f, true, DistanceSound, false);
		else if (tmp == 2)
			SoundManager.Instance.PlaySfx(gameObject, "Sfx_Sign_Yes", 1f, true, DistanceSound, false);
		else if (tmp == 3)
			SoundManager.Instance.PlaySfx(gameObject, "Sfx_Sign_No", 1f, true, DistanceSound, false);

	}

	public void Answer(int reponse)
	{
		_canAske = true;

		if (language.PlayerAnswerMatch(iaValue, reponse))
		{
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
			//TODO ANIMATION
			EndTalk();
		}
	}

	public void EndTalk()
	{
		ResetIA();

		if (_isHumain && _player != null)
		{
			_player.QuitConversation();
			_player = null;
		}

		_isHumain = false;

	}

	public void ResetIA()
	{	
		if (haveIA)
		{
			haveIA = false;
			if (_targetTalke)
			_targetTalke.ResetIA();
		}

		word.gameObject.SetActive(false);
		_index = 0;
		status = Status.NONE;
		questionPosser = false;
		_deltaQuestion = 0f;
		_deltaAnswer = 0f;
		_GetQuestion = false;
		_targetTalke = null;
		haveIA = true;
	}

    public void PlayerGoOut()
    {
        _index = 0;
        status = Status.NONE;

        word.gameObject.SetActive(false);

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
}
