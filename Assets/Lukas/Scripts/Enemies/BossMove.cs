using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private Vector2[] positions;
    [SerializeField] private int[] moveSequence;
    [Range(1f, 10f)][SerializeField] private float moveSpeed;
    private BossScript bossScript;
    private Rigidbody2D rigidbody;
    private float waitTimer = 1f;
    private int sequenceIndex = 0;
    private int positionIndex;
    private Vector2 targetPosition;
    private Vector2 direction;
    private Vector3 offset;
    private bool hasStarted = false;
    void Start()
    {
        bossScript = gameObject.GetComponent<BossScript>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log(offset);
    }

    // Update is called once per frame
    void Update()
    {
        if (positions != null && moveSequence != null)
        {
            if (bossScript.isMoving)
            {
                if (!hasStarted)
                {
                    offset = transform.position;

                    for (int i = 0; i < positions.Length; i++)
                    {
                        positions[i].x += offset.x;
                        positions[i].y += offset.y;
                    }

                    hasStarted = true;

                    SetVelocity();
                }
                else if (Vector3.Magnitude(rigidbody.position - targetPosition) <= .01f)
                {
                    sequenceIndex = (sequenceIndex + 1) % moveSequence.Length;
                    Debug.Log("Sequence index: " + sequenceIndex);
                    positionIndex = moveSequence[sequenceIndex];
                    SetVelocity();
                }
            }
        }
        else
        {
            Debug.Log("Positions array or Move Sequence array is empty");
        }
    }
    private void SetVelocity()
    {
        positionIndex = moveSequence[sequenceIndex];
        targetPosition = positions[positionIndex];
        direction = Vector3.Normalize(targetPosition - rigidbody.position);
        rigidbody.linearVelocityY = direction.y * moveSpeed;
        rigidbody.linearVelocityX = direction.x * moveSpeed;
        Debug.Log("Setting velocity to " + (direction * moveSpeed));
    }
}
