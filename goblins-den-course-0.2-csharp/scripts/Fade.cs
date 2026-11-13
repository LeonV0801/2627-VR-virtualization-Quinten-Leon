using Godot;
using System;
using System.ComponentModel;

public partial class Fade : Node3D
{
	private float _fade = 0.0f;

	[Export(PropertyHint.Range, "0.0f, 1.0f, 0.01f")] private float fade
	{
		get
		{
			return _fade;
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
		if (_mesh == null) return;

        if (_fade <= 0.0f)
        {
            // Verberg de mesh volledig als er geen fade is om performance te besparen
            _mesh.Hide();
        }
        else
        {
            _mesh.Show();
            
            // Haal het materiaal op en pas de shader parameter 'alpha' aan
            var material = _mesh.GetActiveMaterial(0) as ShaderMaterial;
            if (material != null)
            {
                material.SetShaderParameter("alpha", _fade);
            }
        }
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_mesh = GetNodeOrNull<MeshInstance3D>("MeshInstance3D");
		UpdateFade();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
