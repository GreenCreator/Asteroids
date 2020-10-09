using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform[] SpawnPositions;
    public GameObject LaserPrefab;
    public float speedLaser = 10;

    void Update()
    {
        if (!GameHelper.isPausedGame)
        {
            if (GameHelper.isChangeSetting)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    firePlayer();
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
                {
                    firePlayer();
                }
            }
        }
        
        
    }
    private void firePlayer()
    {
        foreach (var spawnPosition in SpawnPositions)
        {
            GameObject bullet = Instantiate(LaserPrefab, spawnPosition.position, spawnPosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speedLaser;
        }
    }
}
