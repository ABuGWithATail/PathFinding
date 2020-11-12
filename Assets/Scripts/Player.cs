using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3;
    public float health = 1;
    private float moveHorizontal;
    private float moveVertical;
    public GameObject playerObject;
    public GameObject enemyObject;
    public float attackRange = 8;
    public float damage = 4;
    private StateMachine enemy;
    public GameObject attackAnimation;
    
    private Rigidbody2D playerRigidBody;


    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAttack();
        }
    }

    void FixedUpdate()
    {
        //this here is the movement,  because it's a rigidbody, I'm adding force rather then
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal,  moveVertical, 0.0f);
        playerRigidBody.AddForce(movement * speed);
       //I don't fully understand this, But it is making my sprite angle the direction I'm facing (though I had to reverse the horizontal for reasons?)
       //thankyou answers.unity.com 
       //oh wait! I get it, "angle" is the direction we're moving. then angle axis changes the rotation of "forward" to be the same angle as "angle." oohhhh , life is different now.
        var angle = Mathf.Atan2(-moveHorizontal, moveVertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void PlayerAttack()
    {               
            
            Debug.Log("attack MAde");
            if(Vector2.Distance(playerObject.transform.position, enemyObject.transform.position) < attackRange)
            {
                Debug.Log("enemy takes a hit");
                enemy.TakeDamage();
            }
    }
}
