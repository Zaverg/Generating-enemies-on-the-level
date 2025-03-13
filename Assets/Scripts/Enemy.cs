using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _secondsToRelease = 10;
    [SerializeField] private float _speed = 4;
    private Transform _target;

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

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    public IEnumerator WaitAndRelease()
    {
        yield return _wait;
        Release?.Invoke(this);
    }
}
