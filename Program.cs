using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        // Add your Computer Vision subscription key and endpoint
        static string subscriptionKey = "4bdfbf8bc448462bae296046ad08e2a5";
        static string endpoint = "https://acv-trabalhoia.cognitiveservices.azure.com/";
        
        // URL image used for analyzing an image
        private const string ANALYZE_URL_IMAGE = "https://thumbs.dreamstime.com/z/beautiful-shot-sea-waves-crashing-against-shore-lighthouse-sunset-nazare-portugal-275236690.jpg";

        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
                new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
                { Endpoint = endpoint };
            return client;
        }

        public static async Task AnalyzeImageUrl(ComputerVisionClient client, string imageUrl)
        {
            // Creating a list that defines the features to be extracted from the image. 
            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
            };
        
            // Analyze the URL image 
            ImageAnalysis results = await client.AnalyzeImageAsync(ANALYZE_URL_IMAGE, features);
        
            // Summarizes the image content.
            Console.WriteLine("Summary:");
            foreach (var caption in results.Description.Captions)
            {
                Console.WriteLine($"{caption.Text} with confidence {caption.Confidence}");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // Create a client
            ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);
            
            // Analyze an image to get features and other properties.
            AnalyzeImageUrl(client, ANALYZE_URL_IMAGE).Wait();
        }
    }
}