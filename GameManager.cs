using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;
    private int totalEnemies;

    void Start()
    {
        // Broj neprijatelja na sceni
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateEnemyCountText();
    }

    void Update()
    {
        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;


        // Ako je broj neprijatelja manji od početnog, ažuriraj UI
        if (currentEnemies != totalEnemies)
        {
            totalEnemies = currentEnemies;
            UpdateEnemyCountText();
        }

        // Ako su svi neprijatelji uništeni
        if (currentEnemies == 0)
        {
            enemyCountText.text = "BRAVO!";
            Invoke("ReloadScene", 3f);
        }
    }

    void UpdateEnemyCountText()
    {
        enemyCountText.text = "Broj neprijatelja: " + totalEnemies;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}