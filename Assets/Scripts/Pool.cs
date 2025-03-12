using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField]private Enemy _enemyPrefab;
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(Create, OnGetEnemy ,OnReleaseEnemy);
    }

    public Enemy GetEnemy() =>
        _pool.Get();

    public void OnRelease(Enemy enemy)
    {
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
}
