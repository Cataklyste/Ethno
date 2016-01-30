using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : CharacterMove
{
	private Vector3 _mousePosition;
	private CharacterMove _AITarget;

	[SerializeField]
	private CirculareMenu _circulareMenu;

	public override void Start()
	{
		base.Start();
	}

	public override void Update()
	{
		GetMousePosition();
		

		base.Update();

		if (_reatchTarget && _AITarget != null)
		{
			_circulareMenu.gameObject.SetActive(true);
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
					_AITarget = raycastHit.collider.gameObject.GetComponentInParent<CharacterMove>();
					
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
}
