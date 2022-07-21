using System;
using UnityEngine;
using System.Collections.Generic;

public class Shoot : MonoBehaviour
{
    [SerializeField] private int _quantity;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private List<Bullet> _pool;

    private void OnEnable()
    {
        AnimationHandler.onShot += OnShot;
    }

    private void Start()
    {
        _pool = new List<Bullet>();
        for(var i = 0; i < _quantity; i++)
        {
            CreateBullet();
        }
    }

    private void OnDisable()
    {
        AnimationHandler.onShot -= OnShot;
    }

    private Bullet CreateBullet()
    {
        var bullet = Instantiate(_bullet);
        bullet.gameObject.SetActive(false);
        _pool.Add(bullet);
        return bullet;
    }

    private bool TryGetBullet(out Bullet bullet)
    {
        foreach(var item in _pool)
        {
            if(!item.gameObject.activeInHierarchy)
            {
                bullet = item;
                item.gameObject.SetActive(true);
                bullet.Init(transform);
                item.Shot();
                return true;
            }
        }
        bullet = null;
        return false;
    }

    public void OnShot()
    {
        var result = TryGetBullet(out var bullet);
        if (!result)
        {
            throw new Exception("Переполнение пула");
        }
    }
}
