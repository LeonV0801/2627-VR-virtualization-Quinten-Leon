using Godot;
using System;
using System.ComponentModel;

public partial class Fade : Node
{
	[Export(PropertyHint.Range, "0.0f, 1.0f, 0.01f")] private float fade
	{
		get
		{
			return fade;
		}
		set
		{
			fade = value;
			if (IsInsideTree())
			{
				UpdateFade();
			}
		}
	}
		

	public void UpdateFade()
	{
		if(fade == 0.0f)
		{
			
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateFade();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
