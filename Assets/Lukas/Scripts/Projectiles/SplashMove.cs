using UnityEngine;

public class SplashMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    void Update()
    {
        transform.position = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0);
    }
}
