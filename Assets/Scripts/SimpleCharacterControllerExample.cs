using UnityEngine;
using System.Collections;

public class SimpleCharacterControllerExample : MonoBehaviour {

	RaycastHit hit;
	public float raycastDistance = 1f;
	public LayerMask raycastMask;
	public Transform groundCast;
	public Transform player;
	public Transform playerAxis;

	public float moveSpeed = 2f;
	public float turnSpeed = 2f;
	[SerializeField]float orientationSpeed = 1.2f;

	void Start () {
		if(player == null)
		{
			player = this.transform;
		}
	}


	void Update () {
		float twist = Input.GetAxis("Horizontal") * turnSpeed;
		float moveDir = Input.GetAxis("Vertical") * moveSpeed;

		transform.Translate(0, 0, moveDir);
		transform.RotateAround(transform.position, transform.up, twist);

		if(Physics.Raycast(new Ray(groundCast.position, -groundCast.up), out hit, raycastDistance, raycastMask))
		{
			playerAxis.forward = Vector3.Cross(hit.normal.normalized, transform.forward);
			transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
			//Debug.Log("We hit! " + hit.transform.name);
		}

		Debug.DrawRay(groundCast.position, -groundCast.up);


	}


}
