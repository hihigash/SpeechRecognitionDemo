using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.SpeechRecognition;

namespace SpeechRecognitionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialization
            var micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(SpeechRecognitionMode.LongDictation, "ja-JP", "<your key>");
            micClient.OnMicrophoneStatus += (sender, eventArgs) =>
            {
                Console.WriteLine("[{0}]", (eventArgs.Recording) ? "MIC ON" : "MIC OFF");
            };

            micClient.OnResponseReceived += (sender, eventArgs) =>
            {
                if (eventArgs.PhraseResponse.RecognitionStatus == RecognitionStatus.RecognitionSuccess)
                {
                    Console.WriteLine(eventArgs.PhraseResponse.Results[0].DisplayText);
                }
            };

            micClient.OnPartialResponseReceived += (sender, eventArgs) =>
            {
                Console.Write(eventArgs.PartialResult);
                Console.SetCursorPosition(0, Console.CursorTop);
            };

            micClient.StartMicAndRecognition();
        }
    }
}
