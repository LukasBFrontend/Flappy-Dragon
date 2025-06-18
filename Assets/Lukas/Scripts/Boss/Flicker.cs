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

        OnOff();
    }

    public void Flicker(int iterations, float interval)
    {
        this.interval = interval;
        this.iterations = iterations * 2;
    }
    private void OnOff()
    {
        if (!isFlickering || index == iterations)
        {
            timer = 0;
            return;
        }

        if (timer % interval > interval / 2) index++;

        sprite.color = index % 0 == 0 ? damageColor : Color.white;

        timer += Time.deltaTime;
    }
}
