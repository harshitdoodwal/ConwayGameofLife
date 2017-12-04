using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float scrollSpeed;
	public float movementSpeed;

	void Update ()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") > 0f) {
			transform.position += Vector3.forward * Time.deltaTime * scrollSpeed;	
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0f) {
			transform.position -= Vector3.forward * Time.deltaTime * scrollSpeed;	
		}

		if (Input.GetAxis ("Vertical") > 0f) {
			transform.position += Vector3.up * Time.deltaTime * movementSpeed;	
		}
		if (Input.GetAxis ("Vertical") < 0f) {
			transform.position -= Vector3.up * Time.deltaTime * movementSpeed;	
		}
		if (Input.GetAxis ("Horizontal") > 0f) {
			transform.position += Vector3.right * Time.deltaTime * movementSpeed;	
		}
		if (Input.GetAxis ("Horizontal") < 0f) {
			transform.position -= Vector3.right * Time.deltaTime * movementSpeed;	
		}

	}
}
