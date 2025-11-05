using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject player;
    public float playerSpeed = 5f;
    public GameObject bulletPrefab;

    [Header("Enemy Settings")]
    public float enemySpeed = 2f;

    [Header("UI")]
    public TextMeshProUGUI enemyCountText;

    private int totalEnemies;

    void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateEnemyCountText();
    }

    void Update()
    {
        HandlePlayerMovement();
        HandleShooting();
        HandleEnemyCount();
        MoveEnemies();
        MoveBullets();
    }

    // ---------------- PLAYER ----------------
    void HandlePlayerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(moveX, 0, 0);
        player.transform.position += move * playerSpeed * Time.deltaTime;

        float clampedX = Mathf.Clamp(player.transform.position.x, -15f, 15f);
        player.transform.position = new Vector3(clampedX, player.transform.position.y, player.transform.position.z);
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);

            SpriteRenderer bulletRenderer = bullet.GetComponent<SpriteRenderer>();
            if (bulletRenderer != null)
                bulletRenderer.sortingOrder = -1;

            // Dodaj podatke o metku (brzina, životni vijek)
            BulletData data = bullet.AddComponent<BulletData>();
            data.speed = 10f;
            Destroy(bullet, 5f);
        }
    }

    // ---------------- BULLET ----------------
    void MoveBullets()
    {
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            if (bullet == null) continue;
            BulletData data = bullet.GetComponent<BulletData>();
            if (data == null) continue;

            bullet.transform.Translate(Vector3.up * data.speed * Time.deltaTime);

            // Provjera kolizije s neprijateljem
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (enemy == null) continue;

                if (Vector2.Distance(bullet.transform.position, enemy.transform.position) < 0.5f)
                {
                    Destroy(enemy);
                    Destroy(bullet);
                    totalEnemies--;
                    UpdateEnemyCountText();

                    if (totalEnemies <= 0)
                    {
                        enemyCountText.text = "BRAVO!";
                        Invoke(nameof(ReloadScene), 3f);
                    }
                    break;
                }
            }
        }
    }

    // ---------------- ENEMY ----------------
    void MoveEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyData data = enemy.GetComponent<EnemyData>();
            if (data == null)
            {
                data = enemy.AddComponent<EnemyData>();
                data.targetPosition = GetNewTarget();
            }

            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, data.targetPosition, enemySpeed * Time.deltaTime);

            if (Vector3.Distance(enemy.transform.position, data.targetPosition) < 0.1f)
                data.targetPosition = GetNewTarget();
        }
    }

    Vector3 GetNewTarget()
    {
        float randomX = Random.Range(-15f, 15f);
        float randomY = Random.Range(0f, 7f);
        return new Vector3(randomX, randomY, 0);
    }

    void HandleEnemyCount()
    {
        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnemies != totalEnemies)
        {
            totalEnemies = currentEnemies;
            UpdateEnemyCountText();
        }
    }

    void UpdateEnemyCountText()
    {
        if (enemyCountText != null)
            enemyCountText.text = "Broj neprijatelja: " + totalEnemies;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ---------------- POMOĆNE KLASE ----------------
    class EnemyData : MonoBehaviour
    {
        public Vector3 targetPosition;
    }

    class BulletData : MonoBehaviour
    {
        public float speed;
    }
}
