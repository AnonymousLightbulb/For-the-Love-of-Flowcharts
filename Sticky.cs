// using Ardot.SaveSystems;
using Godot;
using System;

public partial class Sticky : Button//, ISaveable
{
	Cam TheCam;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		TheCam = GetViewport().GetCamera2D() as Cam;
		Owner = TheCam.GetParent();
		if (Icon != null)
		{
			Theme = TheCam.Clear;
		}
		else
		{
			Theme = null;
		}
	}

	public void SetAsCurrentSticky()
	{
		TheCam.CurrentSticky = this;
		if (TheCam.CopyClickedData.ButtonPressed == true)
		{
			if (TheCam.RelativeToCam.ButtonPressed == false)
			{
				TheCam.TargetX.Text = Position.X.ToString();
				TheCam.TargetY.Text = Position.Y.ToString();
			}
			else
			{
				TheCam.TargetX.Text = (Position.X - TheCam.Position.X).ToString();
				TheCam.TargetY.Text = (Position.Y - TheCam.Position.Y).ToString();
			}
			TheCam.TargetWidth.Text = Size.X.ToString();
			TheCam.TargetHeight.Text = Size.Y.ToString();
			TheCam.TargetText.Text = Text;
			TheCam.Mode.ButtonPressed = false;
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
