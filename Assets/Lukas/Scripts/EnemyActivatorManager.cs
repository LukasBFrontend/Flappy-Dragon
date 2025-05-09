using System.Collections.Generic;
using UnityEngine;

public class EnemyActivatorManager : MonoBehaviour
{
    public float activationRadius = 14f; // Enemies outside this will be disabled
    public float checkInterval = 0.5f;

    private List<GameObject> backgroundTiles = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> enemiesToRemove = new List<GameObject>();
    private float timer;

    void Start()
    {
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        foreach (Transform child in background.transform)
        {
            backgroundTiles.Add(child.gameObject);
        }

        Enemy[] enemyScripts = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy em in enemyScripts)
        {
            enemies.Add(em.gameObject);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;


            foreach (GameObject enemy in enemies)
            {
                var script = enemy.GetComponent<Enemy>();
                if (script != null && script.isDead)
                {
                    enemiesToRemove.Add(enemy);
                    continue;
                }

                float distance = enemy.transform.position.x;
                bool shouldBeActive = distance < activationRadius;

                if (enemy.activeSelf != shouldBeActive)
                    enemy.SetActive(shouldBeActive);
            }

            foreach (GameObject backgroundTile in backgroundTiles)
            {
                float distance = backgroundTile.transform.position.x;
                bool shouldBeActive = distance < activationRadius + 20f && distance > -activationRadius - 10f;

                if (backgroundTile.activeSelf != shouldBeActive)
                    backgroundTile.SetActive(shouldBeActive);
            }
        }
        foreach (GameObject dead in enemiesToRemove)
        {
            enemies.Remove(dead);
        }
        enemiesToRemove.Clear();
    }
}
