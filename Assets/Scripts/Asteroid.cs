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
    private float newSpeed = 0.8f;


    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rotationAxis = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100)).normalized;
        if (driftingDirection == Vector3.zero)
        {
            driftingDirection = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)).normalized;
        }
    }

    void Update()
    {
        transform.Translate(driftingDirection * DriftingSpeed * Time.deltaTime, Space.World);
        transform.Rotate(rotationAxis, RotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        Debug.Log("norm" + driftingDirection);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerLaser")
        {
            if (other != null)
            {
                Destroy(other.gameObject);
            }

            double angle = GameHelper.changeAngle;

            if (gameObject.tag == "AsteroidBig")
            {
                gameController.increaseScore(20);


                CreateChildAsteroid(asteroidMedium, findVector(driftingDirection, angle));
                CreateChildAsteroid(asteroidMedium, findVector(driftingDirection, -angle));
            }

            if (gameObject.tag == "AsteroidMedium")
            {
                gameController.increaseScore(50);


                CreateChildAsteroid(asteroidSmall, findVector(driftingDirection, angle));
                CreateChildAsteroid(asteroidSmall, findVector(driftingDirection, -angle));
            }

            if (gameObject.tag == "AsteroidSmall")
            {
                gameController.increaseScore(100);
            }
            AudioHelper.CreateAudioObject(ExplosiveAsteroidAudioClip, "AsteroidDestroy", transform.position);
            Destroy(gameObject);
        }
    }

   private Vector3 findVector(Vector3 vector, double angle)
    {
        float x = (float)(vector.x * System.Math.Cos(angle) - vector.z * System.Math.Sin(angle));
        float z = (float)(vector.z * System.Math.Cos(angle) + vector.x * System.Math.Sin(angle));

        return new Vector3(x, 0, z);
    }

    

    private void CreateChildAsteroid(GameObject newAsteroid, Vector3 direction)
    {
        

        var rock = Instantiate(newAsteroid, transform.position, Quaternion.identity);
        var asteroid = rock.GetComponent<Asteroid>();

        asteroid.DriftingSpeed *= 1.5f;
        asteroid.SetDrifintDirection(direction);
    }

    private void SetDrifintDirection(Vector3 direction)
    {
        this.driftingDirection = direction;
    }

}
