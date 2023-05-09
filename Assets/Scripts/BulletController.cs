using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float ExplosionForce = 0.04f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ExplosionForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
