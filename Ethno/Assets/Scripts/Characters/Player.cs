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
}
