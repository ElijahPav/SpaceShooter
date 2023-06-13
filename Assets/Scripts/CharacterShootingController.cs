using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class CharacterShootingController : MonoBehaviour
{
    private const float RELOADTIME = 0.5f;
    private const float DEFAULTBULLETSPEED = 10;
    private const float DEFAULTBULLETDAMAGE = 1;

    private const float BULLETDAMAGEPERCENT = 0.2f;
    private const float BULLETSPEEDPERCENT = 0.1f;

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Camera _characterCamer;
    [SerializeField] private CharacterMovementController _movementController;
    [SerializeField] private CharacterCrystalCollector _crystalCollector;

    [SerializeField] private TextMeshProUGUI _shootsText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _speedText;


    private float _bulletSpeed;
    private float _bulletDamage;
    private int _shootsValue;

    private ObjectPooler _objectPooler;

    private int _bulletIndex;

    private bool _canShoot = true;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _bulletDamage = DEFAULTBULLETDAMAGE;
        _bulletSpeed = DEFAULTBULLETSPEED;
        _shootsValue = 0;
        _objectPooler = ObjectPooler.Instance;

        _bulletIndex = _objectPooler.AddObject(_bulletPrefab, 10);
        
        _shootsText.text = _shootsValue.ToString();
        _damageText.text = _bulletDamage.ToString();
        _speedText.text = _bulletSpeed.ToString();

        _crystalCollector.CrystalCollected += IncreaseBulletParameters;

    }

    private void OnDestroy()
    {
        _crystalCollector.CrystalCollected -= IncreaseBulletParameters;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _canShoot)
        {
            Shoot();
            _movementController.MovePlayerByInertia(GetCharacterDerection());
            StartCoroutine(WaitReload());
        }
    }

    private void Shoot()
    {
        var bullet = _objectPooler.GetPooledObject(_bulletIndex);
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;

        bullet.GetComponent<BulletController>().Shoot(GeteBulletDerecion(),_bulletDamage,_bulletSpeed);
        _shootsValue++;
        _shootsText.text = _shootsValue.ToString();
    }

    private Vector3 GeteBulletDerecion()
    {
        Ray ray = _characterCamer.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var rayToHit = new Ray(_shootPoint.position, hit.point - _shootPoint.position);
            return rayToHit.direction;
        }
        else
        {
            return ray.direction;
        }
    }

    private Vector3 GetCharacterDerection()
    {
       return  _characterCamer.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)).direction *-1;
    }

    private IEnumerator WaitReload()
    {
        _canShoot = false;
        yield return new WaitForSeconds(RELOADTIME);
        _canShoot = true;
    }

    public void  IncreaseBulletParameters()
    {
        _bulletSpeed += DEFAULTBULLETSPEED * BULLETSPEEDPERCENT;
        _bulletDamage += _bulletDamage * BULLETDAMAGEPERCENT;

        _damageText.text = _bulletDamage.ToString("F4");
        _speedText.text = _bulletSpeed.ToString("F4");
    }
}
