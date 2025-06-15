using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * bulletSpeed;

        // ROTASI PELURU BIAR NGARAH KE MUSUH
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

   private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Bullet Hit: " + other.gameObject.name);
        
        // Cek apakah AudioManager instance ada
        if (AudioManager.Instance != null)
        {
            Debug.Log("AudioManager found, playing hit sound");
            PlayHitSound();
        }
        else
        {
            Debug.LogError("AudioManager Instance is null!");
        }
        
        // Damage logic
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(bulletDamage);
        }
        
        Destroy(gameObject);
    }

    private void PlayHitSound()
    {
        if (AudioManager.Instance != null && AudioManager.Instance.hitSound != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSound);
            Debug.Log("Hit sound played");
        }
        else
        {
            Debug.LogWarning("Cannot play hit sound - AudioManager or hitSound is null");
        }
    }


}
