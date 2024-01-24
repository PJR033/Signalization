using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _speed;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float direction = Input.GetAxis(Horizontal);
        float distance = direction * _speed * Time.deltaTime;

        if (direction > 0)
        {
            _spriteRenderer.flipX = true;
            _animator.SetBool("IsRunning", true);
        }
        else if (direction < 0)
        {
            _spriteRenderer.flipX = false;
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }

        transform.Translate(distance * Vector2.right);
    }
}
