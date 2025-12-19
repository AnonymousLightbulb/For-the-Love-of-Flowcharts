using Godot;
using System;

public partial class Cam : Control
{
	[Export] Camera2D Viewer;
	[Export] public PackedScene NewSticky;
	[Export] public PackedScene NewConnection;
	[Export] public Theme Clear;
	// Called when the node enters the scene tree for the first time.
	[Export] public Control StickyManagement;
	[Export] public Sticky CurrentSticky;
	[Export] public LineEdit TargetX;
	[Export] public LineEdit TargetY;
	[Export] public LineEdit TargetWidth;
	[Export] public LineEdit TargetHeight;
	[Export] public LineEdit TargetText;
	[Export] public LineEdit TargetConnectionWidth;
	[Export] public Button RelativeToCam;
	[Export] public Button CopyClickedData;
	[Export] public Button HideUI;
	[Export] public Control HideableUI;
	[Export] public Button Mode;
	[Export] public Button ZoomInButton;
	[Export] public Button ZoomOutButton;
	[Export] public Button UpButton;
	[Export] public Button DownButton;
	[Export] public Button LeftButton;
	[Export] public Button RightButton;
	[Export] public Button ShowBlankConnecions;
	[Export] public Control ConnectionManagement;
	// [Export] public Control ConnectionList;
	// [Export] public Control StickyList;
	[Export] public Connection CurrentConnection;
	[Export] public Sticky A;
	[Export] public Sticky B;
	[Export] public LineEdit TargetTexture;
	[Export] public LineEdit FileName;
	[Export] public LineEdit FontSize;
	float ZoomIn = 1;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 CenterPos = GlobalPosition + (Size / 2);
		Viewer.Position = Size / 2;
		GetParent().MoveChild(this, GetParent().GetChildCount() - 1);
		Position += Input.GetVector("Move Left", "Move Right", "Move Up", "Move Down") * (float)delta * 500 / Viewer.Zoom;
		if (ZoomInButton.ButtonPressed == true)
		{
			ZoomIn -= (float)delta;
		}
		if (ZoomOutButton.ButtonPressed == true)
		{
			ZoomIn += (float)delta;
		}
		ZoomIn = Mathf.Clamp(ZoomIn - Input.GetAxis("Zoom Out", "Zoom In") * (float)delta, 0.001f, Mathf.Inf);
		if (Input.IsActionPressed("Zoom 0"))
		{
			ZoomIn = 1;
		}
		Viewer.Zoom = new(1/ZoomIn, 1/ZoomIn);
		Scale = new(ZoomIn, ZoomIn);
		HideableUI.Visible = !HideUI.ButtonPressed;

