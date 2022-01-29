using UnityEngine;
using System.Collections;


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
    public GameObject spiderWall;
    public GameObject monsterWall;
    public GameObject lastWall;


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
        if (collision.gameObject.tag == "MonsterWallTrigger" && enemiesKilled < 4)
        {
            monsterSpawner.enabled = true;
            foreach (Transform child in collision.transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
                child.GetComponent<MeshCollider>().enabled = true;
            }

        }
        if (collision.gameObject.tag == "LastWallTrigger" && enemiesKilled < 5)
        {
            lastSpawner.enabled = true;
            foreach (Transform child in collision.transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
                child.GetComponent<MeshCollider>().enabled = true;
            }
        }
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
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
                    }
                    if (enemiesKilled == 4)
                    {
                        foreach (Transform child in monsterWall.transform)
                        {
                            child.GetComponent<MeshRenderer>().enabled = false;
                            child.GetComponent<MeshCollider>().enabled = false;
                        }
                    }
                    if (enemiesKilled == 5)
                    {
                        foreach (Transform child in lastWall.transform)
                        {
                            child.GetComponent<MeshRenderer>().enabled = false;
                            child.GetComponent<MeshCollider>().enabled = false;
                        }
                    }
                }
            }
        }
    }
}