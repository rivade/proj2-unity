using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    public int width, height;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 10; i++)
        {
            int posX = Random.Range(0, width);
            int posY = Random.Range(0, height);
            Instantiate(enemy, new Vector3(posX, posY, -5), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
