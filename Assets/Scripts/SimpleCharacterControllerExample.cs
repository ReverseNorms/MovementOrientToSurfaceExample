using UnityEngine;

/// <summary>
/// Simple 3D character movement script that shows aligning to the angle of the surface you are over.
/// This script should be placed on the root of the character
/// </summary>
public class SimpleCharacterControllerExample : MonoBehaviour
{
	RaycastHit hit;
	public float raycastDistance = 1f;
	public LayerMask raycastMask;

	//Transform with pivot point placed at feet of Character, needs to be a child of object this script is on. And should be the character rig or character a child of it.
	public Transform playerAxis; 

	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float turnSpeed = 200f;
	[SerializeField] float orientationSpeed = 2f;

	void Update ()
	{
		//Get movement inputs, multiply by Time.deltaTime to adjust the changes by the time our frames are taking.
		float twist = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
		float moveDir = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		
		//If we are holding Shift, increase moveDir to sprint.
        if (Input.GetKey(KeyCode.LeftShift)) { moveDir *= 2f; }

		//Move the character in the direction of our input.
		transform.Translate(0, 0, moveDir);
		//Rotate the character around its vertical axis, based on our horizontal input direction.
		transform.RotateAround(transform.position, transform.up, twist);

		//Calculate the position to cast the ray down from, using the character root position and root upwards direction multiplied by the distance above our feet we want to start the ray from.
		var groundRayPos = transform.position + transform.up * 1.3f;

		//Cast a ray downwards from position calculated on previous line, and aiming in the opposite direction of the character upwards direction.
		if(Physics.Raycast(new Ray(groundRayPos, -transform.up), out hit, raycastDistance, raycastMask))
		{
			//We hit a collider below our character that is in one of the Layers checkmarked in 'raycastMask'.

			//Calculate the cross product of our root's left direction and the direction the surface below us is pointing
			//This will then return a forward angle to which forms the third square axis of those two angles
			var targetRight = Vector3.Cross(hit.normal.normalized, -transform.right);
			//Rotate slower towards our target rotation instead of instantly, to perform some smoothing
			playerAxis.forward = Vector3.RotateTowards(playerAxis.forward, targetRight, orientationSpeed * Time.deltaTime, orientationSpeed * Time.deltaTime);
			//Move our position height to the height we hit the ground, keeping us above the surface.
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
		}
	}
}