		if (Mode.ButtonPressed == false)
		{
			Mode.Text = "Sticky";
			StickyManagement.Visible = true;
			StickyManagement.ProcessMode = ProcessModeEnum.Inherit;
			ConnectionManagement.Visible = false;
			ConnectionManagement.ProcessMode = ProcessModeEnum.Disabled;
		}
		else
		{
			Mode.Text = "Connection";
			StickyManagement.Visible = false;
			StickyManagement.ProcessMode = ProcessModeEnum.Disabled;
			ConnectionManagement.Visible = true;
			ConnectionManagement.ProcessMode = ProcessModeEnum.Inherit;
		}
		Godot.Collections.Array<Node> asdf = (GetParent().GetChild(0) as SaveLoad).StickyList.GetChildren();
        for (int i = 0; i < asdf.Count; i++)
		{
            Node item = asdf[i];
			item.Name = i.ToString();
        }
		asdf = (GetParent().GetChild(0) as SaveLoad).ConnectionList.GetChildren();
        for (int i = 0; i < asdf.Count; i++)
		{
            Node item = asdf[i];
			item.Name = i.ToString();
        }
		// GetViewport().
	}

	public void RemoveSticky()
	{
		try
		{
			CurrentSticky.QueueFree();
		}
		catch
		{

		}
	}
	public void AddSticky()
	{
		CurrentSticky = NewSticky.Instantiate() as Sticky;
		// AddChild(CurrentSticky);
		CurrentSticky.Name = (GetParent().GetChild(0) as SaveLoad).StickyList.GetChildCount().ToString();
		(GetParent().GetChild(0) as SaveLoad).StickyList.AddChild(CurrentSticky);
		MoveSticky();
		WriteSticky();
		// if (TargetTexture.Text != "")
		// {
		// 	TextureSticky();
		// }
	}
	public void MoveSticky()
	{
		try
		{
			if (RelativeToCam.ButtonPressed == false)
			{
				CurrentSticky.Position = new(TargetX.Text.ToFloat(), TargetY.Text.ToFloat());
			}
			else
			{
				CurrentSticky.Position = Position + (Size / 2) + new Vector2(TargetX.Text.ToFloat(), TargetY.Text.ToFloat());
			}
			CurrentSticky.Size = new(TargetWidth.Text.ToFloat(), TargetHeight.Text.ToFloat());
		}
		catch
		{
		}

	}
	public void OffsetSticky()
	{
		CurrentSticky.Position += new Vector2(TargetX.Text.ToFloat(), TargetY.Text.ToFloat());
		CurrentSticky.Size += new Vector2(TargetWidth.Text.ToFloat(), TargetHeight.Text.ToFloat());
	}
	public void WriteSticky()
	{
		try
		{
			CurrentSticky.Text = TargetText.Text;
		}
		catch
		{

		}
	}
	public void TextureSticky(string Address)
	{
		// CurrentSticky.Icon = ImageTexture.CreateFromImage(Image.LoadFromFile("user://" + TargetTexture.Text));
		CurrentSticky.Icon = ImageTexture.CreateFromImage(Image.LoadFromFile(Address));
	}
	public void UntextureSticky()
	{
		CurrentSticky.Icon = null;
	}
	public void SetA()
	{
		try
		{
			A = CurrentSticky;
		}
		catch
		{

		}
	}
	public void SetB()
	{
		try
		{
			B = CurrentSticky;
		}
		catch
		{

		}
	}
	public void SetFontSizeSticky()
	{
		CurrentSticky.AddThemeFontSizeOverride("font_size", FontSize.Text.ToInt());
		// CurrentSticky.AddThemeFontSizeOverride("theme_override_font_sizes/font_size")
	}
	public void AddConnection()
	{
		try
		{
			CurrentConnection = NewConnection.Instantiate() as Connection;
			CurrentConnection.Name = (GetParent().GetChild(0) as SaveLoad).ConnectionList.GetChildCount().ToString();
			CurrentConnection.A = A;
			CurrentConnection.B = B;
			(GetParent().GetChild(0) as SaveLoad).ConnectionList.AddChild(CurrentConnection);
			WriteConnection();
			SetConnectionWidth();
			// if (TargetTexture.Text != "")
			// {
			// 	TextureConnection();
			// }
			// CurrentConnection.Midpoint = (A.Position + B.Position) / 2;
		}
		catch
		{

		}
	}
	public void WriteConnection()
	{
		try
		{
			CurrentConnection.Display.Size = new(0, 0);
			CurrentConnection.Display.Text = TargetText.Text;
		}
		catch
		{

		}
	}
	public void RemoveConnection()
	{
		try
		{
			CurrentConnection.QueueFree();
		}
		catch
		{

		}
	}
	public void TextureConnection(string Address)
	{
		// CurrentConnection.Texture = ImageTexture.CreateFromImage(Image.LoadFromFile("user://" + TargetTexture.Text));
		CurrentConnection.Texture = ImageTexture.CreateFromImage(Image.LoadFromFile(Address));

	}
	public void UntextureConnection()
	{
		CurrentConnection.Texture = null;
	}
	public void SetConnectionWidth()
	{
		CurrentConnection.Width = TargetConnectionWidth.Text.ToInt();
	}
	public void SetFontSizeConnection()
	{
		CurrentConnection.Display.AddThemeFontSizeOverride("font_size", FontSize.Text.ToInt());
		// CurrentSticky.AddThemeFontSizeOverride("theme_override_font_sizes/font_size")
	}
	// public void MoveMidpoint()
	// {
	// 	try
	// 	{
	// 		if (RelativeToCam.ButtonPressed == false)
	// 		{
	// 			CurrentConnection.Midpoint = new(TargetX.Text.ToFloat(), TargetY.Text.ToFloat());
	// 		}
	// 		else
	// 		{
	// 			CurrentConnection.Midpoint = Position + new Vector2(TargetX.Text.ToFloat(), TargetY.Text.ToFloat());
	// 		}
	// 	}
	// 	catch
	// 	{
	// 	}

	// }
	// public void OffsetMidpoint()
	// {
	// 	CurrentConnection.Midpoint += new Vector2(TargetX.Text.ToFloat(), TargetY.Text.ToFloat());
	// }
	// public void ResetMidpoint()
	// {
	// 	CurrentConnection.Midpoint = new((CurrentConnection.Points[0].X + CurrentConnection.Points[2].X) / 2,(CurrentConnection.Points[0].Y + CurrentConnection.Points[2].Y) / 2);
	// }
	// public void UpdateUI()
	// {
	// 	(GetParent().GetChild(0) as SaveLoad).ResetUI();
	// }
	// public void Save()
	// {
	// 	PackedScene SavedFile = new();
	// 	SavedFile.Pack(GetTree().CurrentScene);
	// 	ResourceSaver.Save(SavedFile, "user://" + FileName.Text + ".tscn");
	// }
	// public void Load()
	// {
	// 	GetTree().ChangeSceneToFile("user://" + FileName.Text + ".tscn");
	// }
	public void Save(string Address)
	{
		PackedScene SavedFile = new();
		SavedFile.Pack(GetTree().CurrentScene);
		// ResourceSaver.Save(SavedFile, "user://" + FileName.Text + ".tscn");
		ResourceSaver.Save(SavedFile, Address);
	}
	public void Load(string Address)
	{
		// GetTree().ChangeSceneToFile("user://" + FileName.Text + ".tscn");
		GetTree().ChangeSceneToFile(Address);
		(GetParent().GetChild(0) as SaveLoad).ResetUI();
	}
	public void ResetUI()
	{
		(GetParent().GetChild(0) as SaveLoad).ResetUI();
	}
	public void GoToLicense()
	{
		GetTree().ChangeSceneToFile("res://License.tscn");
	}
    public override void _Input(InputEvent @event)
    {
		if (@event is InputEventMouseMotion && Input.IsActionPressed("Mouse Move"))
		{
			Position -= (@event as InputEventMouseMotion).Relative / Viewer.Zoom;
		}
        base._Input(@event);
    }
}
