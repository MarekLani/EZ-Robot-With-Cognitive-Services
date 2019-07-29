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

        public CustomVisionCommunicator()
        {
            this.visionApiKey = Settings.Instance.VisionApiKey;

            this.predictionKey = Settings.Instance.PredictionKey;
            this.projectId = new Guid(Settings.Instance.VisionApiProjectId);

            //changes everytime when model is retrained
            this.iterationId = new Guid(Settings.Instance.VisionApiIterationId);
            PredictionEndpointCredentials predictionEndpointCredentials = new PredictionEndpointCredentials(predictionKey);
            
            //Create a prediction endpoint, passing in a prediction credentials object that contains the obtained prediction key
            endpoint = new PredictionEndpoint(predictionEndpointCredentials);


            vsc   = new VisionServiceClient(visionApiKey, "https://westeurope.api.cognitive.microsoft.com/vision/v1.0");
        }
     

        public  List<ImageTagPrediction> GetListOfPredictions(Bitmap image)
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
