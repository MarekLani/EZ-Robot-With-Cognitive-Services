using Microsoft.ProjectOxford.Vision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFormApplication.CognitiveServicesCommunicators
{
    class ComputerVisionCommunicator
    {
        private VisionServiceClient vsc;
        private string visionApiKey;
        public ComputerVisionCommunicator()
        {
            this.visionApiKey = Settings.Instance.VisionApiKey;          
            vsc = new VisionServiceClient(visionApiKey, "https://westeurope.api.cognitive.microsoft.com/vision/v1.0");
        }

        public async Task<string> RecognizeObjectsInImage(Bitmap image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var result = await vsc.AnalyzeImageAsync(memoryStream, new List<VisualFeature>() { VisualFeature.Description });
            return result.Description.Captions[0].Text;
        }
    }
}
