using UnityEngine;

public class BackgroundMoveScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    void Start()
    {

    }

    void Update()
    {
        float transformX = gameObject.transform.position.x - moveSpeed * Time.deltaTime;
        gameObject.transform.position = new Vector3(transformX, 0, 0);
    }
}
