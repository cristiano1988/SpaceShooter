using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // Metak ide prema gore
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // uništi neprijatelja
            Destroy(gameObject);           // uništi metak
        }
    }

    private void Start()
    {
        // Uništi metak nakon 5 sekundi da se ne nakupljaju objekti
        Destroy(gameObject, 5f);
    }
}