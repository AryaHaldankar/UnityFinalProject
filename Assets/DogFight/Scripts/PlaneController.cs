using UnityEngine;

public class PlaneController : Vehicle // INHERITANCE
{
    public static PlaneController Instance;
    [SerializeField] private int bulletCount = 500;
    public float horizontalRotationSpeed;
    public float verticalRotationSpeed;
    public float tiltSpeed;
    private bool shooting = false;
    [SerializeField] private AudioClip crashClip;
    [SerializeField] private AudioClip bulletFired;
    [SerializeField] private AudioClip backgroundNoise;
    [SerializeField] private AudioClip emptyBullet;
    [SerializeField] private AudioClip playerOutSound;
    public float maxVertical;
    public float maxHorizontal;
    public float maxTilt;
    private float dynamicHorizontalAngle = 0f;
    private float dynamicVerticalAngle = 0f;
    private float dynamicTilt = 0f;
    private float initialZ;
    public bool tankEmpty;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        tankEmpty = false;
        initialZ = transform.position.z;
        SoundFXManager.instance.playSoundFX(backgroundNoise, transform, 1f);
        GameManager.Instance.DisplayBulletCount(bulletCount);
    }
    void Update()
    {
        HandleInput();

        PerformRotation();

        Move();
        
        if (Input.GetKey(KeyCode.Space) && shooting == false)
        {
            InvokeRepeating("shootBullet", 0f, 0.1f);
            shooting = true;
        }
        else if (Input.GetKey(KeyCode.Space) == false && shooting)
        {
            shooting = false;
            CancelInvoke("shootBullet");
        }
    }

    protected override void Move() // POLYMORPHISM
    {
        if (tankEmpty)
        {
            float distanceX = dynamicHorizontalAngle / maxHorizontal * movementSpeed * Time.deltaTime;
            float distanceY = dynamicVerticalAngle / maxVertical * movementSpeed * Time.deltaTime;

            transform.Translate(new Vector3(distanceX, -Mathf.Abs(distanceY), 0f));
            transform.Translate(new Vector3(0f, 0f, initialZ - transform.position.z));
        }
        else
        {
            float distanceX = dynamicHorizontalAngle / maxHorizontal * movementSpeed * Time.deltaTime;
            float distanceY = dynamicVerticalAngle / maxVertical * movementSpeed * Time.deltaTime;

            transform.Translate(new Vector3(distanceX, distanceY, 0f));
            transform.Translate(new Vector3(0f, 0f, initialZ - transform.position.z));
        }
    }

    void PerformRotation() // ABSTRACTION
    {
        dynamicHorizontalAngle = Mathf.Clamp(dynamicHorizontalAngle, -maxHorizontal, maxHorizontal);
        dynamicVerticalAngle = Mathf.Clamp(dynamicVerticalAngle, -maxVertical, maxVertical);
        dynamicTilt = Mathf.Clamp(dynamicTilt, -maxTilt, maxTilt);

        transform.localRotation = Quaternion.Euler(-dynamicVerticalAngle, dynamicHorizontalAngle, dynamicTilt);
    }

    void HandleInput() // ABSTRACTION
    {
        float verticalInput = -Input.GetAxis("Vertical");
        float horizontalInput = -Input.GetAxis("Horizontal");

        dynamicHorizontalAngle -= horizontalInput * horizontalRotationSpeed * Time.deltaTime;
        dynamicVerticalAngle -= verticalInput * verticalRotationSpeed * Time.deltaTime;
        dynamicTilt += horizontalInput * tiltSpeed * Time.deltaTime;
    }

    void shootBullet(){
        if (bulletCount >= 1)
        {
            BulletPooler.Instance.SpawnBullet(transform);
            SoundFXManager.instance.playSoundFXClip(bulletFired, transform, 1f);
            bulletCount -= 1;
            GameManager.Instance.DisplayBulletCount(bulletCount);
        }
        else
            SoundFXManager.instance.playSoundFXClip(emptyBullet, transform, 1f);
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Boundary"))
        {
            SoundFXManager.instance.playSoundFXClip(crashClip, transform, 1f);
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }
}