using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float lifeTimeSeconds;

    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTimeSeconds)
        {
            Destroy(gameObject);
        }

    }
}
