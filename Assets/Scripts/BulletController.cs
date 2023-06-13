using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    private const float LIFETIME = 10f;

    public float Damage { get; private set; }

    public void Shoot(Vector3 direction, float damage, float speed)
    {
        Damage = damage;
        gameObject.SetActive(true);
        StartCoroutine(StartLifetime());
        _rigidbody.velocity = direction * speed;
    }

    private IEnumerator StartLifetime()
    {
        yield return new WaitForSeconds(LIFETIME);
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter()
    {
        gameObject.SetActive(false);
    }
}
