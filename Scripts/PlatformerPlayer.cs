using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformerPlayer : MonoBehaviour
{
    [SerializeField]
    public float speed = 4.5f;
    [SerializeField]
    public float jumpforce = 12.0f;
    [SerializeField]
    public FollowCam cameraFollow;
    public Rigidbody2D body;
    public Animator anim;
    public BoxCollider2D box;
    public GameManager GameManager;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;

        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(max.x, min.y - 0.2f);

        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool grounded = false;
        if (hit != null)
        {
            grounded = true;
        }

        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }

        MovingPlatform platform = null;
        if (hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }
        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        anim.SetFloat("speed", Mathf.Abs(deltaX));
        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }

        if (transform.position.y < -1.27)
        {
            if (cameraFollow != null)
            {
                cameraFollow.AdjustSmoothness(false);
                cameraFollow.StopFollowingPlayer();
            }
        }
        else
        {
            if (cameraFollow != null)
            {
                cameraFollow.AdjustSmoothness(true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            GameManager.CollectCoin();
            Destroy(collision.gameObject);
            Debug.Log("Coins Collected");
        }
        else if (collision.gameObject.CompareTag("DeathPlatform"))
        {
            KillPlayer();
        }
    
        
        else if (collision.gameObject.CompareTag("GoldenPlatform"))
        {
            if (cameraFollow != null)
            {
                cameraFollow.StartRising(collision.transform);
            }
        }
    }

    void KillPlayer()
    {
        ReloadScene();
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

