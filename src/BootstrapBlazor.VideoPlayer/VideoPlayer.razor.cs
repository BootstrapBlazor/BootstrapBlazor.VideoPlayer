// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components;

/// <summary>
/// 视频播放器 VideoPlayer 组件
/// </summary>
public partial class VideoPlayer : IAsyncDisposable
{
    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    private DotNetObjectReference<VideoPlayer>? Instance { get; set; }

    private ElementReference Element { get; set; }

    private bool IsInitialized { get; set; }

    private string? DebugInfo { get; set; }

    [NotNull]
    private string? Id { get; set; }

    /// <summary>
    /// 资源地址
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public string? Url { get; set; }

    /// <summary>
    /// 资源类型
    /// <para>video/mp4</para>
    /// <para>application/x-mpegURL</para>
    /// <para>video/ogg</para>
    /// <para>video/x-matroska</para>
    /// <para>更多参考 EnumVideoType</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MineType { get; set; } = "application/x-mpegURL";

    /// <summary>
    /// 宽度
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 300;

    /// <summary>
    /// 高度
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 200;

    /// <summary>
    /// 显示控制条,默认 true
    /// </summary>
    [Parameter]
    public bool Controls { get; set; } = true;

    /// <summary>
    /// 自动播放,默认 true
    /// </summary>
    [Parameter]
    public bool Autoplay { get; set; } = true;

    /// <summary>
    /// 预载,默认 auto
    /// </summary>
    [Parameter]
    public string Preload { get; set; } = "auto";

    /// <summary>
    /// 设置封面资源,相对或者绝对路径
    /// </summary>
    [Parameter]
    public string? Poster { get; set; }

    /// <summary>
    /// 显示调试信息
    /// </summary>
    [Parameter]
    public bool Debug { get; set; }

    /// <summary>
    /// 获得/设置 错误回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnError { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Id = $"vp_{GetHashCode()}";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.VideoPlayer/app.js");
            Instance = DotNetObjectReference.Create(this);
            await MakesurePlayerReady();
        }
    }

    /// <summary>
    /// 初始化,无 Url 合法参数不进行初始化, Reload 会检测并重新初始化
    /// </summary>
    /// <returns></returns>
    private async Task MakesurePlayerReady()
    {
        if (!IsInitialized)
        {
            if (string.IsNullOrEmpty(Url))
            {
                await Logger($"Url is empty");
            }
            else
            {
                var option = new VideoPlayerOption()
                {
                    Width = Width,
                    Height = Height,
                    Controls = Controls,
                    Autoplay = Autoplay,
                    Preload = Preload,
                    Poster = Poster
                };
                option.Sources.Add(new VideoSources(MineType, Url));
                await Module.InvokeVoidAsync("loadPlayer", Instance, Id, option);
            }
        }
    }

    /// <summary>
    /// 切换播放资源
    /// </summary>
    /// <param name="url"></param>
    /// <param name="mineType"></param>
    /// <returns></returns>
    public virtual async Task Reload(string url, string mineType)
    {
        Url = url;
        MineType = mineType;
        await MakesurePlayerReady();
        await Module.InvokeVoidAsync("reloadPlayer", url, mineType);
    }

    /// <summary>
    /// 设置封面
    /// </summary>
    /// <param name="poster"></param>
    /// <returns></returns>
    public virtual async Task SetPoster(string poster)
    {
        Poster = poster;
        await Module.InvokeVoidAsync("setPoster", poster);
    }

    /// <summary>
    /// JS回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public void GetInit() => IsInitialized = true;

    /// <summary>
    /// JS回调方法
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task Logger(string message)
    {
        DebugInfo = message;
        if (Debug)
        {
            StateHasChanged();
        }

        Console.WriteLine(DebugInfo);
        if (OnError != null)
        {
            await OnError.Invoke(DebugInfo);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        if (Module is not null)
        {
            await Module.InvokeVoidAsync("destroy", Id);
            await Module.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
