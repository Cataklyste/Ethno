using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Player : CharacterMove
{
	
	//TODO list IA follow
	public List<EthniObject> ItemsBed;

	private IA _AITarget = null;
	private bool _active = false;
	public CirculareMenu _circulareMenu;

	private Cue CueGrass, CueSnow, CueSand, CueDirt;

	public override void Start()
	{
		base.Start();

		Cue[] tmp = GetComponents<Cue>();
		for(int i = 0; i < tmp.Length; ++i)
		{
			if(tmp[i].Type == CueType.GRASS)
				CueGrass = tmp[i];
			else if (tmp[i].Type == CueType.SNOW)
				CueSnow = tmp[i];
			else if (tmp[i].Type == CueType.SAND)
				CueSand = tmp[i];
			else if (tmp[i].Type == CueType.DIRT)
				CueDirt = tmp[i];
		}
	}

	public void ShowItem(EEthni tmp)
	{
		for (int i = 0; i < ItemsBed.Count; ++i)
		{
			if(tmp == ItemsBed[i].tmp)
			{
				ItemsBed[i].GetComponent<SpriteRenderer>().enabled = true;
			}
		}
	}

	public override void Update()
	{
		GetMousePosition();

		base.Update();

		if (_reatchTarget && _AITarget != null && !_active)
		{
			_active = true;
			_circulareMenu.gameObject.SetActive(true);
			_AITarget.QuestionPlayer(this);
		}

		if (_reatchTarget)
		{
			EnableWalkSound(false);
		}
		else
		{
			EnableWalkSound(true);
		}
	}

	void EnableWalkSound(bool status)
	{
		if (CueGrass == null || CueSnow == null || CueSand == null || CueDirt == null) return;
		CueGrass.isMoving = status;
		CueSnow.isMoving = status;
		CueSand.isMoving = status;
		CueDirt.isMoving = status;
	}

	void GetMousePosition()
	{
        if (Input.GetMouseButton(0))
		{
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;

            int layermask = 1 << LayerMask.NameToLayer("Ground");

            if (Physics.Raycast(ray, out raycastHit, 600f, layermask))
			{
				if (raycastHit.collider.tag == "MainAI")
				{
					IA tempAI = raycastHit.collider.gameObject.GetComponentInParent<IA>();

					if (_AITarget == null)
					{
						_AITarget = tempAI;
					}
					else if (_AITarget != null && tempAI != _AITarget)
					{
						_AITarget.RealiseYOU();
						_AITarget = tempAI;
					}
					else if (_AITarget != null && tempAI == _AITarget)
						return;

					_AITarget.YouAreMind();

					_circulareMenu.ia = tempAI;
					Vector3 position = new Vector3(raycastHit.point.x, 0f, raycastHit.point.z);

					MovePosition(position);
				}
				else
				{
					Vector3 position = new Vector3(raycastHit.point.x, 0f, raycastHit.point.z);

					MovePosition(position);

					if (_AITarget)
						_AITarget.RealiseYOU();

					QuitConversation();
				}
			}
		}
	}


	public void QuitConversation()
	{
		_circulareMenu.ia = null;
		_circulareMenu.SUPER();
		_circulareMenu.gameObject.SetActive(false);
        /*if (_AITarget)
            _AITarget.PlayerGoOut();*/
		_AITarget = null;
		_active = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "TriggerGrass")
		{
			CueGrass.isActive = true;
		}
		else if (other.tag == "TriggerSnow")
		{
			CueSnow.isActive = true;
		}
		else if (other.tag == "TriggerSand")
		{
			CueSand.isActive = true;
		}
		else if (other.tag == "TriggerDirt")
		{
			CueDirt.isActive = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "TriggerGrass")
		{
			CueGrass.isActive = false;
		}
		else if (other.tag == "TriggerSnow")
		{
			CueSnow.isActive = false;
		}
		else if (other.tag == "TriggerSand")
		{
			CueSand.isActive = false;
		}
		else if (other.tag == "TriggerDirt")
		{
			CueDirt.isActive = false;
		}
	}
}
