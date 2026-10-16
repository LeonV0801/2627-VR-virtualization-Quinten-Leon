using Godot;
using System;



public partial class xrCharacter_controller : XROrigin3D
{
	private string[] warnings;
	private CharacterBody3D Player;
	private XRCamera3D camera;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<CharacterBody3D>("Player");
		camera = GetNode<XRCamera3D>("XRCamera3D");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		var camera_transform = Transform * camera.Transform; //transform van de camera ophalen door de lokale positie van de camera te vermenigvuldigdigen met de positie van de origin
		Vector3 newPos = camera_transform.Origin * new Vector3(1, 0, 1); //nieuwe positie aanmaken door hoogte weg te halen
		newPos = Player.GlobalTransform * newPos; //stop positie in de wereldruimte

		Vector3 currPos = Player.GlobalPosition;
		Player.MoveAndCollide(newPos - currPos); //beweeg characterbody3d

		Vector3 moved = Player.GlobalPosition - currPos; // check total movement

		moved = Player.GlobalBasis.Inverse() * moved; // verander naar locale orientatie

		Position -= moved;


    }

}
