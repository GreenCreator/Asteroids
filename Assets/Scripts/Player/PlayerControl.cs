using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    public float speedPlayer;//скорость коробля
    public float speedTurn; //скорость поворота
    public AudioClip explosivePlayerAudioClip;
    public GameObject meshPlayer;
    public Image[] lives;

    private Rigidbody rigidbody;
    private float torqueInput;
    private float trottleInput;

    private static int numberOfLives = 3;

    private static bool changeLive = false;
    private static bool isBoxDisable = false;

    private static float timeVulnerability = 3f; // время неуязвимости
    private BoxCollider box;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        box = gameObject.GetComponent<BoxCollider>();

    }

    void Update()
    {
        if (GameHelper.isChangeSetting)
        {
            torqueInput = Input.GetAxisRaw("Horizontal");

        } else
        {
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, playerMouseRotation(), speedTurn * 0.01f);

        }
        trottleInput = Input.GetAxisRaw("Vertical");
        trottleInput = Mathf.Clamp(trottleInput, 0, 1);

        changeLivePlayer();
        changeLiveImage();

        vulnerability();
    }

    private void vulnerability()
    {
        if (GameHelper.isFirstStartPlayer)
        {
            if (box != null)
            {
                box.enabled = false;
                boxEnabled();
            }
        }
        else if (isBoxDisable)
        {
            boxEnabled();
        }
    }


    private void changeLivePlayer()
    {
        if (!changeLive) return;

        numberOfLives--;
        changeLive = false;

        if (numberOfLives <= 0)
        {
            GameHelper.restartGame();
            numberOfLives = lives.Length;
            SceneManager.LoadScene("Game");
        }
    }

    private void changeLiveImage()
    {
        for (int i = 0; i < lives.Length; i++)
            if (i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
    }

    private void boxEnabled()
    {
        if (Time.time > timeVulnerability)
        {
            Debug.Log(Time.time);
            if (box != null)
            {
                box.enabled = true;
                isBoxDisable = false;
                GameHelper.isFirstStartPlayer = false;
            }
        }
    }

    /**
     * Управление мышкой
     */
    Quaternion playerMouseRotation()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = gameObject.transform.position.z;
        Vector3 direction = Camera.main.ScreenToWorldPoint(mouse) - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.up);
    }

    void FixedUpdate()
    {
        rigidbody.AddRelativeForce(Vector3.forward * trottleInput * speedPlayer, ForceMode.Force);
        rigidbody.AddTorque(Vector3.up * torqueInput * speedTurn, ForceMode.Force);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AsteroidBig" || other.tag == "AsteroidMedium" || other.tag == "AsteroidSmall" 
            || other.tag == "EnemyLaser")
        {
            timeVulnerability = Time.time + 3f;
            box.enabled = false; //выключаем collider, чтобы сделать корабль неуязвимым
            var newObj = Instantiate(this.gameObject, transform.position, transform.rotation); //создаем новый корабль
            newObj.transform.position = new Vector3(0f, 0f, 0f);
            newObj.transform.rotation = Quaternion.Euler(0, 0, 0);
            box = newObj.GetComponent<BoxCollider>();
            isBoxDisable = true;

            meshPlayer.GetComponent<Animation>().Play();//анимация неуязвимости
            changeLive = true;

            AudioHelper.CreateAudioObject(explosivePlayerAudioClip, "PlayerDestroy", transform.position);//добавление звука взрыва

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
     