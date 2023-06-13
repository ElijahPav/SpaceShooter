using UnityEngine;

public class BallController : MonoBehaviour
{
    private const string BULLETTAG = "Bullet";
    private const float DEFAULTHP = 10;
    private float _currentHpValue;
    private float CurrentHp
    {
        get
        {
            return _currentHpValue;
        }
        set
        {
            _currentHpValue = value;
            if (_currentHpValue <= 0)
            {
                Death();
            }
        }
    }


    private void Start()
    {
        _currentHpValue = DEFAULTHP;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(BULLETTAG))
        {
            var bulletDamage = collider.GetComponent<BulletController>().Damage;
            CurrentHp -= bulletDamage;
        }
            
    }
    private void Death()
    {
        gameObject.SetActive(false);
    }

}
