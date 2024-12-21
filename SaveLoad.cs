// using Ardot.SaveSystems;
using Godot;
using System;

public partial class SaveLoad : Control
{
	[Export] public Cam CurrentUI;
	[Export] public Control ConnectionList;
	[Export] public Control StickyList;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ConnectionList ??= GetChild(0) as Control;
		StickyList ??= GetChild(1) as Control;
		if (CurrentUI == null)
		{
			PackedScene asdf = ResourceLoader.Load("res://Cam.tscn") as PackedScene;
			CurrentUI = asdf.Instantiate() as Cam;
			AddChild(CurrentUI);
			// foreach (var item in GetChildren())
			// {
			// 	if (item is Sticky)
			// 	{
			// 		(item as Sticky).TheCam = CurrentUI;
			// 	}
			// 	else if (item is Connection)
			// 	{
			// 		(item as Connection).TheCam = CurrentUI;
			// 	}
			// }
		}
	}
	// public void SaveScene()
	// {
	// 	SaveAccess saveAccess = SaveAccess.Open("user://" + FileName.Text + ".txt");

	// 	saveAccess.SaveTree(this);
	// 	saveAccess.Commit();
	// }
	// public void LoadScene()
	// {
	// 	SaveAccess saveAccess = SaveAccess.Open("user://" + FileName.Text + ".txt");
	// 	saveAccess.LoadTree(this);
	// }
	public void ResetUI()
	{
		CurrentUI.QueueFree();
		PackedScene asdf = ResourceLoader.Load("res://Cam.tscn") as PackedScene;
		CurrentUI = asdf.Instantiate() as Cam;
		AddChild(CurrentUI);
	}
}
