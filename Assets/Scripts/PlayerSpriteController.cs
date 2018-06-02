using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour {

	public Transform playerTransform;
	public PlayerController other;

	// Use this for initialization
	void Start () {
		transform.position = playerTransform.position;
		transform.rotation = playerTransform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = playerTransform.position;
		if(Input.GetKey(KeyCode.Space)) {
			other.rotatePlayer(transform, true);
		}
	}

	private void LateUpdate()
	{
		if(!Input.GetKey(KeyCode.Space))
		{
			transform.rotation = playerTransform.rotation;
		}
	}
}
