using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _secondsToRelease = 10;
    [SerializeField] private float _speed = 4;
    private Vector3 _direction;

    private WaitForSeconds _wait;

    public event Action<Enemy> Release;

    private void Awake()
    {
        _wait = new WaitForSeconds(_secondsToRelease);
    }

    private void OnEnable()
    {
       StartCoroutine(WaitAndRelease());
    }

    public void Update()
    {
        Move();
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    public IEnumerator WaitAndRelease()
    {
        yield return _wait;
        Release?.Invoke(this);
    }
}
