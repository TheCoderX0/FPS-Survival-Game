using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool alive = true;
    public HealthBar healthbar;

    [Header("Ammo & Reload")]
    public int maxAmmo = 6;
    private int currentAmmo;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    //public int ammoNumber;
    public bool isFiring;
    public Text ammoDsiplay;

    public Animator animator;

    [Header("Bullet Spawner")]
    public GameObject bulletSpawner;
    public GameObject bulletPrefab;

    [Header("Player Movement")]
    public CharacterController controller;
    Vector3 velocity;
    public float speed = 15f;
    public float gravity = -19.62f;
    private float moveDirectionY;

    [Header("Player Ground Check")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    private bool isGrounded;

    [Header("Dash")]
    //public float speed = 10f;
    public float dashLength = 0.15f;
    public float dashSpeed = 100f;
    public float dashResetTime = 1f;

    public CharacterController characterController;

    private Player player; 

    public GameObject gameOverScreen;

    private Vector3 dashMove;
    private float dashing = 0f;
    private float dashingTime = 0f;
    private bool canDash = true;
    private bool dashingNow = false;
    private bool dashReset = true;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        healthbar = FindObjectOfType<HealthBar>();

        currentAmmo = maxAmmo;
        controller = gameObject.GetComponent<CharacterController>();

        player = gameObject.GetComponent<Player>();
        gameObject.GetComponent<Player>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            //currentHealth = 0;
            alive = false;

            gameObject.GetComponent<Player>().enabled = false;

            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        healthbar.SetHealth(currentHealth);
        

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = (transform.right * x + transform.forward * z).normalized;
        //use .normalized to make that Player not go faster when pressing two movement keys.

        controller.Move(moveDirection * speed * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;

        moveDirection.y = moveDirectionY;

        controller.Move(velocity * Time.deltaTime);

        Dash();

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }


        //ammoDsiplay.text = currentAmmo.ToString();
        ammoDsiplay.text = currentAmmo + " / 6";
        if (Input.GetButtonDown("Fire1") && !isFiring && currentAmmo > 0)
        {
            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.transform.position = bulletSpawner.transform.position;
            bulletObject.transform.forward = bulletSpawner.transform.forward;

            isFiring = true;
            currentAmmo--;
            isFiring = false;
        }
    }

    public void TakeDamage (int damageGiven)
    {
        if (!alive)
        {
            return;
        }

        if (currentHealth <= 0)
        {
            //currentHealth = 0;
            alive = false;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        currentHealth -= damageGiven;
        healthbar.SetHealth(currentHealth);
    }

    public void Dash()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (move.magnitude > 1)
        {
            move = move.normalized;
        }

        if (Input.GetButtonDown("Dash") == true && dashing < dashLength && dashingTime < dashResetTime && dashReset == true && canDash == true)
        {
            dashMove = move;
            canDash = false;
            dashReset = false;
            dashingNow = true;
        }

        if (dashingNow == true && dashing < dashLength)
        {
            characterController.Move(dashMove * dashSpeed * Time.deltaTime);
            dashing += Time.deltaTime;
        }

        if (dashing >= dashLength)
        {
            dashingNow = false;
        }

        if (dashingNow == false)
        {
            characterController.Move(move * speed * Time.deltaTime);
        }

        if (dashReset == false)
        {
            dashingTime += Time.deltaTime;
        }

        if (characterController.isGrounded && canDash == false && dashing >= dashLength)
        {
            canDash = true;
            dashing = 0f;
        }

        if (dashingTime >= dashResetTime && dashReset == false)
        {
            dashReset = true;
            dashingTime = 0f;
        }
    }

    protected void ResetImpactY()
    {
        velocity.y = 0f;
        moveDirectionY = 0f;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

}
