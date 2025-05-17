using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private EnemyMove enemyMoveScript;
    private Enemy enemyLogicScript;

    private void Start()
    {
        enemyMoveScript = enemy.GetComponent<EnemyMove>();
        enemyLogicScript = enemy.GetComponent<Enemy>();
    }

    private void Update()
    {
        if (transform.position.x <= 14)
        {
            {
                enemyMoveScript.isMoving = true;
                enemyLogicScript.isActive = true;
            }
        }
    }
}
