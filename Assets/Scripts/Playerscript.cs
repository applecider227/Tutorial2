using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Playerscript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public TextMeshProUGUI score;

public TextMeshProUGUI livesText;
    private int scoreValue;

    private int scoreValueTotal;

    private int lives;

    public GameObject winTextObject;
    public GameObject loseTextObject;

    public LayerMask allGround;

    public float checkRadius;

    private bool isOnGround;

    public Transform groundcheck;

    public float jumpForce;

    public AudioClip victoryMusic;

    public AudioSource musicSource;

    AudioSource audioSource;


    
Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        lives = 3;
        SetLivesTextText();
         SetScoreText();
         winTextObject.SetActive(false);
         loseTextObject.SetActive(false);
          anim = GetComponent<Animator>();
          audioSource = GetComponent<AudioSource>();
    }

        

    void SetLivesTextText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    void SetScoreText()
    {
       score.text = "Coins Collected: " + scoreValue.ToString();  

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State",1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State",1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State",0);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State",0);
        }
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -0.025f;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 0.025f;
        }
        transform.localScale = characterScale;
      

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State",2);
            
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State",0);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (Input.GetKey("escape"))
        {
          Application.Quit();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.collider.tag == "Coin")
      {
          scoreValue = scoreValue +1;
          scoreValueTotal = scoreValueTotal +1;
          SetScoreText();
          Destroy(collision.collider.gameObject);
          if(scoreValue == 4)
          {
                  SceneManager.LoadScene(1);
          }
          if(scoreValue == 8)
          {
               audioSource.mute = !audioSource.mute;
              musicSource.clip = victoryMusic;
              musicSource.Play();
              winTextObject.SetActive(true);
          }
      }
       if(collision.collider.tag == "Coin2")
      {
          scoreValue = scoreValue +1;
          scoreValueTotal = scoreValueTotal +1;
          SetScoreText();
          Destroy(collision.collider.gameObject);
          if(scoreValue == 4)
          {
                  SceneManager.LoadScene(1);
          }
      }
      if(collision.collider.tag == "endCoin")
      {
          Destroy(collision.collider.gameObject);
           audioSource.mute = !audioSource.mute;
              musicSource.clip = victoryMusic;
              musicSource.Play();
              winTextObject.SetActive(true);
              scoreValue = scoreValue +1;
          scoreValueTotal = scoreValueTotal +1;
              SetScoreText();
      }
      if(collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lives = lives -1;
            SetLivesTextText();
            if(lives <= 0)
            {
                loseTextObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
         if(collision.collider.tag == "Ground" && isOnGround)
        {
            if(Input.GetKey(KeyCode.W))
            {
               
                    rd2d.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
                
            }
        }
    }
}