using UnityEngine;
using System.Collections;

public class PlayerMoveController : MonoBehaviour
{

	private Animator animator;

    private PlayerInfo playerInfo;

	private float horizontal;
	private float vertical;
	private float rotationDegreePerSecond = 1000;
	private bool isAttacking = false;

	public Camera gamecam;
	public Vector2 camPosition;

    public FloatingJoystick movement;
    public Transform modelTransform;


	void Start()
	{
        animator = GetComponentInChildren<Animator>();
        playerInfo = GetComponent<PlayerInfo>();
		gamecam = Camera.main;
	}

	void FixedUpdate()
	{
		if (animator)
		{
			//walk
			horizontal = movement.Horizontal;
			vertical = movement.Vertical;

			Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
			float speedOut;

			if (stickDirection.sqrMagnitude > 1) stickDirection.Normalize();

			if (!isAttacking)
				speedOut = stickDirection.sqrMagnitude;
			else
				speedOut = 0;

			if (stickDirection != Vector3.zero && !isAttacking)
            {
                modelTransform.rotation = Quaternion.RotateTowards(
                    modelTransform.rotation, 
                    Quaternion.LookRotation(stickDirection, Vector3.up), 
                    rotationDegreePerSecond * Time.deltaTime
                    );
            }
				
			GetComponent<Rigidbody>().velocity = modelTransform.forward * speedOut * GameManager.playerInfo.moveSpeed + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);

			animator.SetFloat("Speed", speedOut);
		}
	}

	void LateUpdate()
	{
        // move camera
        if (gamecam)
        {
            gamecam.transform.position = transform.position + new Vector3(0, camPosition.x, -camPosition.y);
        }
	}
}
