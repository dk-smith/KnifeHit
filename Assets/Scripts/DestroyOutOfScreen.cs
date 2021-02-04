using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfScreen : MonoBehaviour
{

    private Renderer renderer;
    private Bounds bounds;

    private void Start()
    {
        renderer = GetComponent<Renderer>();

    }

    private void FixedUpdate()
    {
        if (CheckInvisible()) Destroy(gameObject);
    }

    bool CheckInvisible()
    {
        bounds = renderer.bounds;
        Vector2 max = Camera.main.WorldToScreenPoint(bounds.max);
        Vector2 min = Camera.main.WorldToScreenPoint(bounds.min);
        return (max.x < 0 || min.x > Screen.width
            || max.y < 0 || min.y > Screen.height);
    }
}
