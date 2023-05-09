using UnityEngine;

public class ZumbiController : MonoBehaviour
{
    public GameObject Player;
    public float ValkingSpeed = 1.2f;
    public float VisionRange = 25;

    private Rigidbody rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        ValkingSpeed = Random.Range(1.2f, 4f);

        // build a runner, rate 10%
        if(Random.Range(1, 100) <= 10)
        {
            Debug.Log("RUNNER!!!");
            ValkingSpeed += 2;
        }

        Player = GameObject.FindWithTag("Player");

        int type = Random.Range(1, 28);
        transform.GetChild(type).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var playerDistance = Vector3.Distance(transform.position, Player.transform.position);
        var playerDirection = (Player.transform.position - transform.position).normalized;

        var directionRotation = Quaternion.LookRotation(playerDirection);
        rb.MoveRotation(directionRotation);

        if (playerDistance > VisionRange)
        {
            SetStatus(ZumbiStatus.Idle);
        }
        else if (playerDistance > 3)
        {
            rb.MovePosition(rb.position + (playerDirection * ValkingSpeed * Time.deltaTime));
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
        Player.GetComponent<PlayerController>().GameOver(1);
    }

    private void SetStatus(ZumbiStatus status)
    {
        animator.SetInteger("Zumbi_Status", ((int)status));
    }

    enum ZumbiStatus
    {
        Idle,
        Walking,
        Eating,
        Attacking
    }
}
