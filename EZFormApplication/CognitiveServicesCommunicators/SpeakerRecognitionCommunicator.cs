using Microsoft.ProjectOxford.SpeakerRecognition;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFormApplication.CognitiveServicesCommunicators
{
    class SpeakerRecognitionCommunicator
    {
        public async Task<IdentificationOperation> RecognizeSpeaker(string recordingFileName)
        {
            var srsc = new SpeakerIdentificationServiceClient(Settings.Instance.SpeakerRecognitionApiKeyValue);
            var profiles = await srsc.GetProfilesAsync();

            Guid[] testProfileIds = new Guid[profiles.Length];
            for (int i = 0; i < testProfileIds.Length; i++)
            {
                testProfileIds[i] = profiles[i].ProfileId;
            }

            OperationLocation processPollingLocation;
            using (Stream audioStream = File.OpenRead(recordingFileName))
            {
                processPollingLocation = await srsc.IdentifyAsync(audioStream, testProfileIds, true);
            }

            IdentificationOperation identificationResponse = null;
            int numOfRetries = 10;
            TimeSpan timeBetweenRetries = TimeSpan.FromSeconds(5.0);

            while (numOfRetries > 0)
            {
                await Task.Delay(timeBetweenRetries);
                identificationResponse = await srsc.CheckIdentificationStatusAsync(processPollingLocation);

                if (identificationResponse.Status == Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification.Status.Succeeded)
                {
                    break;
                }
                else if (identificationResponse.Status == Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification.Status.Failed)
                {
                    throw new IdentificationException(identificationResponse.Message);
                }
                numOfRetries--;
            }
            if (numOfRetries <= 0)
            {
                throw new IdentificationException("Identification operation timeout.");
            }
            return identificationResponse;
        }
    }
}
