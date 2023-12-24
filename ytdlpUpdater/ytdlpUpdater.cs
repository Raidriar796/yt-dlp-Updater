using ResoniteModLoader;
using System.Diagnostics;

namespace ytdlpUpdater;

public class ytdlpUpdater : ResoniteMod
{
    public override string Name => "yt-dlp-Updater";
    public override string Author => "Raidriar796";
    public override string Version => "1.0.0";
    public override string Link => "https://github.com/Raidriar796/yt-dlp-Updater";

    public override void OnEngineInit()
    {
        Process process = new Process();
        process.StartInfo.FileName = "RuntimeData/yt-dlp.exe";
        process.StartInfo.Arguments = "-U";
        process.Start();
    }
}
