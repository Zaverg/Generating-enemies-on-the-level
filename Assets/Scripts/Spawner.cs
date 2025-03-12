using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Point> _points = new List<Point>();
    [SerializeField] private Pool _pool;

    private float _seconds = 2;
    
    private void OnEnable()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private void Spawn()
    {
        if (_points.Count == 0)
            return;

        Point point = GetPoint();
        Enemy enemy = _pool.GetEnemy();
        Vector3 direction = GetDirection();

        enemy.transform.position = point.transform.position;
        enemy.SetDirection(direction);

        enemy.Release += Release;
    }

    private Point GetPoint() =>
        _points[Random.Range(0, _points.Count)];

    private Vector3 GetDirection() =>
         new Vector3(Random.Range(0, 2) * 2 - 1, 0, Random.Range(0, 2) * 2 - 1);
    
    private void Release(Enemy enemy)
    {
        enemy.Release -= Release;
        _pool.OnRelease(enemy);
    }

    public IEnumerator SpawnWithDelay()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_seconds);
            Spawn();
        }
    }
}
