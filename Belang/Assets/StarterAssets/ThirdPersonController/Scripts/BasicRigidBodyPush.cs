using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class BasicRigidBodyPush : MonoBehaviour
{
	public LayerMask pushLayers;
	public bool canPush;
	Animator anim;
	[Range(0.5f, 5f)] public float strength = 1.1f;
	int isPushHash;

	void Start()
	{
		anim = GetComponent<Animator>();
		isPushHash = Animator.StringToHash("Push");
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (canPush && hit.gameObject.CompareTag("Cube")) 
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				PushRigidBodies(hit);
				anim.SetBool(isPushHash, true);	
			}
			else if (Input.GetKeyDown(KeyCode.E))
			{
				anim.SetBool(isPushHash, false);	
			}		
		}
	}

	private void PushRigidBodies(ControllerColliderHit hit)
	{
		// https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

		// make sure we hit a non kinematic rigidbody
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic) return;

		// make sure we only push desired layer(s)
		var bodyLayerMask = 3 << body.gameObject.layer;
		if ((bodyLayerMask & pushLayers.value) == 0) return;

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3f) return;

		// Calculate push direction from move direction, horizontal motion only
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

		// Apply the push and take strength into account
		body.AddForce(pushDir * strength, ForceMode.Impulse);
	}
}