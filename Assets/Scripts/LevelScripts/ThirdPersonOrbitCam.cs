using UnityEngine;

// This class corresponds to the 3rd person camera features.
public class ThirdPersonOrbitCam : MonoBehaviour 
{
	public Transform player;                                           
	public Vector3 pivotOffset = new Vector3(0.0f, 1.0f,  0.0f);       
	public Vector3 camOffset   = new Vector3(0.4f, 0.5f, -2.0f);       
	public float smooth = 10f;                                         
	public float horizontalAimingSpeed = 6f;                           
	public float verticalAimingSpeed = 6f;                            
	public float maxVerticalAngle = 30f;                             
	public float minVerticalAngle = -60f;                             
	public string XAxis = "Analog X";                              
	public string YAxis = "Analog Y";                                  

	private float angleH = 0;                                        
	private float angleV = 0;                                         
	private Transform cam;                                           
	private Vector3 relCameraPos;                                      
	private float relCameraPosMag;                                  
	private Vector3 smoothPivotOffset;                                 
	private Vector3 smoothCamOffset;                                 
	private Vector3 targetPivotOffset;                               
	private Vector3 targetCamOffset;                                   
	private float defaultFOV;                                          
	private float targetFOV;                                        
	private float targetMaxVerticalAngle;                             
	private float deltaH = 0;                                           
	private Vector3 firstDirection;                                   
	private Vector3 directionToLock;                                  
	private float recoilAngle = 0f;                                    
	private Vector3 forwardHorizontalRef;                              
	private float leftRelHorizontalAngle, rightRelHorizontalAngle;     

	// Get the camera horizontal angle.
	public float GetH { get { return angleH; } }

	void Awake()
	{
		// Reference to the camera transform.
		cam = transform;

		// Set camera default position.
		cam.position = player.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
		cam.rotation = Quaternion.identity;

		// Get camera position relative to the player, used for collision test.
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;

		// Set up references and default values.
		smoothPivotOffset = pivotOffset;
		smoothCamOffset = camOffset;
		defaultFOV = cam.GetComponent<Camera>().fieldOfView;
		angleH = player.eulerAngles.y;

		ResetTargetOffsets ();
		ResetFOV ();
		ResetMaxVerticalAngle();
	}

	void Update()
	{
	
		angleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * horizontalAimingSpeed;
		angleV += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1) * verticalAimingSpeed;
		
