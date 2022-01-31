using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera cam;
    public EnemySpawner spiderSpawner;
    public EnemySpawner monsterSpawner;
    public EnemySpawner lastSpawner;
    public static int enemiesKilled = 0;
    public static int brojac = 3;
    public GameObject spiderWall;
    public GameObject monsterWall;
    public GameObject lastWall;
    public GameObject Key;
    public GameObject KeySecond;
    public GameObject KeyThird;
    public GameObject Potion;
    public Text winText;

    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;

    public static int KeyCount = 0;


    private const string interactableTag = "InteractiveObject";

    private MyDoorContoller raycastedObj;


    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "SpiderWallTrigger" && enemiesKilled < 3)
        {
            spiderSpawner.enabled = true;
            foreach(Transform child in collision.transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
                child.GetComponent<MeshCollider>().enabled = true;
            }    
            
        }
        
        if (collision.gameObject.tag == "MonsterWallTrigger" && enemiesKilled == 3)
        {
            Debug.Log("entered monster spawn part");
            monsterSpawner.enabled = true;
            foreach (Transform child in collision.transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
                child.GetComponent<MeshCollider>().enabled = true;
            }

        }
        if (collision.gameObject.tag == "LastWallTrigger" && enemiesKilled == 4)
        {
            lastSpawner.enabled = true;
            Debug.Log("last wall trigger");
            foreach (Transform child in collision.transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
                child.GetComponent<MeshCollider>().enabled = true;
            }
        }
        if (collision.gameObject.tag == "End Wall")
        { 
            Debug.Log("YOU WON!");
            foreach (Transform child in collision.transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
                child.GetComponent<MeshCollider>().enabled = true;
            }
            winText.enabled = true;
        }
    }

    void Update()
    {
        if (characterController.isGrounded == true && characterController.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().volume = Random.Range(0.8f, 1);
            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.1f);
            GetComponent<AudioSource>().Play();
        }
        if (characterController.isGrounded == true && characterController.velocity.magnitude == 0f && GetComponent<AudioSource>().isPlaying == true)
        {
            GetComponent<AudioSource>().Stop();
        }

        if (characterController.isGrounded)
        {
            if (characterController.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().volume = Random.Range(0.8f, 1);
                GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.1f);
                GetComponent<AudioSource>().Play();
            }
            if (characterController.velocity.magnitude == 0f && GetComponent<AudioSource>().isPlaying == true)
            {
                GetComponent<AudioSource>().Stop();
            }

            moveDirection = cam.transform.right * Input.GetAxis("Horizontal") + cam.transform.forward * Input.GetAxis("Vertical");
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
           
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.transform.gameObject.tag == interactableTag)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<MyDoorContoller>();
                    if (Input.GetKeyDown(openDoorKey))
                    {
                        raycastedObj.PlayAnimation();
                    }
                }

                if (hit.transform.gameObject.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage();
                    if(enemiesKilled == 3)
                    {
                        foreach (Transform child in spiderWall.transform)
                        {
                            child.GetComponent<MeshRenderer>().enabled = false;
                            child.GetComponent<MeshCollider>().enabled = false;
                        }

                        if (Key != null)
                        {
                            foreach (Transform child in Key.transform)
                            {
                                child.GetComponent<MeshRenderer>().enabled = true;
                                child.GetComponent<BoxCollider>().enabled = true;
                            }
                        }
                    }
                    if (enemiesKilled == 4)
                    {
                        foreach (Transform child in monsterWall.transform)
                        {
                            child.GetComponent<MeshRenderer>().enabled = false;
                            child.GetComponent<MeshCollider>().enabled = false;
                        }

                        if (KeySecond != null)
                        {
                            foreach (Transform child in KeySecond.transform)
                            {
                                child.GetComponent<MeshRenderer>().enabled = true;
                                child.GetComponent<BoxCollider>().enabled = true;
                            }
                        }
                    }
                    if (enemiesKilled == 5)
                    {
                        foreach (Transform child in lastWall.transform)
                        {
                            child.GetComponent<MeshRenderer>().enabled = false;
                            child.GetComponent<MeshCollider>().enabled = false;
                        }

                        if (KeyThird != null)
                        {
                            foreach (Transform child in KeyThird.transform)
                            {
                                child.GetComponent<MeshRenderer>().enabled = true;
                                child.GetComponent<BoxCollider>().enabled = true;
                            }
                        }
                    }
                }
            }
        }
    }
}