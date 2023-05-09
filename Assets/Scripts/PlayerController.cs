using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 2;
    public float runningSpeed = 6;
    public float jumpForce = 6;
    public Rigidbody rb;
    public LayerMask GroundMask;
    public GameObject GameOverText;

    private bool _isRunning = true;
    private Vector3 movementDirection;
    private bool _isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.CapsLock))
            _isRunning = !_isRunning;

        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        movementDirection = new Vector3(xAxis, 0, zAxis);

        if (movementDirection != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("Correndo", _isRunning);
            GetComponent<Animator>().SetBool("Andando", !_isRunning);
        }
        else
        {
            GetComponent<Animator>().SetBool("Correndo", false);
            GetComponent<Animator>().SetBool("Andando", false);
        }

        if (!_isAlive)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    private void FixedUpdate()
    {
        float speed = _isRunning ? runningSpeed : walkingSpeed;

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

    public void GameOver(float damage)
    {
        _isAlive = false;
        GameOverText.SetActive(true);
    }
}
