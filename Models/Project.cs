using System.Text.Json.Serialization;
using MSProject = Microsoft.Build.Evaluation.Project;

namespace Models;
internal class Project
{
    public required string Name { get; set; }
    public required string Path { get; set; }

    public required string TargetPath { get; set; }
    public required string OutputType { get; set; }
    public required string DesignerHostPath { get; set; }
    /** TargetFramework -or- TargetFrameworks must be set. 
        - If both are set, the project will encounter build errors and/or
          produce unexpected property values.
        - If only TargetFrameworks is set, some properties will have unexpected values. 
            - The MSProject must be re-evaluated for each TFM in TargetFrameworks. Use <see cref="GetForTargetFramework(string, TFM)"/>
            with TargetFramework set 
    */
    public required TFM TargetFramework { get; set; }
    public required TFM[] TargetFrameworks { get; set; }
    public required string DepsFilePath { get; set; }
    public required string RuntimeConfigFilePath { get; set; }
    public required string[] ProjectReferences { get; set; }

    [JsonIgnore]
    public MSProject? CoreProject { get; set; } = null;

    public string? DirectoryPath => System.IO.Path.GetDirectoryName(Path);

    public string? IntermediateOutputPath { get; internal set; }

    public override string ToString() => $"{Name} ({OutputType}) - {DesignerHostPath}";
}

internal class ProjectFile
{
    public required string Path { get; set; }

    public required string TargetPath { get; set; }
    public required string ProjectPath { get; set; }

    public override string ToString() => $"{Path} ({TargetPath}) - {ProjectPath}";
}

public record TFM(string Moniker)
{
    public static explicit operator TFM(string v) => new(v);
    public static implicit operator string(TFM v) => v.Moniker;
}