using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Projectile laserPrefab;
    public GameObject audioManager;


    public AudioClip playerHit;
    public GameObject soundPlayer;
    private AudioSource audioSource;

    public float speed = 5.0f;
    public System.Action killed;
    public bool laserActive { get; private set; }

    private void Start()
    {
        audioSource = soundPlayer.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
        }

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // Clamp the position of the character so they do not go out of bounds
        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);
        transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Only one laser can be active at a given time so first check that
        // there is not already an active laser
        if (!laserActive)
        {
            laserActive = true;

            Projectile laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.destroyed += OnLaserDestroyed;
        }
    }

    private void OnLaserDestroyed(Projectile laser)
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") ||
            other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            if (killed != null)
            {
                
                audioSource.clip = this.playerHit;
                audioSource.Play();

                killed.Invoke();
            }
        }
    }

}
