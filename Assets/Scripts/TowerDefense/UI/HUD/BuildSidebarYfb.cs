﻿using TowerDefense.Level;
using TowerDefense.Towers;
using UnityEngine;

namespace TowerDefense.UI.HUD
{
	/// <summary>
	/// UI component that displays towers that can be built on this level.
	/// </summary>
	public class BuildSidebarYfb : MonoBehaviour
	{
		/// <summary>
		/// The prefab spawned for each button
		/// </summary>
		public TowerSpawnButtonYfb towerSpawnButton;

		/// <summary>
		/// Initialize the tower spawn buttons
		/// </summary>
		protected virtual void Start()
		{
			if (!LevelManagerYfb.instanceExists)
			{
				Debug.LogError("[UI] No level manager for tower list");
			}
			foreach (TowerYfb tower in LevelManagerYfb.instance.towerLibrary)
			{
				TowerSpawnButtonYfb button = Instantiate(towerSpawnButton, transform);
				button.InitializeButton(tower);
				button.buttonTapped += OnButtonTapped;
				//button.draggedOff += OnButtonDraggedOff;
			}
		}

		/// <summary>
		/// Sets the GameUI to build mode with the <see cref="towerData"/>
		/// </summary>
		/// <param name="towerData"></param>
		void OnButtonTapped(TowerYfb towerData)
		{
			var gameUI = GameUIYfb.instance;
			if (gameUI.isBuilding)
			{
				gameUI.CancelGhostPlacement();
			}
			gameUI.SetToBuildMode(towerData);
		}

		///// <summary>
		///// Sets the GameUI to build mode with the <see cref="towerData"/> 
		///// </summary>
		///// <param name="towerData"></param>
		//void OnButtonDraggedOff(Tower towerData)
		//{
		//	if (!GameUI.instance.isBuilding)
		//	{
		//		GameUI.instance.SetToDragMode(towerData);
		//	}
		//}

		/// <summary>
		/// Unsubscribes from all the tower spawn buttons
		/// </summary>
		void OnDestroy()
		{
			TowerSpawnButtonYfb[] childButtons = GetComponentsInChildren<TowerSpawnButtonYfb>();

			foreach (TowerSpawnButtonYfb towerButton in childButtons)
			{
				towerButton.buttonTapped -= OnButtonTapped;
				//towerButton.draggedOff -= OnButtonDraggedOff;
			}
		}

		/// <summary>
		/// Called by start wave button in scene
		/// </summary>
		public void StartWaveButtonPressed()
		{
			if (LevelManagerYfb.instanceExists)
			{
				LevelManagerYfb.instance.BuildingCompleted();
			}
		}

	}
}