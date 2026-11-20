using Godot;
using System;

public partial class XR_CharacterController : XROrigin3D
{
	[Export(PropertyHint.Range, "0.01f, 1.0f, 0.01f")] float maxDistance = 0.2f;
	[Export(PropertyHint.Range, "0.01f, 1.0f, 0.01f")] float fadeDistance = 0.1f;
	CharacterBody3D characterBody;
	XRCamera3D camera;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(Engine.IsEditorHint()){
			return;
		}

		characterBody = GetParent().GetNode<CharacterBody3D>("CharacterBody3D");
		
		if(characterBody == null){
			throw new ArgumentException("characterBody3D is leeg");
		}

		camera = GetNode<XRCamera3D>("XRCamera3D");

		Transform3D cameraTransform = Transform * camera.Transform;

		Vector3 newPosition = cameraTransform.Origin * new Vector3(1.0f, 0.0f, 1.0f);
		
		newPosition = characterBody.GlobalTransform * newPosition;

		Vector3 originalPosition = characterBody.GlobalPosition;

		characterBody.MoveAndCollide(newPosition - originalPosition);

		Vector3 deltaMovement = characterBody.GlobalPosition - originalPosition;

		deltaMovement = characterBody.GlobalBasis.Inverse() * deltaMovement;

		this.Position -= deltaMovement;

		Vector3 forward = cameraTransform.Basis.Z * new Vector3(1.0f, 0.0f, 1.0f);

		cameraTransform.Origin = new Vector3();

        Transform3D rotationTransform = cameraTransform.LookingAt(forward, Vector3.Up, true);

		var transform = characterBody.Transform;
		transform.Basis = rotationTransform.Basis * transform.Basis;
		characterBody.Transform = transform;    

		this.Transform = rotationTransform.Inverse() * this.Transform;

		var distance = (GlobalPosition - newPosition).Length();

		var fade = Mathf.Clamp((distance - maxDistance) / fadeDistance, 0.0f, 1.0f);
		
	}
}
