using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage = 5.0f;
    [SerializeField] private float _bulletSpeed = 1000;

    private Rigidbody _rb;
    private Vector3 _startPos;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(Transform start)
    {
        _startPos = start.position;
        transform.rotation = start.rotation;
    }

    public void Shot()
    {
        transform.position = _startPos;
        _rb.AddForce(transform.forward * _bulletSpeed);
        StartCoroutine(DefferedDestruction(5f));
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

    private void OnCollisionEnter(Collision collision)
    {

        var damaged = collision.gameObject.GetComponent<IDamage>();
        print(collision.gameObject.name);
        if (damaged != null)
        {
            damaged.GetDamage(_damage);
        }
        ReturnToPool();
    }
}
