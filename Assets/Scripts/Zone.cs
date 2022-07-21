using UnityEngine;
using System.Collections.Generic;

public class Zone : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();

    public bool IsEmpty
    {
        get
        {
            return enemies.Count == 0;
        }
    }

    private void OnEnable()
    {
        foreach (var item in enemies)
        {
            item.OnDeath += OnDeath;
        }
    }

    private void OnDisable()
    {
        foreach (var item in enemies)
        {
            item.OnDeath -= OnDeath;
        }
    }

    private void OnDeath(Enemy enemy)
    {
        enemy.OnDeath -= OnDeath;
        enemies.Remove(enemy);
    }
}
