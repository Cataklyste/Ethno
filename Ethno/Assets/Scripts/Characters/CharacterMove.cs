using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	protected bool _reatchTarget = true;

	public bool win = false;
	public bool loose = false;

	private float dTimeWin = 0f;
	private float dTimeLoose = 0f;
	private float dt = 0f;

	public Animator anim;

	private float someScale;

	#endregion

#region MonoBehaviours


	public virtual  void Start()
	{
		_path = new NavMeshPath();

		someScale = anim.gameObject.transform.localScale.x;
	}


	public virtual void Update()
	{
		Move();
		SetAnimBool();
	}
#endregion

#region Private Methods
	public void MovePosition(Vector3 position)
	{
		if (Vector3.Distance(transform.position, position) < 0.25f)
			return;

		UpdatePath(position);
	}

	public void StopMove()
	{
		_haveToMove = false;
	}

	#endregion

#region Public Methods
	void Move()
	{
		if (!_haveToMove)
			return;

		Vector3 positionToGo = new Vector3(transform.position.x, _path.corners[_indexPath].y, transform.position.z);

		Vector3 dir = _path.corners[_indexPath] - transform.position;
		directionToMove = new Vector3(dir.x, 0f, dir.z);

		if (directionToMove.x + transform.position.x <= transform.position.x)
			anim.gameObject.transform.localScale = new Vector3(someScale, anim.gameObject.transform.localScale.y, anim.gameObject.transform.localScale.z);
		else
			anim.gameObject.transform.localScale = new Vector3(-someScale, anim.gameObject.transform.localScale.y, anim.gameObject.transform.localScale.z);

		if (Vector3.Distance(positionToGo, _path.corners[_indexPath]) > (directionToMove.normalized * Time.deltaTime * _speed).magnitude)
		{
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
				transform.Translate(_path.corners[_indexPath] - positionToGo);
				++_indexPath;
			}
		}
	}

	void SetAnimBool()
	{
		if(_reatchTarget)
		{

			if (win)
			{
				anim.SetTrigger("isWin");
				win = false;
			}
			else if (loose)
			{
				anim.SetTrigger("isFail");
				loose = false;
			}

			anim.SetBool("isRunning", false);

		}
		else
		{
			anim.SetBool("isRunning", true);
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
#endregion
}