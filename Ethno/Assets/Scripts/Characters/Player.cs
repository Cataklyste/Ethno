using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : CharacterMove
{
	private Vector3 _mousePosition;
	//TODO list IA follow
	private IA _AITarget = null;
	private bool _active = false;
	public CirculareMenu _circulareMenu;

	private Cue CueGrass, CueSnow, CueSand;

	[SerializeField]
	private CirculareMenu _circulareMenu;

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
		if(_reatchTarget)
		{
			CueGrass.isMoving = false;
			CueSnow.isMoving = false;
			CueSand.isMoving = false;
		}
		else
		{
			CueGrass.isMoving = true;
			CueSnow.isMoving = true;
			CueSand.isMoving = true;
		}
	}

	void GetMousePosition()
	{
		if (Input.GetMouseButton(0))
		{
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			if (_mousePosition == Input.mousePosition)
				return;

			_mousePosition = Input.mousePosition;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;


			if (Physics.Raycast(ray, out raycastHit))
			{
				if (raycastHit.collider.tag == "MainAI")
				{
					IA tempAI = raycastHit.collider.gameObject.GetComponentInParent<IA>();

					if (_AITarget == null ||(_AITarget != null && tempAI != _AITarget))
					{
						_AITarget = tempAI;

						_circulareMenu.ia = _AITarget as IA;
						Vector3 position = new Vector3(raycastHit.point.x, 0f, raycastHit.point.z);

						MovePosition(position);
						//QuitConversation();
					}
				}
				else
				{
					Vector3 position = new Vector3(raycastHit.point.x, 0f, raycastHit.point.z);

					MovePosition(position);
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
	}
	void OnTriggerExit(Collider other)
	{
		CueGrass.isActive = false;
		CueSnow.isActive = false;
		CueSand.isActive = false;
	}
}
