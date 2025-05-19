using Unity.Mathematics;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    private GameObject moving;

    void Start()
    {
        moving = GameObject.FindGameObjectWithTag("Moving");
    }
    public void SpawnDrop()
    {
        Instantiate(powerUpPrefab, transform.position, transform.rotation, moving.transform);
    }
}
