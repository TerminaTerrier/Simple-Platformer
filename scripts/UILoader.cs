using Godot;
using System;

public partial class UILoader : CanvasLayer
{
	[Export]
	Node2D sceneData;
	SceneData _sceneData;
	SignalBus signalBus;
	PackedScene mainMenu;
	PackedScene HUD;
	Control UIInstance;
	public override void _Ready()
	{
	  _sceneData = (SceneData)sceneData;

	  HUD = _sceneData.HUD;
	  
	  signalBus = GetNode<SignalBus>("/root/SignalBus");
	  signalBus.StartGame += () => {RemoveSceneInstance(UIInstance); LoadScene(HUD);};
	  signalBus.GameOver += () => LoadScene(_sceneData.GameOverScreen);
	  signalBus.Victory += () => LoadScene(_sceneData.WinScreen);
	 

	  mainMenu = _sceneData.MainMenu;
	  LoadScene(mainMenu);
	}

	private void LoadScene(PackedScene scene)
	{
	  UIInstance = (Control)scene.Instantiate();
	  AddChild(UIInstance);
	}

	private void RemoveSceneInstance(Control instance)
	{
		RemoveChild(instance);
	}
}
