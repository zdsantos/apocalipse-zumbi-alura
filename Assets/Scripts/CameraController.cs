using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerDistance = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + playerDistance;
    }
}
