using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    public static Action onShot;

    [SerializeField] private Transform _gun;
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _holster;

    private Animator _animator;

    public AnimStates AnimState
    {
        get
        {
            return (AnimStates)_animator.GetInteger("State");
        }
        set
        {
            _animator.SetInteger("State", (int)value);
        }
    }

    public void Play(int state)
    {
        AnimState = (AnimStates)state;
    }

    public void OnShot()
    {
        onShot?.Invoke();
    }

    private void GunTo(Transform destination)
    {
        _gun.SetParent(destination);
        _gun.localPosition = Vector3.zero;
        _gun.rotation = destination.rotation;
    }

    public void GetGun()
    {
        GunTo(_hand);
    }

    public void PutGun()
    {
        GunTo(_holster);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
