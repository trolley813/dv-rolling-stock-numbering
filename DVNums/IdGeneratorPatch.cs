using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV.Logic.Job;
using DV.ThingTypes;
using HarmonyLib;
using UnityEngine;

namespace DVNums
{
	[HarmonyPatch(typeof(IdGenerator), nameof(IdGenerator.GenerateCarID))]
	public class IdGeneratorPatch
	{

		public static bool Prefix(ref string __result, TrainCarLivery carType)
		{
			if (!CarTypes.IsLocomotive(carType))
			{
				return true;
			}
			__result = GenerateCarID(carType);
			return false;
		}

		
		public static string GenerateCarID(TrainCarLivery carType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool isLoco = CarTypes.IsLocomotive(carType);
			string locoPrefix = "L";
			var settings = Main.Settings;
			if (settings.PrefixingMode == PrefixingMode.ModelBased)
			{
				locoPrefix = carType.v1 switch
				{
					TrainCarType.LocoS060 => "S060",
					TrainCarType.LocoShunter => "DE2",
					TrainCarType.LocoDM3 => "DM3",
					TrainCarType.LocoDH4 => "DH4",
					TrainCarType.LocoDiesel => "DE6",
					TrainCarType.LocoSteamHeavy => "S282",
					_ => "L"
				};
			}
			stringBuilder.Append(isLoco ? locoPrefix : "C");
			stringBuilder.Append(carType.parentType.carInstanceIdGenBase);
			var (minNum, maxNum) = settings.NumberingMode switch
			{
				NumberingMode.Classic => (0, 100),
				NumberingMode.Classic3Digit => (0, 1000),
				NumberingMode.UnifiedRange => (settings.UnifiedRangeMin, settings.UnifiedRangeMax + 1),
				NumberingMode.ModelBasedRange => carType.v1 switch
				{
					TrainCarType.LocoS060 => (settings.NumberRanges.S060Min, settings.NumberRanges.S060Max + 1),
					TrainCarType.LocoShunter => (settings.NumberRanges.DE2Min, settings.NumberRanges.DE2Max + 1),
					TrainCarType.LocoDM3 => (settings.NumberRanges.DM3Min, settings.NumberRanges.DM3Max + 1),
					TrainCarType.LocoDH4 => (settings.NumberRanges.DH4Min, settings.NumberRanges.DH4Max + 1),
					TrainCarType.LocoDiesel => (settings.NumberRanges.DE6Min, settings.NumberRanges.DE6Max + 1),
					TrainCarType.LocoSteamHeavy => (settings.NumberRanges.S282Min, settings.NumberRanges.S282Max + 1),
					_ => (0, 1000)
				},
				_ => (0, 1000)
			};
			bool freeNumberFound = false;
			int genCarNumber = IdGenerator.idRng.Next(minNum, maxNum);
			int carNumber = genCarNumber;
			do
			{
				string text = carNumber.ToString("D3");
				if (!IdGenerator.Instance.existingCarIds.Contains(stringBuilder.ToString() + text))
				{
					stringBuilder.Append(text);
					freeNumberFound = true;
					break;
				}

				carNumber = ((carNumber < maxNum - 1) ? (carNumber + 1) : minNum);
			}
			while (carNumber != genCarNumber);
			if (!freeNumberFound)
			{
				Debug.LogError($"Couldn't find free carId for type:{carType} within limit [{maxNum}]! Finding first available carId number above limit!");
				int reserveCarNumber = 1000;
				string text;
				while (true)
				{
					text = reserveCarNumber.ToString("D3");
					if (!IdGenerator.Instance.existingCarIds.Contains(stringBuilder.ToString() + text))
					{
						break;
					}

					reserveCarNumber++;
				}

				stringBuilder.Append(text);
			}

			string carId = stringBuilder.ToString();
			IdGenerator.Instance.RegisterCarId(carId);
			return carId;
		}
	}
}
