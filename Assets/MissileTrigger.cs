using UnityEngine;

public class MissileTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject missileWarning;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(missileWarning, new Vector3(object.transform))
    }
}
