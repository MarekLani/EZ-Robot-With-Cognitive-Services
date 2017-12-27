using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFormApplication
{
    class Settings
    {
        public string IPAdress { get; set; } = "0.0.0.0";
        public string FaceApiKey { get; set; } = "";
        public string EmotionApiKey { get; set; } = "";
        public string SpeechRecognitionApiKey { get; set; } = "";
        public string LuisEndpoint { get; set; } = "";
        public string SpeakerRecognitionApiKeyValue { get; set; } = "";

        public string PredictionKey { get; set; } = "";
        public string VisionApiKey { get; set; } = "";

        public string VisionApiProjectId  { get; set; } = "";
        public string VisionApiIterationId { get; set; } = "";


        private static Settings instance;

        private Settings() { }

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = LoadSettings();
                }
                return instance;
            }

        }

        private static Settings LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                var json = File.ReadAllText("settings.json");
                return JsonConvert.DeserializeObject<Settings>(json);
            }
            else
                return new Settings();

        }

        public static void SaveSettings()
        {
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(Settings.Instance));
        }
    }
}
