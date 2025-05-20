using UnityEngine;

public enum PatrolType
{
    Automatic,
    Horizontal,
    Vertical,
    Circle,
    Square
}

public class AnimalPatrol : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float patrolRadius = 2f;
    public float directionChangeInterval = 2f;
    public PatrolType patrolType = PatrolType.Automatic;

    private Vector2 startPos;
    private Vector2 moveDirection;
    private float directionTimer;
    private float circleAngle = 0f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Square movement
    private Vector2[] squareDirections = new Vector2[] { Vector2.right, Vector2.up, Vector2.left, Vector2.down };
    private int currentDir = 0;
    private float squareTimer = 0f;

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        ChooseNewDirection();
    }

    void Update()
    {
        directionTimer -= Time.deltaTime;

        switch (patrolType)
        {
            case PatrolType.Automatic:
                MoveAutomatic();
                break;

            case PatrolType.Horizontal:
                Vector2 horizontalDir = new Vector2(Mathf.Cos(Time.time * moveSpeed), 0);
                transform.Translate(horizontalDir * moveSpeed * Time.deltaTime);
                UpdateFlipX(horizontalDir);
                break;

            case PatrolType.Vertical:
                Vector2 verticalDir = new Vector2(0, Mathf.Sin(Time.time * moveSpeed));
                transform.Translate(verticalDir * moveSpeed * Time.deltaTime);
                UpdateFlipX(verticalDir);
                break;

            case PatrolType.Circle:
                circleAngle += moveSpeed * Time.deltaTime;
                float x = Mathf.Cos(circleAngle) * patrolRadius;
                float y = Mathf.Sin(circleAngle) * patrolRadius;
                Vector2 newPos = startPos + new Vector2(x, y);
                UpdateFlipX(newPos - (Vector2)transform.position);
                transform.position = newPos;
                break;

            case PatrolType.Square:
                MoveInSquare();
                break;
        }

        if (animator != null)
            animator.SetBool("isMoving", true);
    }

    void MoveAutomatic()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        UpdateFlipX(moveDirection);

        if (Vector2.Distance(transform.position, startPos) > patrolRadius || directionTimer <= 0)
        {
            ChooseNewDirection();
        }
    }

    void ChooseNewDirection()
    {
        moveDirection = Random.value > 0.5f ? Vector2.left : Vector2.right;
        directionTimer = directionChangeInterval;
    }

    void MoveInSquare()
    {
        Vector2 dir = squareDirections[currentDir];
        transform.Translate(dir * moveSpeed * Time.deltaTime);
        UpdateFlipX(dir);

        squareTimer += Time.deltaTime;
        if (squareTimer >= directionChangeInterval)
        {
            squareTimer = 0f;
            currentDir = (currentDir + 1) % squareDirections.Length;
        }
    }

    void UpdateFlipX(Vector2 direction)
    {
        if (spriteRenderer != null && direction.x != 0)
            spriteRenderer.flipX = direction.x > 0;
    }

    // ✅ Thêm xử lý khi va chạm (chỉ áp dụng với Automatic / Horizontal / Vertical / Square)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Trường hợp dạng di chuyển có thể đổi hướng
        switch (patrolType)
        {
            case PatrolType.Automatic:
                moveDirection *= -1;
                directionTimer = directionChangeInterval;
                break;

            case PatrolType.Horizontal:
                moveSpeed *= -1; // đảo hướng cosine
                break;

            case PatrolType.Vertical:
                moveSpeed *= -1; // đảo hướng sine
                break;

            case PatrolType.Square:
                squareTimer = 0f;
                currentDir = (currentDir + 2) % squareDirections.Length; // đảo hướng
                break;
        }
    }
}
