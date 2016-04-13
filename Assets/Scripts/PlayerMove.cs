using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public GameObject DLStart, DRStart, DLEnd, DREnd;
    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public bool facingRight;
    public bool dashed;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        speed = 6.0f;
        jumpForce = 3000.0f;
        facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
        RayCast();
        Jump();
        Dash();
	}

    void FixedUpdate()
    {
        MoveHoriz();
    }

    void RayCast()
    {
        if (Physics2D.Linecast(DLStart.transform.position, DLEnd.transform.position) || Physics2D.Linecast(DRStart.transform.position, DREnd.transform.position))
        {
            dashed = false;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void MoveHoriz()
    {
        if (Input.GetAxis("Horizontal") <= -0.3f)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetAxis("Horizontal") >= 0.3f)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    void Dash()
    {
        if (!dashed && !isGrounded && Input.GetButtonDown("Jump") && Input.GetAxis("Horizontal") >= 0.3f)
        {
            dashed = true;
            StartCoroutine(DashRight());
        }
        if (!dashed && !isGrounded && Input.GetButtonDown("Jump") && Input.GetAxis("Horizontal") <= -0.3f)
        {
            dashed = true;
            StartCoroutine(DashLeft());
        }
    }

    IEnumerator DashRight()
    {
        rb.velocity = new Vector3(0, 0, 0);
        Vector2 grav = Physics2D.gravity;
        grav.y = 0;
        Physics2D.gravity = grav;
        Vector3 dashPos = transform.position;
        dashPos.x += 4.0f;
        while (transform.position.x <= dashPos.x)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * 2.5f);
            yield return new WaitForSeconds(0);
        }
        dashPos.y = transform.position.y;
        transform.position = dashPos;
        grav.y = -9.81f;
        Physics2D.gravity = grav;
    }

    IEnumerator DashLeft()
    {
        rb.velocity = new Vector3(0, 0, 0);
        Vector2 grav = Physics2D.gravity;
        grav.y = 0;
        Physics2D.gravity = grav;
        Vector3 dashPos = transform.position;
        dashPos.x -= 4.0f;
        while (transform.position.x >= dashPos.x)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 2.5f);
            yield return new WaitForSeconds(0);
        }
        dashPos.y = transform.position.y;
        transform.position = dashPos;
        grav.y = -9.81f;
        Physics2D.gravity = grav;
    }
}
