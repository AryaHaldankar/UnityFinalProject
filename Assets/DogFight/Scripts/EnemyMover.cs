using UnityEngine;
using UnityEngine.VFX;

public class EnemyMover : MonoBehaviour
{
    public float movementSpeed;
    // public GameObject newEnemy;
    // [SerializeField] private AudioClip crashClip;
    public float verticalAngle = 0f;
    public float horizontalAngle = 0f;
    private float verticalInput;
    private float horizontalInput;
    private float verticalBound = 200f;
    private float horizontalBound = 200f;
    private int frames = 0;
    private Vector3 initialPos;
    void Start(){
        initialPos = transform.position;
        verticalBound = transform.position.x + verticalBound;
        horizontalBound = transform.position.y + horizontalBound;
    }
    void Update()
    {
        if(frames >= 60){
            float flip = Random.Range(-1f, 1f);
            flip = flip == 0f ? 0f : (flip < 0f ? -1 : 1);
            if (flip < 0){
                horizontalInput = 0;
                verticalInput = Random.Range(-1f, 1f);
                verticalInput = verticalInput == 0f ? 0f : (verticalInput < 0f ? -1 : 1);
            }
            else{
                verticalInput = 0;
                horizontalInput = Random.Range(-1f, 1f); // A/D or Left/Right
                horizontalInput = horizontalInput == 0f ? 0f : (horizontalInput < 0f ? -1 : 1);
            }
            
            transform.localRotation = Quaternion.Euler(-verticalInput * verticalAngle, 0f, horizontalInput * horizontalAngle);
            frames = 0;
        }
        else{
            frames += 1;
        }
        //Proposed Update
        Vector3 newDims = new Vector3(-horizontalInput, verticalInput, 0f) * movementSpeed * Time.deltaTime + transform.position;
        Vector3 newDimsClamped = new Vector3(Mathf.Clamp(newDims.x, -horizontalBound, horizontalBound), Mathf.Clamp(newDims.y, -verticalBound, verticalBound), 0f);
        Vector3 validUpdate = newDimsClamped - transform.position;
        
        transform.Translate(validUpdate);
        transform.Translate(new Vector3(0f, 0f, initialPos.z - transform.position.z));
    }

    // void OnTriggerEnter(Collider other){
    //     if (other.CompareTag("Bullet")){
    //         SoundFXManager.instance.playSoundFXClip(crashClip, transform, 1f);
    //         GameObject newObject = Instantiate(newEnemy, new Vector3(0, 0, 450), Quaternion.identity);
    //         newObject.name = "Enemy";
    //         Destroy(gameObject);
    //     }
    // }
}
