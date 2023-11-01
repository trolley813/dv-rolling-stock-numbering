using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityModManagerNet;

namespace DVNums
{
	[Serializable]
	public enum PrefixingMode
	{
		[Description("Classic (L-xxx)")]
		Classic,
		[Description("Model-based (e.g. DE2-xxx)")]
		ModelBased // e.g. DE2-xxx etc.
	}

	[Serializable]
	public enum NumberingMode
	{
		[Description("Classic (000 to 099)")]
		Classic,
		[Description("Classic 3-digit (000 to 999)")]
		Classic3Digit,
		[Description("Unified range (same for all models)")]
		UnifiedRange,
		[Description("Model-based ranges (different per each model)")]
		ModelBasedRange
	}

	[Serializable]
	public class NumberRanges
	{
		[Header("DE2")]
		[Draw("Min", Min = 0, Max = 9999)] public int DE2Min = 100;
		[Draw("Max", Min = 0, Max = 9999)] public int DE2Max = 299;
		[Header("DE6")]
		[Draw("Min", Min = 0, Max = 9999)] public int DE6Min = 600;
		[Draw("Max", Min = 0, Max = 9999)] public int DE6Max = 799;
		[Header("DM3")]
		[Draw("Min", Min = 0, Max = 9999)] public int DM3Min = 300;
		[Draw("Max", Min = 0, Max = 9999)] public int DM3Max = 399;
		[Header("DH4")]
		[Draw("Min", Min = 0, Max = 9999)] public int DH4Min = 400;
		[Draw("Max", Min = 0, Max = 9999)] public int DH4Max = 599;
		[Header("S060")]
		[Draw("Min", Min = 0, Max = 9999)] public int S060Min = 1;
		[Draw("Max", Min = 0, Max = 9999)] public int S060Max = 99;
		[Header("S282")]
		[Draw("Min", Min = 0, Max = 9999)] public int S282Min = 800;
		[Draw("Max", Min = 0, Max = 9999)] public int S282Max = 999;
	}

	public class Settings : UnityModManager.ModSettings, IDrawable
	{
		[Draw("Prefixing mode")] public PrefixingMode PrefixingMode = PrefixingMode.Classic;
		[Draw("Numbering mode")] public NumberingMode NumberingMode = NumberingMode.Classic;
		[Draw("Min (for unified range)", Min = 0, Max = 9999)] public int UnifiedRangeMin = 0;
		[Draw("Max (for unified range)", Min = 0, Max = 9999)] public int UnifiedRangeMax = 99;
		[Draw("Range Settings", Collapsible = true)] public NumberRanges NumberRanges = new NumberRanges();

		public void OnChange()
		{
			
		}

		public override void Save(UnityModManager.ModEntry modEntry)
		{
			Save(this, modEntry);
		}
	}
}
