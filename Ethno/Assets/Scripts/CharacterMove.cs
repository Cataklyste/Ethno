using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.EventSystems;

public class CharacterMove : MonoBehaviour
{
    // private Vector3 Position;
    private Vector3 _targetPosition;
    private Vector3 directionToMove;
    private bool _haveToMove = false;
    private NavMeshPath _path;
    private int _indexPath;
    //  private bool _sickHowToMove = false;
    // Use this for initialization
    void Start()
    {
        _path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();

        Move();

    }

    void GetMousePosition()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                _haveToMove = true;

                _targetPosition = raycastHit.point;
                UpdatePath();
            }
        }
    }

    void Move()
    {
        if (!_haveToMove)
            return;


        float _speed = 10f;
       transform.Translate(directionToMove.normalized * Time.deltaTime * _speed);

        if (Vector3.Distance(transform.position, _path.corners[_indexPath]) > 1f)
        {
            
            Vector3 dir = _path.corners[_indexPath] - transform.position;
            directionToMove = new Vector3(dir.x, transform.position.y, dir.z);
        }
        else
        {
            if (_path.corners.Length - 1 <= _indexPath)
            {
                Debug.Log("nice");
                _haveToMove = false;
                _indexPath = 1;
            }
            else
            {
                ++_indexPath;
            }
        }
    }

    void UpdatePath()
    {
        if (NavMesh.CalculatePath(transform.position, _targetPosition, 1, _path))
        {
            Debug.Log("begin");

            for (var i = 0; i < _path.corners.Length - 1; ++i)
            {
                Debug.DrawLine(_path.corners[i], _path.corners[i + 1]);
                Debug.Log(_path.corners[i]);
            }

            Debug.Log("end");
            _indexPath = 1;

                Debug.Log(_path.corners.Length);

            // transform.position = directionToMove;


        }
    }
}