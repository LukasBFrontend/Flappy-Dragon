using UnityEngine;
using System.Collections;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private Drop[] drops;
    private GameObject moving;

    void Start()
    {
        moving = GameObject.FindGameObjectWithTag("Moving");
    }
    public void SpawnDrop()
    {
        float random = Random.Range(0, 100);

        foreach (Drop drop in drops)
        {
            if (random <= drop.dropChance) Instantiate(drop.dropPrefab, transform.position, transform.rotation, moving.transform);
        }
    }

    [System.Serializable]
    public class Drop
    {
        public GameObject dropPrefab;
        [Range(0, 100)] public float dropChance;
    }
}
