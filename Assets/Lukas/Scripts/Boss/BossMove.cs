using System;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private Vector2[] positions;
    [SerializeField] private int[] moveSequence;
    [Range(1f, 10f)][SerializeField] private float moveSpeed;
    [Range(0f, 5f)][SerializeField] private float waitTime = 1f;
    private BossScript bossScript;
    private Rigidbody2D rigidbody;
    private float waitTimer;
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
        waitTimer = waitTime;
    }

    void Update()
    {
        if (!bossScript.isMoving) return;
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
        else if (Vector3.Distance(rigidbody.position, targetPosition) <= .08f)
        {
            if (waitTimer == waitTime)
            {
                sequenceIndex = (sequenceIndex + 1) % moveSequence.Length;
                positionIndex = moveSequence[sequenceIndex];
                rigidbody.linearVelocity = Vector2.zero;
            }

            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                SetVelocity();
                waitTimer = waitTime;
            }
        }
    }
    private void SetVelocity()
    {
        positionIndex = moveSequence[sequenceIndex];
        targetPosition = positions[positionIndex];
        direction = Vector3.Normalize(targetPosition - rigidbody.position);
        rigidbody.linearVelocity = direction * moveSpeed;
    }
}
