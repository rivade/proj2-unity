using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField]
    GameObject standardBullet;

    [SerializeField]
    GameObject heavyBullet;

    public float rotationSpeed = 5f;

    private InputAction mousePositionAction;

    void Start()
    {
        mousePositionAction = new InputAction(binding: "<Mouse>/position");
        mousePositionAction.Enable();
    }

    void OnFire()
    {
        Instantiate(standardBullet, transform.GetChild(0).gameObject.transform.position, transform.rotation);
    }

    void Update()
    {
        Vector2 mousePos = mousePositionAction.ReadValue<Vector2>();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.y - transform.position.y));

        Vector3 direction = mouseWorldPos - transform.position;
        direction.z = 0f;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnDisable()
    {
        mousePositionAction.Disable();
    }
}
