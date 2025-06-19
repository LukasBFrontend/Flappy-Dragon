using UnityEngine;

public class FlickerScript : MonoBehaviour
{
    [SerializeField] private Color damageColor;
    private SpriteRenderer sprite;
    private int index = 0;
    private float interval;
    private float iterations;
    private float timer = 0;
    private bool isFlickering = false;

    void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        OnOff();
    }
    public void Flicker(int iterations, float interval)
    {
        Debug.Log("It's flickering time!");
        this.interval = interval;
        this.iterations = iterations * 2;
        isFlickering = true;
    }
    private void OnOff()
    {
        if (!isFlickering || index >= iterations)
        {
            timer = 0;
            index = 0;
            isFlickering = false;
            return;
        }

        Debug.Log("Index: " + index);
        sprite.color = index % 2 == 0 ? damageColor : Color.white;

        if (timer > (index + 1) * interval / 2) index++;

        timer += Time.deltaTime;
    }
}
