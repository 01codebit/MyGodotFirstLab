using Godot;
using System;

public partial class TestNode : Node
{
	[Export]
	public Label Label { get; set; }

	[Export]
	public string Message { get; set; } = "Hello, Godot!";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print(Message);
		Label.Text = Message;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
