using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 _direction;
    [SerializeField] private float _secondsToRelease = 10;
    [SerializeField] private float _speed = 4;

    public event Action<Enemy> Release;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(WaitThenRelease());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
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
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    public IEnumerator WaitThenRelease()
    {
        yield return new WaitForSeconds(_secondsToRelease);
        Release?.Invoke(this);
    }
}
