using Godot;
using System;


public partial class xrCharacter_controller : XROrigin3D
{
	[Export(PropertyHint.Range, "0.01f, 1.0f, 0.01f")] private float maxDistance = 0.2f;
	[Export(PropertyHint.Range, "0.01f, 1.0f, 0.01f")] private float fadeDistance = 0.1f;


	private string[] warnings;
	private CharacterBody3D player;
	private XRCamera3D camera;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<CharacterBody3D>("..");
		camera = GetNode<XRCamera3D>("XRCamera3D");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
		if (Engine.IsEditorHint())
		{
			return;
		}


        base._PhysicsProcess(delta);

		var camera_transform = Transform * camera.Transform; //transform van de camera ophalen door de lokale positie van de camera te vermenigvuldigdigen met de positie van de origin
		Vector3 newPos = camera_transform.Origin * new Vector3(1, 0, 1); //nieuwe positie aanmaken door hoogte weg te halen
		newPos = player.GlobalTransform * newPos; //stop positie in de wereldruimte

		Vector3 currPos = player.GlobalPosition;
		player.MoveAndCollide(newPos - currPos); //beweeg characterbody3d

		Vector3 moved = player.GlobalPosition - currPos; // check total movement

		moved = player.GlobalBasis.Inverse() * moved; // verander naar locale orientatie

		Position = Position - moved;

		//calculate forward vector
		Vector3 forward = camera_transform.Basis.Z * new Vector3(1.0f, 0.0f, 1.0f);

		//genereer een rotatie hieruit
		camera_transform.Origin = new Vector3();
		Transform3D rotation_transform = camera_transform.LookingAt(forward, Vector3.Up, true);

        //transformatie toevoegen aan de player
		// 1. Haal de transform op als lokale variabele
		Transform3D currentTransform = player.Transform;

		// 2. Pas de basis aan
		currentTransform.Basis = rotation_transform.Basis * currentTransform.Basis;

		// 3. Wijs de aangepaste transform weer toe aan de speler
		player.Transform = currentTransform;

		//inverse gebruiken op de origin
		this.Transform = rotation_transform.Inverse() * Transform;

		//berekenen van afstand tussen target en player
		float distance = (GlobalPosition - newPos).Length();

		float fade = Mathf.Clamp((distance - maxDistance) / fadeDistance, 0.0f, 1f);
    }

}
