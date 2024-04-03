using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int hp = 100;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject explosion;

    [Range(0, 1)]
    public float speed;

    [Range(5, 15)]
    public float rotationSpeed;

    void Update()
    {
        Vector3 playerPos = player.transform.position;

        Vector3 directionToPlayer = playerPos - transform.position;

        if (directionToPlayer.magnitude <= 10)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, speed * Time.deltaTime);

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            angle -= 90;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
}