		angleH += Mathf.Clamp(Input.GetAxis(XAxis), -1, 1) * 60 * horizontalAimingSpeed * Time.deltaTime;
		angleV += Mathf.Clamp(Input.GetAxis(YAxis), -1, 1) * 60 * verticalAimingSpeed * Time.deltaTime;

		
		angleV = Mathf.Clamp(angleV, minVerticalAngle, targetMaxVerticalAngle);

		
		angleV = Mathf.LerpAngle(angleV, angleV + recoilAngle, 10f*Time.deltaTime);

		
		if (firstDirection != Vector3.zero)
		{
			angleH -= deltaH;
			UpdateLockAngle();
			angleH += deltaH;
		}

		
		if(forwardHorizontalRef != default(Vector3))
		{
			ClampHorizontal();
		}

		
		Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
		Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0);
		cam.rotation = aimRotation;

		
		cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp (cam.GetComponent<Camera>().fieldOfView, targetFOV,  Time.deltaTime);

		
		Vector3 baseTempPosition = player.position + camYRotation * targetPivotOffset;
		Vector3 noCollisionOffset = targetCamOffset;
		for(float zOffset = targetCamOffset.z; zOffset <= 0; zOffset += 0.5f)
		{
			noCollisionOffset.z = zOffset;
			if (DoubleViewingPosCheck (baseTempPosition + aimRotation * noCollisionOffset, Mathf.Abs(zOffset)) || zOffset == 0) 
			{
				break;
			} 
		}

		
		smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
		smoothCamOffset = Vector3.Lerp(smoothCamOffset, noCollisionOffset, smooth * Time.deltaTime);

		cam.position =  player.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;

		
		if (recoilAngle > 0)
			recoilAngle -= 5 * Time.deltaTime;
		else if(recoilAngle < 0)
			recoilAngle += 5 * Time.deltaTime;
	}

	
	public void ToggleClampHorizontal(float LeftAngle = 0, float RightAngle = 0, Vector3 fwd = default(Vector3))
	{
		forwardHorizontalRef = fwd;
		leftRelHorizontalAngle = LeftAngle;
		rightRelHorizontalAngle = RightAngle;
	}

	
	private void ClampHorizontal()
	{
		
		Vector3 cam2dFwd = this.transform.forward;
		cam2dFwd.y = 0;
		float angleBetween = Vector3.Angle(cam2dFwd, forwardHorizontalRef);
		float sign = Mathf.Sign(Vector3.Cross(cam2dFwd, forwardHorizontalRef).y);
		angleBetween = angleBetween * sign;

		
		float acc = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * horizontalAimingSpeed;
		acc += Mathf.Clamp(Input.GetAxis("Analog X"), -1, 1) * 60 * horizontalAimingSpeed * Time.deltaTime;

		
		if (sign < 0 && angleBetween < leftRelHorizontalAngle)
		{
			if (acc > 0)
				angleH -= acc;
		}
		
		else if (angleBetween > rightRelHorizontalAngle)
		{
			if (acc < 0)
				angleH -= acc;
		}
	}

	
	public void BounceVertical(float degrees)
	{
		recoilAngle = degrees;
	}


	private void UpdateLockAngle()
	{
		directionToLock.y = 0f;
		float centerLockAngle = Vector3.Angle(firstDirection, directionToLock);
		Vector3 cross = Vector3.Cross(firstDirection, directionToLock);
		if (cross.y < 0) centerLockAngle = -centerLockAngle;
		deltaH = centerLockAngle;
	}

	
	public void LockOnDirection(Vector3 direction)
	{
		if (firstDirection == Vector3.zero)
		{
			firstDirection = direction;
			firstDirection.y = 0f;
		}
		directionToLock = Vector3.Lerp(directionToLock, direction, 0.15f * smooth * Time.deltaTime);
	}

	
	public void UnlockOnDirection()
	{
		deltaH = 0;
		firstDirection = directionToLock = Vector3.zero;
	}

	
	public void SetTargetOffsets(Vector3 newPivotOffset, Vector3 newCamOffset)
	{
		targetPivotOffset = newPivotOffset;
		targetCamOffset = newCamOffset;
	}

	
	public void ResetTargetOffsets()
	{
		targetPivotOffset = pivotOffset;
		targetCamOffset = camOffset;
	}

	
	public void ResetYCamOffset()
	{
		targetCamOffset.y = camOffset.y;
	}

	
	public void SetYCamOffset(float y)
	{
		targetCamOffset.y = y;
	}

	
	public void SetXCamOffset(float x)
	{
		targetCamOffset.x = x;
	}

	
	public void SetFOV(float customFOV)
	{
		this.targetFOV = customFOV;
	}

	
	public void ResetFOV()
	{
		this.targetFOV = defaultFOV;
	}

	
	public void SetMaxVerticalAngle(float angle)
	{
		this.targetMaxVerticalAngle = angle;
	}

	
	public void ResetMaxVerticalAngle()
	{
		this.targetMaxVerticalAngle = maxVerticalAngle;
	}

	
	bool DoubleViewingPosCheck(Vector3 checkPos, float offset)
	{
		float playerFocusHeight = player.GetComponent<CapsuleCollider> ().height *0.5f;
		return ViewingPosCheck (checkPos, playerFocusHeight) && ReverseViewingPosCheck (checkPos, playerFocusHeight, offset);
	}

	
	bool ViewingPosCheck (Vector3 checkPos, float deltaPlayerHeight)
	{
		RaycastHit hit;
		
		
		if(Physics.Raycast(checkPos, player.position+(Vector3.up* deltaPlayerHeight) - checkPos, out hit, relCameraPosMag))
		{
			
			if(hit.transform != player && !hit.transform.GetComponent<Collider>().isTrigger)
			{
			
				return false;
			}
		}
		
		return true;
	}

	
	bool ReverseViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight, float maxDistance)
	{
		RaycastHit hit;

		if(Physics.Raycast(player.position+(Vector3.up* deltaPlayerHeight), checkPos - player.position, out hit, maxDistance))
		{
			if(hit.transform != player && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
			{
				return false;
			}
		}
		return true;
	}


	public float GetCurrentPivotMagnitude(Vector3 finalPivotOffset)
	{
		return Mathf.Abs ((finalPivotOffset - smoothPivotOffset).magnitude);
	}
}
