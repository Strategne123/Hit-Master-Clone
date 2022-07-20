using System;
using UnityEngine;
using System.Collections.Generic;


public class Enemy : MonoBehaviour,IDamage
{
    public Action onDeath;
    [SerializeField] List<Rigidbody> _rbList = new List<Rigidbody>();

    private Collider _collider;
    private Animator _animator;

    private void Awake()
    {
        ChangeKinematic(true);
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
    }

    private void ChangeKinematic(bool value)
    {
        foreach (var rb in _rbList)
        {
            rb.isKinematic = value;
        }
    }

    public void Death()
    {
        _collider.enabled = false;
        _animator.enabled = false;
        ChangeKinematic(false);
        onDeath?.Invoke();
    }

    public void GetDamage(float damage)
    {
        Death();
    }
}
