using UnityEngine;
using System.Collections;

public class CubeGlouton : MonoBehaviour
{
    public float vitesse = 5f;
    public float vitesseAugmentee = 10f;
    public float tailleAugmentee = 1.0f;
    public float tailleSoustraite = 2.0f;
    public float jumpPower = 5f;
    public float invulnerabilityTime = 0.0f;
    public float additionalVelocity = 4f;
    public float additionalJumpPower = 1.5f;
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool isGrounded;
    private bool isSprinting;
    private bool isAttachedToWall;
    private bool canDetach;
    private bool isInvulnerable;
    private bool isPropelling;
    private Vector3 attachDirection;

    private int jumpLeft = 2;

    public AudioClip eatingSound;
    public AudioClip jumpSound;
    public AudioClip fallSound;
    public AudioClip attachSound;

    public bool IsSprinting
    {
        get { return isSprinting; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        canDetach = true;
        isInvulnerable = false;
        isPropelling = false;
    }

    void Update()
    {
        if (isAttachedToWall)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDetach)
            {
                DetachFromWall();
                Jump();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && jumpLeft > 0)
        {
            Jump();
            jumpLeft--;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
            jumpLeft--;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }

    private void FixedUpdate()
    {
        if (isAttachedToWall) return;

        float currentVitesse = isSprinting ? vitesseAugmentee : vitesse;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float cibleAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, cibleAngle, 0f) * Vector3.forward;

            Vector3 movement = moveDir.normalized * currentVitesse * Time.fixedDeltaTime;

            if (isPropelling)
            {
                rb.AddForce(movement * 50f, ForceMode.Acceleration);
            }
            else
            {
                rb.MovePosition(rb.position + movement);

                Quaternion nouvelleRotation = Quaternion.Euler(0f, cibleAngle, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, nouvelleRotation, Time.fixedDeltaTime * 10f);
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpLeft = 2;
            ResetVelocity();
            if (fallSound != null)
            {
                audioSource.PlayOneShot(fallSound);
            }
        }
        else if (collision.gameObject.CompareTag("Wall") && !isInvulnerable)
        {
            jumpLeft = 2;
            AttachToWall(collision.contacts[0].normal);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void AttachToWall(Vector3 collisionNormal)
    {
        isAttachedToWall = true;
        attachDirection = collisionNormal;
        rb.isKinematic = false;
        ResetVelocity();
        rb.isKinematic = true;
        if (attachSound != null)
        {
            audioSource.PlayOneShot(attachSound);
        }
    }

    private void DetachFromWall()
    {
        isAttachedToWall = false;
        rb.isKinematic = false;
        isPropelling = true;
        Vector3 detachForce = attachDirection * additionalVelocity + Vector3.up * (jumpPower + additionalJumpPower);
        rb.AddForce(detachForce, ForceMode.Impulse);
        StartCoroutine(InvulnerabilityPeriod());
        StartCoroutine(EndPropulsion());
    }

    private void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    private IEnumerator InvulnerabilityPeriod()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private IEnumerator EndPropulsion()
    {
        yield return new WaitForSeconds(0.2f);
        isPropelling = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FoodTall"))
        {
            transform.localScale += new Vector3(tailleAugmentee, tailleAugmentee, tailleAugmentee);

            if (eatingSound != null)
            {
                audioSource.PlayOneShot(eatingSound);
            }

        }
        if (other.CompareTag("FoodSmall") && transform.localScale.x > 0.1)
        {
            transform.localScale -= new Vector3(tailleSoustraite, tailleSoustraite, tailleSoustraite);
            if (eatingSound != null)
            {
                audioSource.PlayOneShot(eatingSound);
            }


        }
    }
}
