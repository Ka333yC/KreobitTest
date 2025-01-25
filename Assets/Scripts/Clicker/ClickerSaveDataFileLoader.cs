using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

namespace DefaultNamespace.Clicker
{
	public class ClickerSaveDataFileLoader
	{
		private const string FileName = "clicker_save.json";

		private readonly string _saveFilePath;

		public ClickerSaveDataFileLoader()
		{
			_saveFilePath = Path.Combine(Application.persistentDataPath, FileName);
		}

		public async UniTask Write(ClickerSaveData clickerSaveData) 
		{
			await UniTask.RunOnThreadPool(() => WriteInternal(clickerSaveData));
		}

		public UniTask<ClickerSaveData> Read() 
		{
			return UniTask.RunOnThreadPool(ReadInternal);
		}

		private void WriteInternal(ClickerSaveData clickerSaveData) 
		{
			var serializedSettings = JsonConvert.SerializeObject(clickerSaveData);
			File.WriteAllText(_saveFilePath, serializedSettings);
		}

		private ClickerSaveData ReadInternal() 
		{
			try
			{
				if(!File.Exists(_saveFilePath))
				{
					return null;
				}

				var serializedSettings = File.ReadAllText(_saveFilePath);
				return JsonConvert.DeserializeObject<ClickerSaveData>(serializedSettings);
			}
			catch(Exception)
			{
				return null;
			}			
		}
	}
}