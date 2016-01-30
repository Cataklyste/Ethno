using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : CharacterMove
{
	private Vector3 _mousePosition;
	private CharacterMove _AITarget;

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

		if (_reatchTarget && _AITarget != null)
		{
			_circulareMenu.gameObject.SetActive(true);
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
					_AITarget = raycastHit.collider.gameObject.transform.parent.GetComponentInParent<CharacterMove>();
                    _circulareMenu.ia = _AITarget as IA;
				}
				else
				{
					_circulareMenu.SUPER();
					_circulareMenu.gameObject.SetActive(false);
					_AITarget = null;
				}

				Vector3 position = new Vector3(raycastHit.point.x, 0f, raycastHit.point.z);

				MovePosition(position);
			}
		}
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
	}
}
