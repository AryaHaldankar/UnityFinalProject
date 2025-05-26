using UnityEngine;
using UnityEngine.VFX;

public class EnemyMover : Vehicle // INHERITANCE
{
    public float verticalAngle = 0f;
    public float horizontalAngle = 0f;
    private float verticalInput;
    private float horizontalInput;
    private float verticalBound = 200f;
    private float horizontalBound = 200f;
    private int frames = 0;
    private Vector3 initialPos;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private bool hitOnce;
    [SerializeField] private bool goingDown;
    void Start()
    {
        fire.gameObject.SetActive(false);
        hitOnce = false;
        goingDown = false;
        initialPos = transform.position;
        verticalBound = transform.position.x + verticalBound;
        horizontalBound = transform.position.y + horizontalBound;
    }
    void Update()
    {
        if (goingDown)
        {
            transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
        }
        else
        {
            if (frames >= 180)
            {
                HandleRotation();
                frames = 0;
            }
            else
            {
                frames += 1;
            }

            Move();
        }
    }

    protected override void Move() // POLYMORPHISM
    {
        Vector3 newDims = new Vector3(-horizontalInput, verticalInput, 0f) * movementSpeed * Time.deltaTime + transform.position;
        Vector3 newDimsClamped = new Vector3(Mathf.Clamp(newDims.x, -horizontalBound, horizontalBound), Mathf.Clamp(newDims.y, -verticalBound, verticalBound), 0f);
        Vector3 validUpdate = newDimsClamped - transform.position;

        transform.Translate(validUpdate);
        transform.Translate(new Vector3(0f, 0f, initialPos.z - transform.position.z));
    }

    void HandleRotation() // ABSTRACTION
    {
        float flip = Random.Range(-1f, 1f);
        flip = flip == 0f ? 0f : (flip < 0f ? -1 : 1);

        if (flip < 0)
        {
            horizontalInput = 0;
            verticalInput = Random.Range(-1f, 1f);
            verticalInput = verticalInput == 0f ? 0f : (verticalInput < 0f ? -1 : 1);
        }
        else
        {
            verticalInput = 0;
            horizontalInput = Random.Range(-1f, 1f); // A/D or Left/Right
            horizontalInput = horizontalInput == 0f ? 0f : (horizontalInput < 0f ? -1 : 1);
        }

        transform.localRotation = Quaternion.Euler(-verticalInput * verticalAngle, 0f, horizontalInput * horizontalAngle);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !hitOnce)
        {
            hitOnce = true;
            fire.gameObject.SetActive(true);
        }
        else if (other.CompareTag("Bullet") && hitOnce)
        {
            goingDown = true;
            transform.localRotation = Quaternion.Euler(-1 * verticalAngle, 0f, 0f);
        }
        else if (other.CompareTag("Boundary"))
        {
            ParticleSystem explodeObj = Instantiate(explosion, transform.position, Quaternion.identity);
            explodeObj.Play();
            Destroy(explodeObj.gameObject, explodeObj.main.duration + explodeObj.main.startLifetime.constantMax);

            SoundFXManager.Instance.PlaySoundFXClip("Explosion", transform, 1f);
            
            GameManager.Instance.PlaneDestroyed();
            Destroy(gameObject);
        }
    }
}
