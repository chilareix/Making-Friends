using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEnemyScript : MonoBehaviour
{
	public PolygonCollider2D EnemyCollider;
	public CapsuleCollider2D PlayerCollider;
	public Rigidbody2D TouchEnemyRigidBody;
	public GameObject TouchEnemyGameObject;
	private bool ContactBroken = true;
	public int TouchEnemyHealth = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//Deletes enemy game object if a condition is met
		if (TouchEnemyRigidBody.position.y < -100) GameObject.Destroy(TouchEnemyGameObject); 
		if(TouchEnemyHealth <= 0) GameObject.Destroy(TouchEnemyGameObject);

		//Detects if player touches and adds damage upon touch
		if (EnemyCollider.IsTouching(PlayerCollider) && ContactBroken)
		{
			ContactBroken = false;
			PlayerInteractions.DamageTakenBuffer += 1;
		}
		//Detects if player breaks contact
		if (!ContactBroken && !EnemyCollider.IsTouching(PlayerCollider))
		{
			ContactBroken = true;
		}

    }
}
