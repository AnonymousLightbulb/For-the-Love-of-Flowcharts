// using Ardot.SaveSystems;
using Godot;
using System;
using System.Linq;

public partial class Connection : Line2D//, ISaveable
{
	Cam TheCam;
	[Export] public Sticky A;
	[Export] public Sticky B;
	[Export] public Button Display;
	[Export] public Vector2 Midpoint;
	// [Export] public CollisionShape2D ClickZone;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Root.AddChild(this);
		// ClickZone.Shape = new recta
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if (A == null || B == null)
		// {
		// 	QueueFree();
		// }
		TheCam = GetViewport().GetCamera2D() as Cam;
		Owner = TheCam.GetParent();
		Display.Owner = TheCam.GetParent();
		try
		{
			Points = new Vector2[] {A.Position + (A.Size / 2), B.Position + (B.Size / 2)};
			// Points[0].MoveToward(B.Position, Mathf.Sqrt((A.Size.X * A.Size.X) + (A.Size.Y * A.Size.Y)) / 2);
			// Points[1].MoveToward(A.Position, Mathf.Sqrt((B.Size.X * B.Size.X) + (B.Size.Y * B.Size.Y)) / 2);
			Display.Position = ((Points[0] + Points[1]) / 2) - (Display.Size / 2);
			// Points[0] = A.Position;
			// Points[1] = B.Position;
			// Display.Size = new((A.Position + (A.Size/2)).DistanceTo(B.Position + (B.Size/2)),5);
			// Position = ((A.Position + (A.Size/2)) + (B.Position + (B.Size/2)))/2
		}
		catch
		{

		}
		if (Display.Text == "" && TheCam.ShowBlankConnecions.ButtonPressed == false)
		{
			Display.Visible = false;
		}
		else
		{
			Display.Visible = true;
		}
	}
	public void SetAsCurrentSticky()
	{
		TheCam.CurrentConnection = this;
		if (TheCam.CopyClickedData.ButtonPressed == true)
		{
		TheCam.TargetText.Text = Display.Text;
		TheCam.Mode.ButtonPressed = true;
		TheCam.TargetConnectionWidth.Text = Width.ToString();
		}
	}
	// public SaveData Save(params Variant[] parameters)
	// {
	// 	//Create and return a new SaveData with its key as GetLoadKey(), and its first value as GlobalPosition
	// 	return new SaveData(GetLoadKey(), GlobalPosition); 
	// }   
	
	// public void Load(SaveData data, params Variant[] parameters)
	// {
	// 	//Checking if data is null, in case something went wrong.
	// 	if(data == null)
	// 	  return;

	// 	//Setting GlobalPosition to the first value of data, as a Vector2
	// 	GlobalPosition = data[0].AsVector2();
	// }
	
	// public StringName GetLoadKey(params Variant[] parameters)
	// {
	// 	//Returning the LoadKey as 'Player'. It's important that this is unique, otherwise data can be confused
	// 	//If there were going to be more than one player, we may want this key to include some other identifier, like the node's path
	// 	return Name;
	// }
}
