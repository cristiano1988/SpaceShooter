using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector3 targetPosition;

    void Start()
    {
        SetNewTarget();
    }

    void Update()
    {
        // Kretanje prema ciljnoj točki
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Kad stigne, odaberi novu nasumičnu točku
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTarget();
        }
    }

    void SetNewTarget()
    {
        float randomX = Random.Range(-15f, 15f);
        float randomY = Random.Range(0f, 7f);
        targetPosition = new Vector3(randomX, randomY, 0);
    }
}