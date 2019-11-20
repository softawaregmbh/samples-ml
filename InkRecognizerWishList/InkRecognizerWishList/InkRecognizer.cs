using Azure.AI.InkRecognizer;
using Azure.AI.InkRecognizer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InkRecognizerWishList
{
    public class InkRecognizer : IInkRecognizer
    {
        private readonly string subscriptionKey;
        private readonly string endpoint;
        private readonly string language;
        private readonly InkRecognizerClient inkRecognizer;

        public InkRecognizer(string subscriptionKey, string endpoint, string language)
        {
            this.subscriptionKey = subscriptionKey ?? throw new ArgumentNullException(nameof(subscriptionKey));
            this.endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            this.language = language ?? throw new ArgumentNullException(nameof(language));
            string inkPreviewEndpoint = $"{endpoint}/inkrecognizer";

            this.inkRecognizer = new InkRecognizerClient(
                new Uri(inkPreviewEndpoint), 
                new InkRecognizerCredential(subscriptionKey), 
                new InkRecognizerClientOptions(InkRecognizerClientOptions.ServiceVersion.Preview1)
                {
                    ApplicationKind = ApplicationKind.Mixed,
                    Language = language,
                    InkPointUnit = InkPointUnit.Mm,
                    UnitMultiple = 1
                });
        }

        public async Task<RecognitionRoot> RecognizeAsync(IList<InkStroke> strokes)
        {
            var response = await this.inkRecognizer.RecognizeInkAsync(strokes, InkPointUnit.Mm, 1, language);
            return response.Value;
        }
    }
}
