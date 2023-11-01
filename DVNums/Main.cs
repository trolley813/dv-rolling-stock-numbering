using System;
using System.Reflection;
using System.Runtime;
using dnlib;
using HarmonyLib;
using UnityModManagerNet;

namespace DVNums;

public static class Main
{
	public static Settings Settings = null!;

	// Unity Mod Manage Wiki: https://wiki.nexusmods.com/index.php/Category:Unity_Mod_Manager
	private static bool Load(UnityModManager.ModEntry modEntry)
	{
		Harmony? harmony = null;

		try
		{
			harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			// Other plugin startup logic
			Settings = UnityModManager.ModSettings.Load<Settings>(modEntry);
			modEntry.OnGUI = OnGUI;
			modEntry.OnSaveGUI = OnSaveGUI;
		}
		catch (Exception ex)
		{
			modEntry.Logger.LogException($"Failed to load {modEntry.Info.DisplayName}:", ex);
			harmony?.UnpatchAll(modEntry.Info.Id);
			return false;
		}

		return true;
	}

	static void OnGUI(UnityModManager.ModEntry modEntry)
	{
		Settings.Draw(modEntry);
	}

	static void OnSaveGUI(UnityModManager.ModEntry modEntry)
	{
		Settings.Save(modEntry);
	}
}
