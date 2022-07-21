using TMPro;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator),typeof(Collider))]
public class Enemy : MonoBehaviour,IDamage
{
    public EnemyAction OnDeath;
    public delegate void EnemyAction(Enemy enemy);

    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _health = 10;
    [SerializeField] private List<Rigidbody> _rbList = new List<Rigidbody>();

    private Collider _collider;
    private Animator _animator;
    
    private void Awake()
    {
        ChangeKinematic(true);
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        _text = GetComponentInChildren<TMP_Text>();
        _text.GetComponentInParent<Canvas>().worldCamera = Camera.main;
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
        OnDeath?.Invoke(this);
    }

    public void GetDamage(float damage)
    {
        _health -= damage;
        _text.text = _health.ToString();
        if(_health<=0)
        {
            Death();
        }
    }
}
