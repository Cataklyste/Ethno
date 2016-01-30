using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.EventSystems;

public class CharacterMove : MonoBehaviour
{

#region Fields

	[SerializeField] private float _speed = 10f;

	private Vector3 directionToMove;
	private bool _haveToMove = false;
	private NavMeshPath _path;
	private int _indexPath = 1;

	#endregion

#region MonoBehaviours
	void Start()
	{
		_path = new NavMeshPath();
	}


	public virtual void Update()
	{
		Move();
	}
#endregion

#region Private Methods
	public void MovePosition(Vector3 position)
	{
		if (Vector3.Distance(transform.position, position) < 1f)
			return;

		_haveToMove = true;
		UpdatePath(position);
	}
#endregion

#region Public Methods
	void Move()
	{
		if (!_haveToMove)
			return;

		transform.Translate(directionToMove.normalized*Time.deltaTime*_speed);

		if (Vector3.Distance(transform.position, _path.corners[_indexPath]) > 0.5f)
		{
			Vector3 dir = _path.corners[_indexPath] - transform.position;
			directionToMove = new Vector3(dir.x, transform.position.y, dir.z);
		}
		else
		{
			if (_path.corners.Length - 1 <= _indexPath)
			{
				_haveToMove = false;
			}
			else
			{
				transform.position = new Vector3(_path.corners[_indexPath].x, transform.position.y, _path.corners[_indexPath].z);
				++_indexPath;
			}
		}
	}

	void UpdatePath(Vector3 targetPosition)
	{
		if (NavMesh.CalculatePath(transform.position, targetPosition, 1, _path))
		{
			_indexPath = 1;
		}
		else
			_haveToMove = false;
	}
#endregion
}