using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Rigidbody2D rb;
    public bool isUpsideDown = false;
    public Vector2 initialPosition;
    public float distanceMoved = 0f;
    public float desiredDistance; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        
        float enemyWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        int numSquaresToMove = 4; 
        desiredDistance = enemyWidth * numSquaresToMove;
    }

    void Update()
    {
       
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

       
        distanceMoved = Mathf.Abs(transform.position.x - initialPosition.x);

        
        if (distanceMoved >= desiredDistance)
        {
            ChangeDirection();
        }
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
                
                KillEnemy();
            }
        }
       }
    }

    void KillEnemy()
    {
        Debug.Log("Enemy killed");
        Destroy(gameObject);
        
    }
    void KillPlayer(GameObject Player)
    {
        Debug.Log("Player killed");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DeathPlatform"))
        {
            Destroy(gameObject);
        }
    }

    void ChangeDirection()
    {
       
        moveSpeed *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        distanceMoved = 0f;
        initialPosition = transform.position;
    }
}