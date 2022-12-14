// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 播放器选项
/// </summary>
public class VideoPlayerOption
{
    /// <summary>
    /// 宽度
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; } = 300;

    /// <summary>
    /// 高度
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; } = 200;

    /// <summary>
    /// 显示控制条,默认 true
    /// </summary>
    [JsonPropertyName("controls")]
    public bool Controls { get; set; } = true;

    /// <summary>
    /// 自动播放,默认 true
    /// </summary>
    [JsonPropertyName("autoplay")]
    public bool Autoplay { get; set; } = true;

    /// <summary>
    /// 预载,默认 auto
    /// </summary>
    [JsonPropertyName("preload")]
    public string Preload { get; set; } = "auto";

    /// <summary>
    /// 播放资源
    /// </summary>
    [JsonPropertyName("sources")]
    public List<VideoSources> Sources { get; set; } = new List<VideoSources>();

    /// <summary>
    /// 设置封面资源,相对或者绝对路径
    /// </summary>
    [JsonPropertyName("poster")]
    public string? Poster { get; set; } 

    //[JsonPropertyName("enableSourceset")]
    //public bool EnableSourceset { get; set; }

    //[JsonPropertyName("techOrder")]
    //public string? TechOrder { get; set; } = "['html5', 'flash']";


}
