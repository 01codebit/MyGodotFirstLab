using Godot;
using System;

public partial class FPSLabel : Label
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = "FPS: " + Math.Round(1.0 / delta).ToString();
	}
}
