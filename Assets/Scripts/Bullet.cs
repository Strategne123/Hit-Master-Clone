using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage = 5.0f;
    [SerializeField] private float _bulletSpeed = 10.0f;

    private Rigidbody _rb;
    private Vector3 _startPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        var damaged = collision.gameObject.GetComponent<IDamage>();
        if (damaged != null)
        {
            damaged.GetDamage(_damage);
        }
        ReturnToPool();
    }

    private IEnumerator DefferedDestruction(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        StopAllCoroutines();
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void Init(Transform start)
    {
        _startPos = start.position;
        transform.rotation = start.rotation;
    }

    public void Shot()
    {
        transform.position = _startPos;
        _rb.velocity = (transform.forward * _bulletSpeed);
        StartCoroutine(DefferedDestruction(5f));
    }
}
