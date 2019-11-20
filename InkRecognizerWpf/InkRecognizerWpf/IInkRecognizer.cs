using Azure.AI.InkRecognizer;
using Azure.AI.InkRecognizer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InkRecognizerWpf
{
    public interface IInkRecognizer
    {
        Task<RecognitionRoot> RecognizeAsync(IList<InkStroke> strokes);
    }
}
