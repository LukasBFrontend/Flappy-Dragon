using UnityEngine;

public class BossMissile : MonoBehaviour
{
    [Range(1f, 20f)][SerializeField] private float flightSpeed = 5f;
    private GameObject player;
    private Vector3 targetPosition;
    private Rigidbody2D rigidbody;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        if (player == null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        targetPosition = player.transform.position;

        rigidbody.linearVelocity = (targetPosition - transform.position).normalized * flightSpeed;
    }
}
