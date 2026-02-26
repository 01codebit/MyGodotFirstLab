using Godot;
using Common;

public partial class Camera3d : Camera3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.PrintRich("[b][color=yellow][Camera3d._Ready][/color][/b]");

		//GD.PrintErr("PrintErr: this is an error message");
		//GD.PushWarning("PushWarning: this is a warning message");
		//GD.PushError("PushError: this is an error message");
//
		//CustomLogger.Log("Logger.Log: this is a log message");
		//CustomLogger.LogWarning("Logger.LogWarning: this is a warning message");
		//CustomLogger.LogError("Logger.LogError: this is an error message");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _Process(double delta)
	{
		MoveCamera(delta);
	}

	private void MoveCamera(double delta)
	{
		// Implement camera movement logic here
		Transform3D currentTransform = this.Transform;
		Vector3 forward = -currentTransform.Basis.Z;
		Vector3 right = currentTransform.Basis.X;

		if (Input.IsActionPressed("ui_up"))
		{
			currentTransform.Origin += forward * (float)(delta * 5); // Move up
		}
		if (Input.IsActionPressed("ui_down"))
		{
			currentTransform.Origin -= forward * (float)(delta * 5); // Move down
		}
		if (Input.IsActionPressed("ui_right"))
		{
			currentTransform.Origin += right * (float)(delta * 5); // Move right
		}
		if (Input.IsActionPressed("ui_left"))
		{
			currentTransform.Origin -= right * (float)(delta * 5); // Move left
		}

		this.Transform = currentTransform;
	}
}
