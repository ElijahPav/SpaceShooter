using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    private const float SPEED = 5;
    private const float INERTIAFORCE = 10;
    private const float DROPFORCE = 100;

    [SerializeField] private Camera _characterCamera;

    private Rigidbody _rigidbody;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    [Range(0.1f, 9f)][SerializeField] float sensitivity = 10f;

    private void Awake()
    {
        if (_characterCamera == null)
        {
            _characterCamera = Camera.main;
        }
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0)));
        _rigidbody.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * SPEED * Time.deltaTime)
                                                   + (transform.right * Input.GetAxis("Horizontal") * SPEED * Time.deltaTime));  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Drop();
        }
    }

    public void MovePlayerByInertia(Vector3 direction)
    {
        Vector3 inertiaDirection = -_characterCamera.transform.forward;
        _rigidbody.AddForce(inertiaDirection * INERTIAFORCE, ForceMode.Impulse);
    }

    private void Drop()
    {
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        Vector3 dropDerection = -transform.up;
        var ray = new Ray(transform.position, dropDerection);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var crystal = hit.transform.gameObject.GetComponent<ÑrystalController>();
            if(crystal != null)
            {
                crystal.GetDamageByDrop(hit.distance);
            }
        }
        _rigidbody.AddForce( dropDerection * DROPFORCE, ForceMode.Impulse);
    }
}
