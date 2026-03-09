using Godot;

public partial class Box3 : RigidBody3D
{
	[Export]
	private float impulseY = 10f;

	[Export]
	private CollisionObject3D collisionObject;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_accept"))
		{
			this.ApplyCentralImpulse(new Vector3(0, impulseY, 0)); // Apply an upward impulse to the box
		}
	}
}
