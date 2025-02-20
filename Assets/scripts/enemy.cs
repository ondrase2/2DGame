using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] int maxHP;

    public int Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, maxHP); }

    }
    int damagevalue = 15;


    public int health;
    float horizontal = 1;
    float vertical = -1;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

        rigidbody2d = GetComponent<Rigidbody2D>();
        health = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void FixedUpdate()
    {
        // movement
        timer += Time.fixedDeltaTime;
        if (timer > 5) {
            horizontal *= -1;
            timer = 0;
        }
        Vector2 position = rigidbody2d.position;
        position.x += Time.deltaTime * horizontal;
        //position.y += Time.deltaTime * vertical;
        rigidbody2d.MovePosition(position);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        player1 player = other.GetComponent<player1>();
        //Debug.Log("robot trigger");
        if (player != null)
        {
            player.Health -= damagevalue;
            Debug.Log(player.Health);

        }
    }
}
