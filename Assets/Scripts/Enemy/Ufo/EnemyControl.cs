using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speedUfo = 2f;
    public AudioClip explosiveUfoAudioClip;
    public GameController gameController;

    private Vector3 direction;
    void Start()
    {
        direction = new Vector3(Random.Range(-100, 100), 0, 0).normalized;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        transform.Translate(direction * speedUfo * Time.deltaTime, Space.World);
        if (GameHelper.isFirstStartPlayer && gameObject != null)//в случае начала новой игры, убрать нло
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerLaser")
        {
            DestroyUfo(other);
            gameController.increaseScore(200);
        }

        if (other.tag == "AsteroidBig" || other.tag == "AsteroidMedium" || other.tag == "AsteroidSmall")
        {
            DestroyUfo(other);
        }
    }

    private void DestroyUfo(Collider other)
    {
        Destroy(other.gameObject);
        AudioHelper.CreateAudioObject(explosiveUfoAudioClip, "UfoDestroy", transform.position);
        Destroy(this.gameObject);
    }
}
