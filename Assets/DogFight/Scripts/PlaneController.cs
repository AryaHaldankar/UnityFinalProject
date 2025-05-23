using UnityEngine;

public class PlaneController : Vehicle // INHERITANCE
{
    [SerializeField] private int bulletCount = 500;
    private RaycastHit[] hits;
    public float horizontalRotationSpeed;
    public float verticalRotationSpeed;
    public float tiltSpeed;
    private bool shooting = false;
    [SerializeField] private AudioClip crashClip;
    [SerializeField] private AudioClip bulletFired;
    [SerializeField] private AudioClip backgroundNoise;
    [SerializeField] private AudioClip emptyBullet;
    [SerializeField] private AudioClip playerOutSound;
    public GameObject bullet;
    
    public GameObject enemy;
    public float maxVertical;
    public float maxHorizontal;
    public float maxTilt;
    private float dynamicHorizontalAngle = 0f;
    private float dynamicVerticalAngle = 0f;
    private float dynamicTilt = 0f;
    private float initialZ;
    
    void Start()
    {
        initialZ = transform.position.z;
        SoundFXManager.instance.playSoundFX(backgroundNoise, transform, 1f);
    }
    void Update()
    {
        HandleInput();

        PerformRotation();

        move();
        
        if (shoot && shooting == false)
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

    override void move() // POLYMORPHISM
    {
        float distanceX = dynamicHorizontalAngle / maxHorizontal * movementSpeed * Time.deltaTime;
        float distanceY = dynamicVerticalAngle / maxVertical * movementSpeed * Time.deltaTime;

        transform.Translate(new Vector3(distanceX, distanceY, 0f));
        transform.Translate(new Vector3(0f, 0f, initialZ - transform.position.z));
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
        if(bulletCount >= 1){
            hits = Physics.RaycastAll(transform.position, transform.forward, 500f);
            foreach(RaycastHit hit in hits){
                if(hit.collider.name == "Enemy")
                    Invoke("DestroyRoutine", hit.distance / 500);
            }
            Instantiate(bullet, transform.position, transform.rotation);
            SoundFXManager.instance.playSoundFXClip(bulletFired, transform, 1f);
            bulletCount -= 1;
        }
        else
            SoundFXManager.instance.playSoundFXClip(emptyBullet, transform, 1f);
    }

    void DestroyRoutine(){
        GameObject currentEnemy = GameObject.FindGameObjectWithTag("Enemy");
        SoundFXManager.instance.playSoundFXClip(crashClip, currentEnemy.transform, 1f);
        Destroy(currentEnemy);
        GameObject newEnemy = Instantiate(enemy, new Vector3(0, 0, 300), Quaternion.identity);
        newEnemy.name = "Enemy";
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Boundary")){
            SoundFXManager.instance.playSoundFXClip(playerOutSound, transform, 1f);
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.identity;
        }
    }
}