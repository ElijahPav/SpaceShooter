using UnityEngine;

public class CharacterCameraController : MonoBehaviour
{
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    Vector2 rotation = Vector2.zero;

    void Update()
    {
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = yQuat;
    }
}
