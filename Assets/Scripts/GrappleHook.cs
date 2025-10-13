using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GrappleHook : MonoBehaviour
{
    private LineRenderer line;
    private Rigidbody2D rb;
    private PlayerMovement pm;
    
    public LayerMask grapplableMask;
    public float maxDistance = 10f;
    public float grappleSpeed = 10f;
    public float grappleShootSpeed = 20f;
    public float recoilForce = 2f;
    public LayerMask enemyLayerMask;

    [HideInInspector] public bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    private Vector2 target;
    private Vector2 grappleStart;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            line.SetPosition(0, transform.position);

            if (Vector2.Distance(transform.position, target) < 0.5f)
            {   
                float travelDistance = Vector2.Distance(grappleStart, target);

                rb.linearVelocity = pm.InputDirection * travelDistance * recoilForce;

                Collider2D hitEnemy = Physics2D.OverlapCircle(transform.position, 2f, enemyLayerMask);
                if (hitEnemy != null)
                {
                    EnemyMovement enemy = hitEnemy.GetComponent<EnemyMovement>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(1);
                    }
                }


                isGrappling = false;
                retracting = false;
                line.enabled = false;
            }
        }
    }

    private void OnGrapple()
    {
        if (!isGrappling)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

            if (hit.collider != null)
            {

                isGrappling = true;
                grappleStart = transform.position;
                target = hit.point;

                line.enabled = true;
                line.positionCount = 2;
                
                StartCoroutine(Grapple());
            }
        }
    }

    IEnumerator Grapple()
    {
        float time = 10;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPos;

        for (float t = 0; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, target);
        retracting = true;
    }
}
