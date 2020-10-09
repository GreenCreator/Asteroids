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

        //Debug.Log("norm" + driftingDirection);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerLaser")
        {
            if (other != null)
            {
                Destroy(other.gameObject);
            }

            Vector3 targetDir = driftingDirection - transform.position;
            float angle = Vector3.Angle(targetDir, driftingDirection);





            if (gameObject.tag == "AsteroidBig")
            {
                gameController.increaseScore(20);

                var findNewVector = findVector();

                CreateChildAsteroid(asteroidMedium, findNewVector);
                CreateChildAsteroid(asteroidMedium, new Vector3(-findNewVector.z, 0 ,findNewVector.x));
            }

            if (gameObject.tag == "AsteroidMedium")
            {
                gameController.increaseScore(50);

                var findNewVector = findVector();

                CreateChildAsteroid(asteroidSmall, findNewVector);
                CreateChildAsteroid(asteroidSmall, new Vector3(-findNewVector.z, 0, -findNewVector.x));
            }

            if (gameObject.tag == "AsteroidSmall")
            {
                gameController.increaseScore(100);
            }
            AudioHelper.CreateAudioObject(ExplosiveAsteroidAudioClip, "AsteroidDestroy", transform.position);
            Destroy(gameObject);
        }
    }

   private Vector3 findVector()
    {
        for (float i = 0; i <= 1.1f; i += 0.02f)
        {
            for (float j = 0; j <= 1.1f; j += 0.02f)
            {
                float x = driftingDirection.x;
                float z = driftingDirection.z;

                
                double axis = (x*i+z*j)  / System.Math.Sqrt(x*x + z*z) * System.Math.Sqrt(i*i + j*j);

                if (System.Math.Round(axis, 2) == 0.52d)
                {
                    Debug.Log("FIND");
                    return new Vector3(i, 0, j);
                }
            }
        }
        return Vector3.one;
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
