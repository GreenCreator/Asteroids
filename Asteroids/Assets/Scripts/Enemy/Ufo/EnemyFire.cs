using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public float timeShotsMin;
    public float timeShotsMax;
    public float angleOffset;
    public GameObject laserPrefab;

    private float timer;
    private GameObject player;

   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Random.Range(timeShotsMin, timeShotsMax))
        { 
            timer = 0;
            var direction = Aim();
            Fire(direction);
        }
    }

    private void Fire(Vector3 direction)
    {
        Instantiate(laserPrefab, transform.position, Quaternion.LookRotation(direction));
    }

    private Vector3 Aim()
    {
        var direction = player.transform.position - transform.position;
        return Quaternion.AngleAxis(Random.Range(-angleOffset, angleOffset), Vector3.up) * direction;
    }
    
}
