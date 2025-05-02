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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemyMoveScript.isMoving = true;
            enemyLogicScript.isActive = true;
        }
    }
}
