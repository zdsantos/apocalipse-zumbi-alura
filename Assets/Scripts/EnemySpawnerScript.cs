using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject ZumbiObject;
    public float SpawnRate = 3f;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        var variance = Random.Range(-1.0f, 1.0f);
        SpawnRate += variance;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= SpawnRate)
        {
            Instantiate(ZumbiObject, transform.position, transform.rotation);
            timer = 0.0f;
        }
    }
}
