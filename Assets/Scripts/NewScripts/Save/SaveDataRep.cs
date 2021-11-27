using System;
using System.Data;
using System.IO;
using UnityEngine;
namespace AS
{
    public class SaveDataRep : ISavePlayerPosition
    {

        public event Action<int> OnLoadHealth = delegate (int pos) { };
        private readonly IDataSave<SavedData> _data;
        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;
        private Transform _currentPlayer;

        public SaveDataRep()
        {

            _data = new XMLData();
            _path = Path.Combine(Application.dataPath, _folderName);

        }

        public void Save(int player)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            var savePlayer = new SavedData
            {
                Health = player,
                Name = "Max",
                IsEnabled = true
            };

            _data.Save(savePlayer, Path.Combine(_path, _fileName));
            Debug.Log("Save");
        }

        public void Load(int player)
        {
            var file = Path.Combine(_path, _fileName);
            if (!File.Exists(file))
            {
                throw new DataException($"File {file} not found");
            }
            var newPlayer = _data.Load(file);
            player = newPlayer.Health;

            OnLoadHealth?.Invoke(player);
            Debug.Log(newPlayer);
        }
    }
}
