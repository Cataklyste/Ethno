using UnityEngine;
using System.Collections;

public class Player : CharacterMove
{
	public override void Update()
	{
		GetMousePosition();

		base.Update();
	}

	void GetMousePosition()
	{
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;

			if (Physics.Raycast(ray, out raycastHit))
			{
				MovePosition(raycastHit.point);
			}
		}
	}
}
