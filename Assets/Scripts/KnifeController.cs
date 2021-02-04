using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [SerializeField] private float throwForce;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController gc = Camera.main.GetComponent<GameController>();
        if (gc.isGameOver) return;
        if (collision.gameObject.tag == "Knife" && transform.parent == null && collision.transform.parent != null)
            KnifeCollide(gc);
        else if (collision.gameObject.tag == "Wood" && collision.transform != transform.parent)
            WoodCollide(gc, collision.transform);

    }

    private void KnifeCollide(GameController gc)
    {
        rb.velocity = Vector3.zero;
        rb.gravityScale = 1;
        rb.AddTorque(10f, ForceMode2D.Impulse);
        rb.AddForce(Vector2.right * 2f + Vector2.down * 3f, ForceMode2D.Impulse);
        gameObject.AddComponent<DestroyOutOfScreen>();
        if (gc.isVibration) Vibration.VibratePeek();
        gc.GameOver();
    }

    private void WoodCollide(GameController gc, Transform wood)
    {
        transform.parent = wood;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        Vector3 pos = transform.position - wood.position;
        pos = pos.normalized * (pos.magnitude - wood.GetComponent<CircleCollider2D>().radius);
        transform.Translate(Vector2.up * pos.magnitude, Space.Self);
        wood.gameObject.GetComponent<WoodController>().Hit();
        if (gc.isVibration) Vibration.VibratePop();
    }

    public void Throw()
    {
        transform.parent = null;
        rb.AddRelativeForce(Vector2.up * throwForce, ForceMode2D.Impulse);
    }
}
