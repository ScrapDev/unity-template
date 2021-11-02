using SuperUnityBuild.BuildTool;
using System.IO;

public class GetVersion : BuildAction, IPreBuildAction
{
    [FilePath(false, false, "Path to the project version file.")]
    public string file;

    public override void Execute()
    {
        base.Execute();
        string version = new ProductParameters().buildVersion;
        File.WriteAllText(Path.GetFullPath(file), version);
    }
}
