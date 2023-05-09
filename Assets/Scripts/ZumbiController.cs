using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumbiController : MonoBehaviour
{
    public GameObject player;
    public float walkingSpeed = 1.2f;
    public float visionRange = 25;

    private ZumbiStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        walkingSpeed = Random.Range(1.2f, 4f);

        // build a runner, rate 10%
        if(Random.Range(1, 100) <= 10)
        {
            Debug.Log("RUNNER!!!");
            walkingSpeed += 2;
        }

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var playerDistance = Vector3.Distance(transform.position, player.transform.position);
        var playerDirection = (player.transform.position - transform.position).normalized;

        var directionRotation = Quaternion.LookRotation(playerDirection);
        GetComponent<Rigidbody>().MoveRotation(directionRotation);

        if (playerDistance > visionRange)
        {
            SetStatus(ZumbiStatus.Idle);
        }
        else if (playerDistance > 3)
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (playerDirection * walkingSpeed * Time.deltaTime));
            SetStatus(ZumbiStatus.Walking);
        }
        else
        {
            SetStatus(ZumbiStatus.Attacking);
        }
    }

    void AttackPlayer()
    {
        Time.timeScale = 0;
        player.GetComponent<PlayerController>().ReceiveDamage(1);
    }

    private void SetStatus(ZumbiStatus status)
    {
        _status = status;
        GetComponent<Animator>().SetInteger("Zumbi_Status", ((int)status));
    }

    enum ZumbiStatus
    {
        Idle,
        Walking,
        Eating,
        Attacking
    }
}
