using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Naninovel;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
	{
		private Vector2 rotation = new Vector2(0, 0);
		public float speed = 3;


		private Camera cameraComponent;

		private Ray ray;
		private RaycastHit hit;

	public bool acceptInput = true;
	public bool enableInputNextFrame = false;

	[SerializeField]
	LayerMask mask;

	[SerializeField]
	private DialogueHandler handler;

	[SerializeField]
	private InteractableObject hoveringObject;

	private InteractableObject oldObject;

	private void OnEnable()
	{
		cameraComponent = GetComponent<Camera>();

		Cursor.lockState = CursorLockMode.Locked;

		handler = FindObjectOfType<DialogueHandler>();

	}

		private void OnDisable()
		{
		}



	void Update()
	{

		if (acceptInput)
		{
			FollowMouseRotation();

			RayCastToObjects();


			if (Input.GetMouseButtonDown(0) && hoveringObject)
			{
				handler.RunDialogue(hoveringObject.Script);
				Cursor.lockState = CursorLockMode.None;
				acceptInput = false;
			}

		}


	


		}

    private void LateUpdate()
    {
		if (enableInputNextFrame) 
		{
			acceptInput = true;
			enableInputNextFrame = false;
		}
    }


    private void RayCastToObjects() 
	{
		ray = cameraComponent.ScreenPointToRay(Input.mousePosition);
		float distance = 99;
		if (Physics.Raycast(ray, out hit, distance))
		{
			var obj = hit.transform.gameObject.GetComponent<InteractableObject>();
			hoveringObject = obj;
			if (hoveringObject != oldObject) 
			{
			}

		}

		else 
		{
			if (hoveringObject != null) 
			{
			}
			hoveringObject = null;
		}
	}
	
	private void FollowMouseRotation()
		{
			rotation.y += Input.GetAxis("Mouse X") * speed;
			rotation.x += -Input.GetAxis("Mouse Y") * speed;
			rotation.x = ClampAngle(rotation.x, -90, 90);
			transform.eulerAngles = (Vector2)rotation;
		}

		public float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360F)
				angle += 360F;
			if (angle > 360F)
				angle -= 360F;
			return Mathf.Clamp(angle, min, max);
		}

		public float GetNormalizedCameraHeight()
		{
			return Mathf.InverseLerp(1.8f, 2500, transform.position.y);
		}

		public float GetCameraHeight()
		{
			return transform.position.y;
		}
		public void OnRotation(Quaternion rotation)
		{
			Vector2 rotationEuler = rotation.eulerAngles;
			if (rotationEuler.x > 180)
			{
				rotationEuler.x -= 360f;
			}

			if (rotationEuler.x < -180)
			{
				rotationEuler.x += 360f;
			}

			this.rotation = rotationEuler;
		}

		public Vector3 GetMousePositionInWorld(Vector3 optionalPositionOverride = default)
		{
			var pointerPosition = Input.mousePosition;
			if (optionalPositionOverride != default) pointerPosition = optionalPositionOverride;

			ray = cameraComponent.ScreenPointToRay(pointerPosition);
			float distance = 99;
			if (Physics.Raycast(ray, out hit, distance))
			{
				return hit.point;
			}
			else
			{
				// return end of mouse ray if nothing collides
				return ray.origin + ray.direction * (distance / 10);
			}
		}

		public void SetNormalizedCameraHeight(float height)
		{
			//TODO: Determine if we want to expose the height slider.
		}
	}