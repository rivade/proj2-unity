using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject explosion;

    private float instantiationTime;
    private float timeAlive = 0;

    void Start()
    {
        instantiationTime = Time.time;
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
        switch (other.gameObject.tag)
        {
            case "wall":
                other.gameObject.GetComponent<WallBreakHandler>().OnHit(new Vector3Int((int)transform.position.x, (int)transform.position.y, 0));
                Destroy(this.gameObject);
                break;

            case "enemy":
                ScoreHandler.UpdateScore(100);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
        }
    }

    void OnDestroy()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeMagnitude = Mathf.Clamp01(1f - (distanceToPlayer / 10f)) * 0.75f;

        Camera.main.GetComponent<CameraController>().StartShake(0.75f, shakeMagnitude);
        GameObject explosionInstance = Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(explosionInstance, 0.75f);
    }
}
