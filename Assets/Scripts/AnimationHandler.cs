using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour, IAnimated
{
    public static Action onShot;

    [SerializeField] private Transform _gun;
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _holster;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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

    public void OnShot()
    {
        onShot?.Invoke();
    }

    public bool IsState(AnimStates state)
    {
        return (AnimStates)_animator.GetInteger("State") == state;
    }

    public void SetState(AnimStates state)
    {
        _animator.SetInteger("State", (int)state);
    }
}
