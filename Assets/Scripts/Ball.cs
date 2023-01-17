using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
 
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 250f;

    [SerializeField] private AudioSource hitWallSoundEffect;
    [SerializeField] private AudioSource hitPaddleSoundEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BoxCollider2D wall = collision.gameObject.GetComponent<BoxCollider2D>();
        Paddle paddle = collision.gameObject.GetComponent<Paddle>();

        if (wall != null)
            hitWallSoundEffect.Play();
        if (paddle != null)
            hitPaddleSoundEffect.Play();
    }
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }
    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigidbody.velocity = Vector2.zero;

        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity.normalized * 7;
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.speed);
    }
}
