using UnityEngine;

public class EmitterSpawn : MonoBehaviour
{
    public GameObject[] asteroid;
    public float minDelay, maxDelay;

    private int countAsteroid = 2;

    private GameObject asteroidSmall;
    private GameObject asteroidMedium;
    private GameObject asteroidBig;

    void Update()
    {
        asteroidSmall = GameObject.FindGameObjectWithTag("AsteroidSmall");
        asteroidMedium = GameObject.FindGameObjectWithTag("AsteroidMedium");
        asteroidBig = GameObject.FindGameObjectWithTag("AsteroidBig");

        if (asteroidBig == null && asteroidMedium == null && asteroidSmall == null)
        {
            SpawnAsteroid(countAsteroid);
            countAsteroid++;
        }
    }

    private void SpawnAsteroid(int countAsteroid)
    {
        int numberAsteroid = 0;
        for (int i = 0; i < countAsteroid; i++)
        {
            //В начале уровня астероиды всегда крупные.
            if (countAsteroid > 2)
            {
                numberAsteroid = Random.Range(0, asteroid.Length);
            }
            float xPos = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
            Instantiate(asteroid[numberAsteroid], new Vector3(xPos, 0, transform.position.z), Quaternion.identity);
        }
    }
}
