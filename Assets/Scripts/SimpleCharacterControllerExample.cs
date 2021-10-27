using UnityEngine;

public class SimpleCharacterControllerExample : MonoBehaviour
{
	RaycastHit hit;
	public float raycastDistance = 1f;
	public LayerMask raycastMask;

	public Transform playerAxis; 

	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float turnSpeed = 200f;
	[SerializeField] float orientationSpeed = 2f;

	void Update ()
	{
		float twist = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
		float moveDir = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		
        if (Input.GetKey(KeyCode.LeftShift)) { moveDir *= 2f; }

		transform.Translate(0, 0, moveDir);

		transform.RotateAround(transform.position, transform.up, twist);

		var groundRayPos = transform.position + transform.up * 1.3f;
		if(Physics.Raycast(new Ray(groundRayPos, -transform.up), out hit, raycastDistance, raycastMask))
		{
			var targetRight = Vector3.Cross(hit.normal.normalized, -transform.right);
			playerAxis.forward = Vector3.RotateTowards(playerAxis.forward, targetRight, orientationSpeed * Time.deltaTime, orientationSpeed * Time.deltaTime);
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
		}
	}
}