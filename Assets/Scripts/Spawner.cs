using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Target> _targets = new List<Target>();
    [SerializeField] private Enemy _enemyPrefab;

    private ObjectPool<Enemy> _pool;
    private WaitForSeconds _wait;
    private float _seconds = 2;

    private void Awake()
    {
        _wait = new WaitForSeconds(_seconds);
        _pool = new ObjectPool<Enemy>(Create, OnGetEnemy, OnReleaseEnemy);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private void Spawn()
    {
        Enemy enemy = _pool.Get();

        enemy.transform.position = transform.position;
        enemy.SetTarget(_targets[Random.Range(0, _targets.Count)]);

        enemy.Release += Release;
    }
    
    private void Release(Enemy enemy)
    {
        enemy.Release -= Release;
        _pool.Release(enemy);
    }

    private Enemy Create() =>
     Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnemy(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(false);
    }

    private IEnumerator SpawnWithDelay()
    {
        while (enabled)
        {
            yield return _wait;
            Spawn();
        }
    }
}
