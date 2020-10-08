using UnityEngine;

public class UfoEmitter : MonoBehaviour
{
    public GameObject ufo;
    public float minDelay, maxDelay;
    private float nextLaunch;
    private GameObject ufoObject;

    private void Start()
    {
        nextLaunch = Time.time + Random.Range(minDelay, maxDelay);
    }
    void Update()
    {
        ufoObject = GameObject.FindGameObjectWithTag("EnemyLaser");
        if (GameHelper.isFirstStartUfo)
        {
            FirstStart();
        }
        else
        {
            UfoCreater();
        }

    }

    private void UfoCreater()
    {
        if (!ufoObject)
        {
            if (Time.time > nextLaunch)
            {
                float zPos = Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2);
                Instantiate(ufo, new Vector3(transform.position.x, transform.position.y, zPos), Quaternion.identity);

                nextLaunch = Time.time + Random.Range(minDelay, maxDelay);
            }
        }
    }
    private void FirstStart()
    {
        if (Time.time > nextLaunch)
        {
            UfoCreater();
            GameHelper.isFirstStartUfo = false;
        }
    }
}
