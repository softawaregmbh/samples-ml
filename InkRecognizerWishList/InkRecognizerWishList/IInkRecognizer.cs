using Azure.AI.InkRecognizer;
using Azure.AI.InkRecognizer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InkRecognizerWishList
{
    public interface IInkRecognizer
    {
        Task<RecognitionRoot> RecognizeAsync(IList<InkStroke> strokes);
    }
}
