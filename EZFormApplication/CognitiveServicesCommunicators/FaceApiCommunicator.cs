using Microsoft.ProjectOxford.Common;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFormApplication.CognitiveServicesCommunicators
{
    public class FaceApiCommunicator
    {
        private const string FaceApiEndpoint = "https://westeurope.api.cognitive.microsoft.com/face/v1.0/";
        private static string faceApiKeyValue = "7aeb6c349d3b4155a9e00480b4e43ab7";
        private static string emotionApiKeyValue = "62a9031e98ab4a55830db75794af98d3";
        private static List<FaceResult> personResults = new List<FaceResult>();
        private static DateTime lastFaceDetectTime = DateTime.MinValue;

        static FaceServiceClient fsc = new FaceServiceClient(faceApiKeyValue, FaceApiEndpoint);
        static EmotionServiceClient esc = new EmotionServiceClient(emotionApiKeyValue);

        public static async Task<(Face[] faces, Person person, Emotion[] emotions)> DetectAndIdentifyFace(Bitmap image)
        {
            //FACE Detection
            //TODO add detection interval as param
            Emotion[] emotions = null;
            Person person = null;
            Face[] faces = null;


            //Detect and identify only once per 10 seconds
            if (lastFaceDetectTime.AddSeconds(10) < DateTime.Now)
            {
                lastFaceDetectTime = DateTime.Now;

                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                //We need to seek to begin
                memoryStream.Seek(0, SeekOrigin.Begin);
                faces = await fsc.DetectAsync(memoryStream, true, true, new List<FaceAttributeType>() { FaceAttributeType.Age, FaceAttributeType.Gender });

                if (faces.Any())
                {

                    var rec = new Microsoft.ProjectOxford.Common.Rectangle[] { faces.First().FaceRectangle.ToRectangle() };
                    //Emotions

                    //We need to seek to begin
                    memoryStream = new MemoryStream();
                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    emotions = await esc.RecognizeAsync(memoryStream, rec);


                    //Identification
                    var groups = await fsc.ListPersonGroupsAsync();
                    var groupId = groups.First().PersonGroupId;
                    var identifyResult = await fsc.IdentifyAsync(groupId, new Guid[] { faces.First().FaceId }, 1);
                    var candidate = identifyResult?.FirstOrDefault()?.Candidates?.FirstOrDefault();

                    if (candidate != null)
                    {
                        person = await fsc.GetPersonAsync(groupId, candidate.PersonId);
                    }

                }
            }
            return (faces, person, emotions);
        }

        public static void SetFaceKey(string value)
        {
            faceApiKeyValue = value;
            fsc = new FaceServiceClient(faceApiKeyValue, FaceApiEndpoint);
        }

        public static void SetEmotionKey(string value)
        {
            emotionApiKeyValue = value;
            esc = new EmotionServiceClient(emotionApiKeyValue);
        }
    }

    public class FaceResult
    {
        public string Name { get; set; }
        public DateTime IdentifiedAt { get; set; }
    }
}
