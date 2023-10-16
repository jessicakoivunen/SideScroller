using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;

public class PlayerController : MonoBehaviour
{
    //Physics (basically)
    private Rigidbody rb = null;
    public float jumpForce = 10f;
    public float gravityModifier;


    
    //Game over :(
    public GameObject GameOverScreen;
    SpawnManager spawnManager;

    //Score keeping
    [SerializeField] TMP_Text scoreText;
    int score = 0;

    //Boolean tracking if player is on groung
    public bool isOnGround = true;

    //Animation
    private Animator playerAnim;

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -9.81f,0);
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        spawnManager = GetComponent<SpawnManager>();

        playerAnim = GetComponent<Animator>();
        Time.timeScale = 1;
    }

    //Obstacles and scores
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            spawnManager.PlayerDead();
            Invoke("GameOver", 3);

        }
        else if (other.CompareTag("ScoreTrigger"))
        {
            Debug.Log("Score");
            score++;
            scoreText.text = "SCORE  " + score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        { 
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
