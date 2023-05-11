    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float WalkingSpeed = 2;
    public float RunningSpeed = 6;
    public float jumpForce = 6;
    public LayerMask GroundMask;
    public GameObject GameOverText;
    public int HP = 10;

    private Rigidbody rb;
    private Animator animator;
    private bool isRunning = true;
    private Vector3 movementDirection;
    private bool isAlive => HP > 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.CapsLock))
            isRunning = !isRunning;

        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        movementDirection = new Vector3(xAxis, 0, zAxis);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("Correndo", isRunning);
            animator.SetBool("Andando", !isRunning);
        }
        else
        {
            animator.SetBool("Correndo", false);
            animator.SetBool("Andando", false);
        }

        if (!isAlive)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    private void FixedUpdate()
    {
        float speed = isRunning ? RunningSpeed : WalkingSpeed;

        rb.MovePosition(rb.position + (movementDirection * speed * Time.deltaTime));

        Ray pointerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pointerRay.origin, pointerRay.direction * 100, Color.red);

        RaycastHit groundHit;

        if (Physics.Raycast(pointerRay, out groundHit, 100, GroundMask))
        {
            Vector3 aimDirection = (groundHit.point - transform.position).normalized;

            aimDirection.y = transform.position.y;

            Quaternion aimRotation = Quaternion.LookRotation(aimDirection);
            rb.MoveRotation(aimRotation);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("damage taken");

        HP -= damage;
        
        if (!isAlive)
        {
            Time.timeScale = 0;
            GameOverText.SetActive(true);
        }
    }
}
