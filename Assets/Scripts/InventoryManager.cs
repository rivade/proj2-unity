using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    GunController gun;

    TMP_Text bulletOneAmountText;
    TMP_Text bulletTwoAmountText;

    void Start()
    {
        gun = player.GetComponentInChildren<GunController>();
        
        bulletOneAmountText = transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>();
        bulletTwoAmountText = transform.GetChild(1).transform.GetChild(1).GetComponent<TMP_Text>();
    }

    void Update()
    {
        bulletOneAmountText.text = $"{gun.inventory[BulletTypes.Standard]}";
        bulletTwoAmountText.text = $"{gun.inventory[BulletTypes.Heavy]}";
    }

    public void SetBullet(string bulletName)
    {
        BulletTypes bullet = BulletTypes.Standard;

        switch (bulletName.ToLower())
        {
            case "standard":
                bullet = BulletTypes.Standard;
            break;
            case "heavy":
                bullet = BulletTypes.Heavy;
            break;
            default:
                Debug.LogError("Invalid bullet type in inventorybutton: " + bulletName);
                return;
        }

        gun.bulletToFire = bullet;
    }
}
