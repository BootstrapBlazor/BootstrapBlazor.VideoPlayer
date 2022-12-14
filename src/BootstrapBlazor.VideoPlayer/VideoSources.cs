// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 播放资源
/// </summary>
public class VideoSources
{
    public VideoSources() { }

    public VideoSources(string? type, string? src)
    {
        this.Type = type ?? throw new ArgumentNullException(nameof(type));
        this.Src = src ?? throw new ArgumentNullException(nameof(src));
    }

    /// <summary>
    /// 资源类型
    /// <para>video/mp4</para>
    /// <para>application/x-mpegURL</para>
    /// <para>video/ogg</para>
    /// <para>video/x-matroska</para>
    /// <para>更多参考 EnumVideoType</para>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; } = "application/x-mpegURL";

    /// <summary>
    /// 资源地址
    /// </summary>
    [JsonPropertyName("src")]
    public string? Src { get; set; }
}
