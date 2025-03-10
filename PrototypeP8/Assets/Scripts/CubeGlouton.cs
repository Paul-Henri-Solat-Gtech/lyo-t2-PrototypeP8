using UnityEngine;
using System.Collections;

public class CubeGlouton : MonoBehaviour
{
    public float vitesse = 5f;
    public float vitesseAugmentee = 10f;
    public float tailleAugmentee = 0.1f;
    public float jumpPower = 5f;
    public float invulnerabilityTime = 0.5f;
    public float additionalVelocity = 4f;
    public float additionalJumpPower = 1.5f;
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool isGrounded;
    private bool isSprinting;
    private bool isAttachedToWall;
    private bool canDetach;
    private bool isInvulnerable;
    private Vector3 attachDirection;

    public AudioClip sonMiam;

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
    }

    void Update()
    {
        if (isAttachedToWall)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDetach)
            {
                DetachFromWall();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
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
        float moveX = Input.GetAxis("Horizontal") * currentVitesse;
        float moveZ = Input.GetAxis("Vertical") * currentVitesse;

        Vector3 movement = new Vector3(moveX, 0, moveZ) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            ResetVelocity();
        }
        else if (collision.gameObject.CompareTag("Wall") && !isInvulnerable)
        {
            AttachToWall(collision.contacts[0].normal);
        }
    }

    private void AttachToWall(Vector3 collisionNormal)
    {
        isAttachedToWall = true;
        attachDirection = collisionNormal;
        rb.isKinematic = true;
    }

    private void DetachFromWall()
    {
        isAttachedToWall = false;
        rb.isKinematic = false;
        Vector3 detachForce = -attachDirection * additionalVelocity + Vector3.up * (jumpPower + additionalJumpPower);
        rb.AddForce(detachForce, ForceMode.Impulse);
        StartCoroutine(InvulnerabilityPeriod());
    }

    private void ResetVelocity()
    {
        //rb.linearVelocity = Vector3.zero;
    }

    private IEnumerator InvulnerabilityPeriod()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nourriture"))
        {
            transform.localScale += new Vector3(tailleAugmentee, tailleAugmentee, tailleAugmentee);
            vitesse += 1;
            vitesseAugmentee += 1;
            if (sonMiam != null)
            {
                audioSource.PlayOneShot(sonMiam);
            }

            other.GetComponent<Nourriture>().Respawn();
        }
    }
}
