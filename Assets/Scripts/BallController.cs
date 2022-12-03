using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    private Rigidbody rb;
    public float minDirection = 0.3f;
    private bool stopped = false;
    public GameObject sparksVFX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (stopped) return;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool hit = false;
        if (other.CompareTag("Wall"))
        {
            direction.z = -direction.z;
            hit = true;
        }

        if (other.CompareTag("Racket"))
        {
            Vector3 newDirection = (transform.position - other.transform.position).normalized;
            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(Mathf.Abs(newDirection.x), minDirection);
            newDirection.z = Mathf.Sign(newDirection.z) * Mathf.Max(Mathf.Abs(newDirection.z), minDirection);
            direction = newDirection;
            hit = true;
        }

        if (hit)
        {
            GameObject sparks = Instantiate(sparksVFX, transform.position, transform.rotation);
            Destroy(sparks, 0.5f);
        }
    }

    private void ChooseDirection()
    {
        float signX = Mathf.Sign(Random.Range(-1f, 1f));
        float signY = Mathf.Sign(Random.Range(-1f, 1f));

        direction = new Vector3(0.5f * signX, 0, 0.5f * signY);
    }

    public void Stop()
    {
        stopped = true;
    }

    public void Go()
    {
        ChooseDirection();
        stopped = false;
    }
}
