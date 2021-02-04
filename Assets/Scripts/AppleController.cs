using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer frontSpriteRenderer;
    [SerializeField] private Sprite sliceSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Camera.main.GetComponent<GameController>().isGameOver && collision.gameObject.tag == "Knife")
        {
            GetHit();
        }
    }

    private void GetHit()
    {
        Camera.main.GetComponent<GameController>().HitApple();
        frontSpriteRenderer.sprite = sliceSprite;
        var slices = GetComponentsInChildren<Rigidbody2D>();
        int i = 0;
        foreach (var slice in slices)
        {
            slice.transform.parent = null;
            slice.isKinematic = false;
            slice.AddForce(Vector2.up + (i++ == 0 ? Vector2.left : Vector2.right), ForceMode2D.Impulse);
            slice.AddTorque(2f, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }

}
