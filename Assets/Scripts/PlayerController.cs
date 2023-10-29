using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;

public class PlayerController : MonoBehaviour
{
    //Movement
    private Rigidbody rb = null;
    public float jumpForce = 10f;
    public float gravityModifier;
    private Animator playerAnim;

    //Sounds
    public AudioClip jump, crash;
    private AudioSource playerAudio;
    
    //Game over
    public bool gameOver = false;
    public GameObject GameOverScreen;
    public ParticleSystem explosion, dirt;

    //Score keeping
    [SerializeField] TMP_Text scoreText;
    int score = 0;

    //Boolean tracking if player is on groung
    public bool isOnGround = true;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -9.81f,0);
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        playerAnim = GetComponent<Animator>();
        Time.timeScale = 1;

        playerAudio = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
        dirt.Play();
    }

    //Obstacles and scores
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            gameOver = true;
            //Death animation and explosion
            dirt.Stop();
            Debug.Log("Game over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            explosion.Play();
            playerAudio.PlayOneShot(crash, 1.0f);
            //Stop backround
            GameObject background;
            background = GameObject.FindGameObjectWithTag("Background");
            RepeatBackround repeatBackround;
            repeatBackround = background.GetComponent<RepeatBackround>();
            repeatBackround.speed = 0.0f;
            //Game over.
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
            dirt.Stop();
            playerAudio.PlayOneShot(jump, 1.0f);
        }
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
