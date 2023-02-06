using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    public float speed = 5f;
    public float cycleTime = 30f;
    public int score = 300;
    public System.Action<MysteryShip> killed;
    public Sprite[] sprites;
    public float animationSpeed = 1f;

    // Audio code
    public AudioClip ufoSound;
    public AudioClip ufoHitSound;
    private AudioSource audioSource;

    public Vector3 leftDestination { get; private set; }
    public Vector3 rightDestination { get; private set; }
    public int direction { get; private set; } = -1;
    public bool spawned { get; private set; }

    private int spriteIndex = 0;
    private float timeSinceLastFrame = 0f;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Just some audio stuff
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ufoSound;

        // Transform the viewport to world coordinates so we can set the mystery
        // ship's destination points
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // Offset the destination by a unit so the ship is fully out of sight
        Vector3 left = transform.position;
        left.x = leftEdge.x - 1.5f;
        leftDestination = left;

        Vector3 right = transform.position;
        right.x = rightEdge.x + 1.5f;
        rightDestination = right;

        transform.position = leftDestination;
        Despawn();
    }

    private void Update()
    {
        if (!spawned)
        {
            return;
        }

        timeSinceLastFrame += Time.deltaTime;
        if (timeSinceLastFrame >= animationSpeed)
        {
            timeSinceLastFrame = 0f;
            spriteIndex = (spriteIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[spriteIndex];
        }

        if (direction == 1)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x >= rightDestination.x)
        {
            Despawn();
        }
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= leftDestination.x)
        {
            Despawn();
        }
    }

    private void Spawn()
    {
        direction *= -1;

        if (direction == 1)
        {
            transform.position = leftDestination;
        }
        else
        {
            transform.position = rightDestination;
        }

        spawned = true;
        audioSource.Stop();
        audioSource.clip = ufoSound;
        audioSource.Play();
    }

    private void Despawn()
    {
        spawned = false;

        if (direction == 1)
        {
            transform.position = rightDestination;
        }
        else
        {
            transform.position = leftDestination;
        }
        audioSource.Stop();

        Invoke(nameof(Spawn), cycleTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            Despawn();
            audioSource.clip = ufoHitSound;
            audioSource.Play();
            if (killed != null)
            {
                killed.Invoke(this);
            }
        }
    }

}
