# Install Jupyter Notebooks for ML.NET

## Install Python and Jupyter
* Download [Anaconda](https://www.anaconda.com/distribution/) to install Python and Jupyter
* Open Anaconda Prompt
* Open Jupyter Notebook ```jupyter notebook```

## Install _dotnet try_ tool
 ```
 dotnet tool install -g dotnet-try
 ```

In Anaconda Prompt:

```
dotnet-try jupyter install
```

## Run Jupyter Notebook
* Open Anaconda Prompt
* Switch to target directory

```
jupyter notebook
```

##### C#:
```cs
#r "nuget:XPlot.Plotly,3.0.1"

using XPlot.Plotly;
```

##### Markdown:
```
## Generate Numbers
```

##### C#:
```cs
%%time
var numbers = Enumerable.Range(0, 1000);

display(numbers);
```
```cs
var chart = Chart.Plot(
    new Graph.Scatter() 
    {
        x = numbers
    }
);

display(chart);
```


