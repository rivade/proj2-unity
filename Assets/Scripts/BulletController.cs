using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum BulletTypes
{
    Standard,
    Heavy,
    EnemyBullet
}

public class BulletController : MonoBehaviour
{
    [SerializeField]
    BulletTypes bulletType;

    [SerializeField]
    GameObject explosion;

    private GameObject player;
    private int speed;
    private int damage;

    private float instantiationTime;
    private float timeAlive = 0;

    void Start()
    {
        instantiationTime = Time.time;
        switch (bulletType)
        {
            case BulletTypes.Standard:
                speed = 10;
                damage = 50;
                break;

            case BulletTypes.Heavy:
                speed = 5;
                damage = 100;
                break;

            case BulletTypes.EnemyBullet:
                speed = 10;
                damage = 10;
                break;
        }

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        timeAlive = Time.time - instantiationTime;
        if (timeAlive >= 3)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (bulletType == BulletTypes.EnemyBullet)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(this.gameObject);
                HealthHandler.UpdateHealth(damage);
            }
        }
        else switch (other.gameObject.tag)
            {
                case "wall":
                    other.gameObject.GetComponent<WallBreakHandler>().OnHit
                    (new Vector3Int((int)transform.position.x, (int)transform.position.y, 0), damage);
                    Destroy(this.gameObject);
                    break;

                case "enemy":
                    other.GetComponent<EnemyController>().OnHit(damage);
                    Destroy(this.gameObject);
                    break;
            }
    }

    void OnDisable()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        float shakeMagnitude = Mathf.Clamp01(1f - distanceToPlayer / 7.5f) * 0.5f;

        Camera.main.GetComponent<CameraController>().StartShake(0.75f, shakeMagnitude);
        GameObject explosionInstance = Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(explosionInstance, 0.75f);
    }
}
