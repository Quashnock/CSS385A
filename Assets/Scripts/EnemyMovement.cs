using UnityEngine;

public class EnemyMovement : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public Transform pointA;
    public Transform pointB;
    public int health = 3;

    private float speed = 2f;
    private Vector3 targetPosition;
    private SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;
    
    [SerializeField] private bool isDefeated = false; 

    void Start()
    {
        targetPosition = pointB.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!isDefeated)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDefeated) return;
        
        health -= damage;
        if (health <= 0)
        {
            DefeatEnemy();
        }
    }

    private void DefeatEnemy()
    {
        isDefeated = true;
        ScoreManager.Instance.AddPoint();
        UpdateEnemyVisibility();
    }

    // Method for loading defeated state without adding score points
    private void SetDefeatedState(bool defeated)
    {
        isDefeated = defeated;
        UpdateEnemyVisibility();
    }

    private void UpdateEnemyVisibility()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = !isDefeated;
        
        if (enemyCollider != null)
            enemyCollider.enabled = !isDefeated;
    }

    public void LoadData(GameData data)
    {
        data.enemiesDefeated.TryGetValue(id, out isDefeated);
        // Use SetDefeatedState instead of DefeatEnemy to avoid adding points during load
        SetDefeatedState(isDefeated);
    }
    
    public void SaveData(ref GameData data)
    {
        if (data.enemiesDefeated.ContainsKey(id))
        {
            data.enemiesDefeated.Remove(id);
        }
        data.enemiesDefeated.Add(id, isDefeated);
    }
}
