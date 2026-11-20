using Godot;
using System;

public partial class Fade : Node3D
{
	[Export(PropertyHint.Range, "0.0f, 1.0f, 0.01f")] float fade = 0.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
