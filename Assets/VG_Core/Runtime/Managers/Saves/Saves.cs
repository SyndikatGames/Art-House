using System;
using UnityEngine;
using VG2.Internal;

namespace VG2
{
    public partial class Saves : Manager
    {
        public static event Action onDeleted;
        public static bool Initialized { get; private set; }
        private static SaveService service => _instance.supportedService as SaveService;

        private static Saves _instance;


        [SerializeField] private StartValuesConfig _startValues;

        protected override string managerName => "VG Saves";


        protected override void OnInitialized()
        {
            _instance = this;

            var dataDictionary = GameStateEncoder.Decode(service.GetData());
            GameStateParcer.Parse(_instance._startValues, dataDictionary);

            Log(Core.Message.Initialized(managerName) + " Data: \n" + dataDictionary);

            Initialized = true;
        }

        public static void Save()
        {
            var gameStateData = GameStateParcer.ToDataString();
            var encodedData = GameStateEncoder.Encode(gameStateData);

            service.Commit(encodedData, (success) =>
            {
                if (success) _instance.Log("Saves commited.");
                else _instance.Log("Saves not commited.");
            });
        }

        public static void Delete()
        {
            service.Commit(string.Empty, (success) => 
            {
                if (success) 
                    _instance.Log("Saves deleted.");
            });
            onDeleted?.Invoke();
        }
        

    }


}




