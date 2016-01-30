using UnityEngine;
using System.Collections;

public class Player : CharacterMove
{
	private Vector3 _mousePosition;

	public override void Update()
	{
		GetMousePosition();

		base.Update();
	}

	void GetMousePosition()
	{
		if (Input.GetMouseButton(0))
		{
			if (_mousePosition == Input.mousePosition)
				return;

			_mousePosition = Input.mousePosition;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;

			if (Physics.Raycast(ray, out raycastHit))
			{
				MovePosition(raycastHit.point);
			}
		}
	}
}
