using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField]
    GameObject standardBullet;

    [SerializeField]
    GameObject heavyBullet;

    [SerializeField]
    RectTransform inventoryOnScreen;

    [HideInInspector]
    public BulletTypes bulletToFire = BulletTypes.Standard;

    [HideInInspector]
    public Dictionary<BulletTypes, int> inventory = new() { { BulletTypes.Standard, 50 }, { BulletTypes.Heavy, 10 } };

    public float rotationSpeed = 5f;

    private InputAction mousePositionAction;

    void Start()
    {
        mousePositionAction = new InputAction(binding: "<Mouse>/position");
        mousePositionAction.Enable();
    }

    void OnFire()
    {
        if (inventory[bulletToFire] > 0 && !IsCursorOverInventory())
        {
            switch (bulletToFire)
            {
                case BulletTypes.Standard:
                    Instantiate(standardBullet, transform.GetChild(0).gameObject.transform.position, transform.rotation);
                    break;
                case BulletTypes.Heavy:
                    Instantiate(heavyBullet, transform.GetChild(0).gameObject.transform.position, transform.rotation);
                    break;
            }
            inventory[bulletToFire]--;
        }
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

    private bool IsCursorOverInventory()
    {   
        Rect inventoryRect = 
        new(Screen.width - inventoryOnScreen.rect.width, Screen.height - inventoryOnScreen.rect.height, inventoryOnScreen.rect.width, inventoryOnScreen.rect.height);
        
        Vector2 mousePosition = mousePositionAction.ReadValue<Vector2>();

        return inventoryRect.Contains(mousePosition);
    }
}
