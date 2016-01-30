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
	[SerializeField] private List<string> _areas;

	private int layermask;

	private Vector3 directionToMove;
	protected bool _haveToMove = false;
	private NavMeshPath _path;
	private int _indexPath = 1;

	protected bool _reatchTarget = false;
	#endregion

#region MonoBehaviours


	public virtual  void Start()
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

		UpdatePath(position);
	}

	public void StopMove()
	{
		_haveToMove = false;
	}

	protected virtual void DoAction(CharacterMove CM)
	{
	}
#endregion

#region Public Methods
	void Move()
	{
		if (!_haveToMove)
			return;

		Vector3 positionToGo = new Vector3(transform.position.x, _path.corners[_indexPath].y, transform.position.z);

		if (Vector3.Distance(positionToGo, _path.corners[_indexPath]) > 0.5f)
		{
			Vector3 dir = _path.corners[_indexPath] - transform.position;

			directionToMove = new Vector3(dir.x, 0f, dir.z);
			transform.Translate(directionToMove.normalized * Time.deltaTime * _speed);
		}
		else
		{
			if (_path.corners.Length - 1 <= _indexPath)
			{
				StopMove();
				_reatchTarget = true;
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
		int layermask = 0;

		foreach (string area in _areas)
			layermask |= 1 << NavMesh.GetAreaFromName(area);

	/*	NavMeshHit hit;
		NavMesh.SamplePosition(targetPosition, out hit, 10, layermask);
		{
			
			Debug.DrawRay(hit.position, Vector3.up, Color.red);
		}*/


		if (NavMesh.CalculatePath(transform.position, targetPosition, layermask, _path))
		{
			_indexPath = 1;
			_haveToMove = true;
			_reatchTarget = false;

			/*for (int i = 0; i < _path.corners.Length - 1; ++i)
				Debug.DrawLine(_path.corners[i], _path.corners[i+1], Color.red);*/
		}
		else
		{
			_haveToMove = false;
		}
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		CharacterMove character = other.gameObject.GetComponent<CharacterMove>();

		if (character != null && character != this)
		{
			DoAction(character);
		}
	}
#endregion
}