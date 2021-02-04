using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WoodController : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    [SerializeField] private int startHealth;
    [SerializeField] private int hitScore;
    [SerializeField] private GameObject apple;
    [SerializeField] private GameObject knife;
    private int health;

    public int StartHealth { get => startHealth; set => startHealth = value; }
    public int Health { get => health; set => health = value; }

    private void Awake()
    {
        health = startHealth;
        bool[] mask = new bool[12];
        float rnd = Random.value;
        float radius = GetComponent<CircleCollider2D>().radius;
        if (rnd < .25f)
            InstantiateOnMask(apple, mask, radius, false);
        for (int i = 0; i < Random.Range(1, 3); i++)
            InstantiateOnMask(knife, mask, radius, true);
    }

    GameObject InstantiateOnMask(GameObject obj, bool[] mask, float radius, bool reverse)
    {
        for (int i = 0; i < 12; i++)
        {
            int index = Random.Range(0, 12);
            if (!mask[index])
            {
                mask[index] = true;
                Vector3 objPos = transform.position + Quaternion.Euler(0, 0, index * 30f) * (Vector3.right * radius);
                GameObject result = 
                    Instantiate(obj, objPos, Quaternion.LookRotation(Vector3.forward, reverse ? transform.position - objPos : objPos - transform.position), transform);
                mask[index] = true;
                return result;
            }
        }
        return null;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    }

    public void Hit()
    {
        Camera.main.GetComponent<GameController>().HitWood();
        if (--health == 0) Die();
    }

    void Die()
    {
        GameController gc = Camera.main.GetComponent<GameController>();
        if (gc.isVibration) Vibration.VibrateNope();
        GetComponent<Collider2D>().enabled = false;
        var knifesRb = transform.GetComponentsInChildren<Rigidbody2D>();
        foreach (var rb in knifesRb)
        {
            if (rb != GetComponent<Rigidbody2D>())
            {
                Collider2D collider;
                if (rb.TryGetComponent<Collider2D>(out collider)) collider.enabled = false;
                rb.transform.parent = null;
                rb.gameObject.AddComponent<DestroyOutOfScreen>();
                rb.isKinematic = false;
                rb.gravityScale = 1;
                rb.AddForce((rb.transform.position - transform.position) * Random.Range(5f, 10f), ForceMode2D.Impulse);
                rb.AddTorque(Random.Range(5f, 10f), ForceMode2D.Impulse);
            }
        }
        var partsRb = transform.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in partsRb)
        {
            rb.transform.parent = null;
            rb.gameObject.AddComponent<DestroyOutOfScreen>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce((rb.transform.position - transform.position) * Random.Range(5f, 20f), ForceMode.Impulse);
            rb.AddTorque(Random.onUnitSphere * Random.Range(5f, 20f), ForceMode.Impulse);
        }
        gc.PassStage();
        Destroy(gameObject);
    }
}
