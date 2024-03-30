using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Boss : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpChance = 0.001f; 
    public float directionChangeChance = 0.01f; 

    public float JumpForce = 12.0f;

    public Rigidbody2D rb;
    public Animator anim;
    public bool isMovingRight = true;
    public bool isGrounded = false;
    public int hopAttacksLeft = 3;
    public TextMeshProUGUI bossDefeatedText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bossDefeatedText.gameObject.SetActive(false);
    }

    void Update()
    {
        
        if (Random.value < jumpChance && isGrounded)
        {
            Jump();
        }
        if (Random.value < directionChangeChance)
        {
            ChangeDirection();
        }
        float horizontalMovement = isMovingRight ? moveSpeed : -moveSpeed;
        rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
       if (collision.gameObject.CompareTag("Player"))
       {
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        collision.GetContacts(contacts);

        foreach (ContactPoint2D contact in contacts)
        {
            Vector2 enemyPos = transform.position;
            Vector2 playerPos = collision.gameObject.transform.position;

           
            Vector2 direction = enemyPos - playerPos;

    
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
               
                KillPlayer(collision.gameObject);
                return;
            }
            else
            {
                
               HitByPlayer();
            }
        }
       }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
       rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void HitByPlayer()
    {
        anim.SetTrigger("HitTrigger");
        hopAttacksLeft--;

        if (hopAttacksLeft <= 0)
        {
            KillBoss();
        }
    }

    void KillPlayer(GameObject Player)
    {
        Debug.Log("Player killed");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void KillBoss()
    {
    bossDefeatedText.gameObject.SetActive(true);
    Destroy(gameObject);
    }
}