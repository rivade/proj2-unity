using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int hp = 100;

    GameObject player;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject explosion;

    [Range(0, 1)]
    public float speed;

    [Range(5, 15)]
    public float rotationSpeed;

    public int timeBetweenShots;
    private float timeSinceShot = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        Vector3 directionToPlayer = playerPos - transform.position;

        if (directionToPlayer.magnitude <= 10)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, speed * Time.deltaTime);
            RotateEnemyToPlayer(directionToPlayer);
            Shoot();
        }

        if (hp <= 0) Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        ScoreHandler.UpdateScore(100);
        Instantiate(explosion, transform.position, transform.rotation);
    }

    public void OnHit(int damage)
    {
        hp -= damage;
    }

    private void Shoot()
    {
        timeSinceShot += Time.deltaTime;

        if (timeSinceShot >= timeBetweenShots)
        {
            Instantiate(bullet, transform.GetChild(1).gameObject.transform.position, transform.rotation);
            timeSinceShot = 0;
        }
    }

    private void RotateEnemyToPlayer(Vector2 directionToPlayer)
    {
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
