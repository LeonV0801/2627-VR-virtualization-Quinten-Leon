using Godot;
using System;

[Tool]
public partial class Fade : Node3D
{
	MeshInstance3D meshInstance3D;
	private float fade = 0.0f;

[Export(PropertyHint.Range, "0.0, 1.0, 0.01")]
public float FadeValue
{
    get => fade;
    set
    {
        fade = value;
        if (IsInsideTree())
        {
            UpdateFade();
        }
    }
}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		meshInstance3D = GetNode<MeshInstance3D>("MeshInstance3D");
		UpdateFade();
	}

	/*private void UpdateFade(){
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
	}*/
	private void UpdateFade()
{
    if (meshInstance3D == null)
        return;

    if (fade == 0.0f)
    {
        meshInstance3D.Visible = false;
    }
    else
    {
        meshInstance3D.Visible = true;
        var material = (meshInstance3D.GetSurfaceOverrideMaterial(0) ?? meshInstance3D.MaterialOverride) as ShaderMaterial;

        if (material != null)
        {
            material.SetShaderParameter("alpha", fade);
        }
    }
}
}
