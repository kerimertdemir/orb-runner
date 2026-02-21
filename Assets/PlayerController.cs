using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    
    private bool isGrounded = false;
    private Vector3 spawnPoint;


    [Header("Can Ayarları")]
    public int max_can = 3;
    public int guncelCan;

    [Header("UI Elemanları")]
    public TextMeshProUGUI winText;
    public TextMeshProUGUI canText;
    public TextMeshProUGUI loseText;

    [Header("Ses Kaynakları")]
    public AudioSource sesKaynak;
    public AudioClip bounceClip;

    [Header("Aktifleşecek Tuzaklar")]
    public GameObject[] trapsToActivate;

    [Header("Düşcek Tuzaklar")]
    public GameObject[] fallToGroundd;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (SceneManager.GetActiveScene().name == "Level5")
        {
            rb.gravityScale = -1f;
            jumpForce = -Mathf.Abs(jumpForce);
        }
        
        spawnPoint = transform.position;
        winText.gameObject.SetActive(false);
        guncelCan = max_can;
        UpdateCanText();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            sesKaynak.PlayOneShot(bounceClip);
        }

        if (transform.position.y < -10 || transform.position.y > 10)
        {
            HasarAl();
        }
    }

    void HasarAl()
    {
        guncelCan--;
        UpdateCanText();

        if (guncelCan <= 0)
        {
            loseText.gameObject.SetActive(true);
            SceneManager.LoadScene("Level1");
        }
        else
        {
            ReSpawn();
        }
    }

    void UpdateCanText()
    {
        canText.text = "Can: " + guncelCan.ToString();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Groundd"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            HasarAl();
        }
        else if (collision.gameObject.CompareTag("trapsToActivate"))
        {
            ActivateTraps();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("grounddToActivate"))
        {
            fallGroundd();
            Destroy(collision.gameObject);
        }
        
    }

    void ActivateTraps()
    {
        foreach(GameObject trap in trapsToActivate)
        {
            trap.SetActive(true);
            Rigidbody2D rb_trap = trap.GetComponent<Rigidbody2D>();
            if( rb_trap != null)
            {
                rb_trap.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    void fallGroundd()
    {
        foreach (GameObject groundd in fallToGroundd)
        {
            FallingGround fg = groundd.GetComponent<FallingGround>();
            if (fg != null)
            {
                fg.ActivateFall();
            }
        }
    }

    void ReSpawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = spawnPoint;
    }

    
}
