using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    public bool isEmpty
    {
        get
        {
            return enemies.Count == 0;
           
        }
    }

    private void Start()
    {
        foreach(var item in enemies)
        {
            item.onDeath += OnDeath;
        }
    }

    private void OnDeath()
    {
        enemies.RemoveAt(0);
    }
}
