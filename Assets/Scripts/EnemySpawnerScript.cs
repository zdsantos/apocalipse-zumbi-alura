using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject ZumbiObject;

    public float spawnRate = 3f;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        var variance = Random.Range(-1.0f, 1.0f);
        spawnRate += variance;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            Instantiate(ZumbiObject, transform.position, transform.rotation);
            timer = 0.0f;
        }
    }
}
