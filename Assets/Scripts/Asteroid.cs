using UnityEngine;
public class Asteroid : MonoBehaviour
{
    public float RotationSpeed;
    public float DriftingSpeed;
    public AudioClip ExplosiveAsteroidAudioClip;
    public GameController gameController;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;

    private Vector3 rotationAxis;
    private Vector3 driftingDirection;


    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rotationAxis = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100)).normalized;
        if(driftingDirection == Vector3.zero)
        {
            driftingDirection = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100)).normalized;
        }
    }

    void Update()
    {
        transform.Translate(driftingDirection * DriftingSpeed * Time.deltaTime, Space.World);
        transform.Rotate(rotationAxis, RotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerLaser")
        {
            if (other != null)
            {
                Destroy(other.gameObject);
            }
                if (gameObject.tag == "AsteroidBig")
                {
                    gameController.increaseScore(20);
                    CreateChildAsteroid(asteroidMedium, other.gameObject.transform.right);
                    CreateChildAsteroid(asteroidMedium, other.gameObject.transform.right * -1);
                }

                if (gameObject.tag == "AsteroidMedium")
                {
                    gameController.increaseScore(50);
                    CreateChildAsteroid(asteroidSmall, other.gameObject.transform.right);
                    CreateChildAsteroid(asteroidSmall, other.gameObject.transform.right * -1);
                }

                if (gameObject.tag == "AsteroidSmall")
                {
                    gameController.increaseScore(100);
                }
            AudioHelper.CreateAudioObject(ExplosiveAsteroidAudioClip, "AsteroidDestroy", transform.position);
            Destroy(gameObject);
        }
    }

    private void CreateChildAsteroid(GameObject newAsteroid, Vector3 direction)
    {
        var rock = Instantiate(newAsteroid, transform.position, transform.rotation);
        var asteroid = rock.GetComponent<Asteroid>();
        asteroid.DriftingSpeed *= 1.5f;
        asteroid.SetDrifintDirection(direction);
    }

    private void SetDrifintDirection(Vector3 direction)
    {
        this.driftingDirection = direction;
    }

}
