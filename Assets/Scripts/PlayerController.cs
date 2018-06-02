using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float charge;
	public float speed;
	public float rotationSpeed;
	public float chargeRotationSpeed;
	public float chargeSpeed; //Between 0.1 and 0.001
	public float chargeDecay; //Between 0.05 and 0.001
	public float chargePenalty;
	public float maxCharge; //Between 1 and 4
	public float driftFriction;
	public Transform spriteTransfrom;

	private float builtUpTranslation = 1;
	private float translation;
	private float previousTranslation;

	public void rotatePlayer(Transform transformer, bool charge)
	{
		float rotation;
		if(charge == false)
		{
			rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		}
		else
		{
			rotation = Input.GetAxis("Horizontal") * chargeRotationSpeed;
		}
		rotation *= Time.deltaTime;
		transformer.Rotate(0, 0, -rotation);
	}

	void Start () {
		translation = 1 * speed;
		previousTranslation = translation;
	}
	
	// Update is called once per frame
	void Update () {

		if (!Input.GetKey(KeyCode.Space))
		{
			if (translation > 1 * speed)
			{
				translation -= chargeDecay * speed;
				builtUpTranslation -= chargeDecay;
				charge -= chargeDecay;
			}
			else if(builtUpTranslation < 1)
			{
				builtUpTranslation = 1;
				charge = 0;
			}
		}
		else if(translation > 0)
		{
			translation -= driftFriction * speed;
		}
		else if(translation < 0)
		{
			translation = 0;
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			builtUpTranslation = Mathf.Max(builtUpTranslation - chargePenalty, 0);
			charge -= chargePenalty;
		}
		if (Input.GetKey(KeyCode.Space) && builtUpTranslation < maxCharge + 1)
		{
			builtUpTranslation += chargeSpeed;
			charge += chargeSpeed;
		}
		else if(builtUpTranslation > maxCharge + 1)
		{
			builtUpTranslation = maxCharge + 1;
			charge = maxCharge;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			transform.rotation = spriteTransfrom.rotation;
			translation = builtUpTranslation * speed;
		}
		
		if(!Input.GetKey(KeyCode.Space))
		{
			rotatePlayer(transform, false);
		}
		previousTranslation = translation;
		translation *= Time.deltaTime;
		transform.Translate(0, translation, 0);
		translation = previousTranslation;
	}
	
}

