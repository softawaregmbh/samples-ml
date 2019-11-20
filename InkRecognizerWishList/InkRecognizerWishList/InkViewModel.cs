using Azure.AI.InkRecognizer;
using Azure.AI.InkRecognizer.Models;
using softaware.ViewPort.Commands;
using softaware.ViewPort.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InkRecognizerWishList
{
    public class InkViewModel : NotifyPropertyChanged
    {
        private readonly string language;
        private readonly IInkRecognizer writingRecognizer;
        private IList<InkStroke> strokes;
        private IList<InkWord> words;

        public InkViewModel(string language, IInkRecognizer writingRecognizer)
        {
            this.language = language ?? throw new ArgumentNullException(nameof(language));
            this.writingRecognizer = writingRecognizer ?? throw new ArgumentNullException(nameof(writingRecognizer));

            this.strokes = new List<InkStroke>();
            this.RecognizeCommand = new AsyncCommand(RecognizeAsync);
        }

        public AsyncCommand RecognizeCommand { get; set; }

        public void ClearStrokes()
        {
            this.strokes.Clear();
        }

        public void AddStroke(uint id, IEnumerable<InkPoint> points)
        {
            this.strokes.Add(new InkStroke(points, this.language, id));
        }
        
        public IList<InkWord> Words
        {
            get { return words; }
            set { this.SetProperty(ref this.words, value);  }
        }

        private async Task RecognizeAsync()
        {
            var result = await this.writingRecognizer.RecognizeAsync(this.strokes);
            this.Words = result.GetWords().ToList();
        }
    }
}
