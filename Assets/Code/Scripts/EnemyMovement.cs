using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private bool hasInvokedDestroy = false; // Flag untuk mencegah double invoke

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                ReachedEnd();
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    private void ReachedEnd()
    {
        if (hasInvokedDestroy) return; // Prevent double execution

        hasInvokedDestroy = true;
        LevelManager.main.ReduceLives();
        EnemySpawner.onEnemyDestroy.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // Only invoke if enemy was destroyed by other means (not reaching end)
        // AND Health component hasn't already invoked it
        if (!hasInvokedDestroy && !GetComponent<Health>().isDestroyed)
        {
            hasInvokedDestroy = true;
            EnemySpawner.onEnemyDestroy.Invoke();
        }
    }
}
