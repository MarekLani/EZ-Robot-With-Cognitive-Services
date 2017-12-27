using Microsoft.Cognitive.CustomVision;
using Microsoft.Cognitive.CustomVision.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;

namespace EZFormApplication.CognitiveServicesCommunicators
{
    class CustomVisionCommunicator
    {

        private string predictionKey;
        private string visionApiKey;

        private Guid projectId;
        private Guid iterationId;


        PredictionEndpoint endpoint;
        VisionServiceClient vsc; 

        public CustomVisionCommunicator(string predictionKey, string visionApiKey, string projectId, string iterationId)
        {
            this.visionApiKey = visionApiKey;
            this.predictionKey = predictionKey;
            this.projectId = new Guid(projectId);
            this.iterationId = new Guid(iterationId);
            PredictionEndpointCredentials predictionEndpointCredentials = new PredictionEndpointCredentials(predictionKey);
            
            //Create a prediction endpoint, passing in a prediction credentials object that contains the obtained prediction key
            endpoint = new PredictionEndpoint(predictionEndpointCredentials);
            vsc   = new VisionServiceClient(visionApiKey, "https://westeurope.api.cognitive.microsoft.com/vision/v1.0");
        }
     

        public  async Task<string> RecognizeObjectsInImage(Bitmap image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var result = await vsc.AnalyzeImageAsync(memoryStream,new List<VisualFeature>() { VisualFeature.Description });
            return result.Description.Captions[0].Text;
        }

        public  List<ImageTagPrediction> RecognizeObject(Bitmap image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            //We need to seek to begin
            memoryStream.Seek(0, SeekOrigin.Begin);
            var result = endpoint.PredictImage(projectId, memoryStream,iterationId);
            return result.Predictions.ToList();
        }
        
}
}
