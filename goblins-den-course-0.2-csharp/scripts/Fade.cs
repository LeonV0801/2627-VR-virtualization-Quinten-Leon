using Godot;
using System;

public partial class Fade : Node3D
{
	MeshInstance3D meshInstance3D;
	[Export(PropertyHint.Range, "0.0f, 1.0f, 0.01f")] float fade{
		set{
			fade = value;
			if(IsInsideTree()){
				UpdateFade();
				
			}
		}
		get
		{
			return fade;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		meshInstance3D = GetNode<MeshInstance3D>("MeshInstance3D");
		UpdateFade();
	}

	private void UpdateFade(){
		if(fade == 0.0f){
			meshInstance3D.Visible = false;
		}
		else{
			meshInstance3D.Visible = true;
			ShaderMaterial material = meshInstance3D.MaterialOverride as ShaderMaterial;

			if(material != null){
				material.SetShaderParameter("alpha", fade);
			}
			
		}
	}
}
