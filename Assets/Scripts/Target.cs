using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private List<Vector3> _points = new List<Vector3>();
    private int _index = 0;
    private float _distance = 0.02f;

    private void Awake()
    {
        transform.position = _points[0];
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _points[_index], _speed * Time.deltaTime);

        if ((transform.position - _points[_index]).sqrMagnitude <= _distance)
        {
            _index = _index >= _points.Count - 1 ? 0 : _index + 1;
        }
    }
}
