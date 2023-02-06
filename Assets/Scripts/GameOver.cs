using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject square;
    private SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public GameObject over;

    void Start()
    {
        spriteRenderer = square.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
        StartCoroutine(ChangeTransparency());
    }

    IEnumerator ChangeTransparency()
    {
        yield return new WaitForSeconds(5f);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 2;
            spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        yield return new WaitForSeconds(2f);

        ChangeObjectSprite(over, newSprite);

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 2;
            spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        yield return new WaitForSeconds(35f);

        ScenesManager.Instance.LoadMainMenu();
    }

    public void Update()
    {
        if (Input.anyKey)
        {
            ScenesManager.Instance.LoadMainMenu();
        }
    }

    public void ChangeObjectSprite(GameObject objectToChange, Sprite newSprite)
    {
        SpriteRenderer spriteRenderer = objectToChange.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;
    }

}