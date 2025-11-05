using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;

    void Update()
    {
        // Kretanje igrača lijevo i desno (A i D)
        float moveX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(moveX, 0, 0) * moveSpeed * Time.deltaTime;

        // Ograniči kretanje unutar granica ekrana
        float clampedX = Mathf.Clamp(transform.position.x, -15f, 15f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Ispucavanje metka tipkom razmaknice
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instancira metak točno na poziciji igrača
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Postavi da se iscrtava ispod igrača (Sorting Layer)
            SpriteRenderer bulletRenderer = bullet.GetComponent<SpriteRenderer>();
            if (bulletRenderer != null)
            {
                bulletRenderer.sortingOrder = -1;
            }
        }
    }
}