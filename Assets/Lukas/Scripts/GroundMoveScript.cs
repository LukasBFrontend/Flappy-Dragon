using UnityEngine;

public class GroundMoveScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 5;
    public PlayerScript playerScript;
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.playerIsAlive)
        {
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        }

        /* if (transform.position.x < deadZone)
        {
            Debug.Log("Ground deleted");
            Destroy(gameObject);
        } */

    }
}
