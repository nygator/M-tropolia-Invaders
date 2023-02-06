using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ship : MonoBehaviour
{
    public SCR_ShipSpawner ship_spawner;
    public GameObject game_area;

    public float speed;
    public Sprite[] spritesArray1;
    public Sprite[] spritesArray2;

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex;
    private float timeSinceLastAnimationSwitch = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpriteIndex = Random.Range(0, spritesArray1.Length);
        spriteRenderer.sprite = spritesArray1[currentSpriteIndex];
    }

    void Update()
    {
        Move();
        Animate();
    }

    void Move()
    {
        /** Move this ship forward per frame, if it gets too far from the game area, destroy it **/

        transform.position += transform.up * (Time.deltaTime * speed);

        float distance = Vector3.Distance(transform.position, game_area.transform.position);
        if (distance > ship_spawner.death_circle_radius)
        {
            RemoveShip();
        }
    }

    void Animate()
    {
        /** Switch between the two arrays of sprites for animation with 1 second delay **/

        timeSinceLastAnimationSwitch += Time.deltaTime;
        if (timeSinceLastAnimationSwitch >= 1)
        {
            if (spriteRenderer.sprite == spritesArray1[currentSpriteIndex])
            {
                spriteRenderer.sprite = spritesArray2[currentSpriteIndex];
            }
            else
            {
                spriteRenderer.sprite = spritesArray1[currentSpriteIndex];
            }
            timeSinceLastAnimationSwitch = 0;
        }
    }

    void RemoveShip()
    {
        /** Update the total ship count and then destroy this individual ship. **/

        Destroy(gameObject);
        ship_spawner.ship_count -= 1;
    }
}
