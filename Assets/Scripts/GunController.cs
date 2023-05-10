using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject BulletSpawn;
    
    private AudioSource gunFireSound;

    // Start is called before the first frame update
    void Start()
    {
        gunFireSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
        gunFireSound.Play();
    }
}
