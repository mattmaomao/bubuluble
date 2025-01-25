using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public bool haveShield = false;

    [SerializeField] GameObject shield;
    GameObject cotactingObj;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] bool isFacingLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.Instance.gameRunning)
            autoMove();
        
        if (rb.velocity.x != 0)
            animator.SetBool("moving", true);
        else
            animator.SetBool("moving", false);

        if (rb.velocity.y != 0)
            animator.SetBool("jumping", true);
        else
            animator.SetBool("jumping", false);
        

        if (isFacingLeft)
            transform.localScale = new Vector2(-1, 1);
        else
            transform.localScale = new Vector2(1, 1);
    }

    void autoMove()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    public void getShield()
    {
        haveShield = true;
        shield.SetActive(true);
    }

    // trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bubble"))
        {
            other.GetComponent<Bubble>().trigger();
        }

        if (other.CompareTag("spike") || other.CompareTag("arrow"))
        {
            Debug.Log(other.gameObject);
            Debug.Log(other.gameObject == cotactingObj);
            if (other.gameObject == cotactingObj)
                return;

            if (other.CompareTag("arrow"))
                AudioManager.Instance.playSE(AudioManager.Instance.ARROW);
            else if (other.CompareTag("spike"))
                AudioManager.Instance.playSE(AudioManager.Instance.SPIKE);

            if (haveShield)
            {
                Debug.Log("Shield");
                cotactingObj = other.gameObject;
                haveShield = false;
                // hide shield
                shield.SetActive(false);
                return;
            }
            else
            {
                animator.SetBool("getHit", true);
                GameManager.Instance.gameOver();
                animator.SetBool("getHit", false);
            }
        }

        if (other.CompareTag("end pt"))
        {
            animator.SetBool("winning", true);
            GameManager.Instance.completeLvl();
            animator.SetBool("winning", false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("spike") && other.gameObject == cotactingObj)
        {
            cotactingObj = null;
        }
    }
}
