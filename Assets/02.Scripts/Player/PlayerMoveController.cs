using UnityEngine;
using System.Collections;

public class PlayerMoveController : MonoBehaviour
{

	private Animator animator;

    private PlayerInfo playerInfo;

	private float horizontal;
	private float vertical;
	//private float rotationDegreePerSecond = 1000;
	private bool isAttacking = false;

    public FloatingJoystick movement;
    public Transform modelTransform;
	public GameObject test;

	Rigidbody2D rb;


	void Start()
	{
        animator = GetComponentInChildren<Animator>();
        playerInfo = GetComponent<PlayerInfo>();
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		//walk
		horizontal = movement.Horizontal;
		vertical = movement.Vertical;

		Vector2 stickDirection = new Vector2(horizontal, vertical);
		float speedOut;

		if (stickDirection.sqrMagnitude > 1) stickDirection.Normalize();

		if (!isAttacking)
			speedOut = stickDirection.sqrMagnitude;
		else
			speedOut = 0;

		// if (stickDirection != Vector3.zero && !isAttacking)
		// {
		//     modelTransform.rotation = Quaternion.RotateTowards(
		//         modelTransform.rotation, 
		//         Quaternion.LookRotation(stickDirection, Vector3.up), 
		//         rotationDegreePerSecond * Time.deltaTime
		//         );
		// }
			
		//rb.velocity = stickDirection * speedOut * GameManager.playerInfo.moveSpeed;// + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
		rb.AddForce(stickDirection * speedOut * playerInfo.moveSpeed);

		if(stickDirection.x > 0)
			GetComponentInChildren<SpriteRenderer>().flipX = true;
		else if (stickDirection.x < 0)
			GetComponentInChildren<SpriteRenderer>().flipX = false;
		//animator.SetFloat("Speed", speedOut);

		if(horizontal != 0 && vertical != 0)
			playerInfo.headAngle = Mathf.Atan2(vertical,horizontal) * Mathf.Rad2Deg;
	}
}
