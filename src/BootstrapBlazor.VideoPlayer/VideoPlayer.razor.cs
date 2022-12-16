// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Runtime.Intrinsics.Arm;

namespace BootstrapBlazor.Components;

/// <summary>
/// 视频播放器 VideoPlayer 组件
/// </summary>
public partial class VideoPlayer : IAsyncDisposable
{
    [Inject] IJSRuntime? JS { get; set; }
    public IJSObjectReference? module;
    private DotNetObjectReference<VideoPlayer>? instance { get; set; }
    protected ElementReference element { get; set; }
    private bool IsInitialized;
    private string? info;

    private string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// 资源地址
    /// </summary>
    [Parameter]
    public string? SourcesUrl { get; set; }

    /// <summary>
    /// 资源类型
    /// <para>video/mp4</para>
    /// <para>application/x-mpegURL</para>
    /// <para>video/ogg</para>
    /// <para>video/x-matroska</para>
    /// <para>更多参考 EnumVideoType</para>
    /// </summary>
    [Parameter]
    public string? SourcesType { get; set; } = "application/x-mpegURL";

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
    /// 播放器选项, 不为空则优先使用播放器选项,否则使用参数构建
    /// </summary>
    [Parameter]
    public VideoPlayerOption? Option { get; set; }

    /// <summary>
    /// 显示调试信息
    /// </summary>
    [Parameter]
    public bool Debug { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                module = await JS!.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.VideoPlayer/app.js");
                instance = DotNetObjectReference.Create(this);
                await OnInit();
            }
        }
        catch (Exception e)
        {
            if (OnError != null) await OnError.Invoke(e.Message);
        }
    }

    /// <summary>
    /// 初始化,无 SourcesUrl 合法参数不进行初始化, Reload 会检测并重新初始化
    /// </summary>
    /// <returns></returns>
    async Task OnInit()
    {
        if (string.IsNullOrEmpty(SourcesUrl))
        {
            if (OnError != null) await OnError.Invoke("SourcesUrl is empty.");
            return;
        }
        if (!this.IsInitialized)
        {
            Option = Option ?? new VideoPlayerOption()
            {
                Width = Width,
                Height = Height,
                Controls = Controls,
                Autoplay = Autoplay,
                Preload = Preload,
                Poster = Poster,
                //EnableSourceset= true,
                //TechOrder= "['fakeYoutube', 'html5']"
            };
            Option.Sources.Add(new VideoSources(SourcesType, SourcesUrl));

            try
            {
                await module!.InvokeVoidAsync("loadPlayer", instance, "video_" + Id, Option);
            }
            catch (Exception e)
            {
                info = e.Message;
                if (Debug) StateHasChanged();
                Console.WriteLine(info);
                if (OnError != null) await OnError.Invoke(info);
            }
        }
    }

    /// <summary>
    /// 切换播放资源
    /// </summary>
    /// <param name="url"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public virtual async Task Reload(string? url, string? type)
    {
        await OnInit();
        try
        {
            await module!.InvokeVoidAsync("reloadPlayer", url, type);
        }
        catch (Exception e)
        {
            info = e.Message;
            if (Debug) StateHasChanged();
            Console.WriteLine(info);
            if (OnError != null) await OnError.Invoke(info);
        }
    }

    /// <summary>
    /// 设置封面
    /// </summary>
    /// <param name="poster"></param>
    /// <returns></returns>
    public virtual async Task SetPoster(string? poster)
    {
        try
        {
            await module!.InvokeVoidAsync("setPoster", poster);
        }
        catch (Exception e)
        {
            info = e.Message;
            if (Debug) StateHasChanged();
            Console.WriteLine(info);
            if (OnError != null) await OnError.Invoke(info);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.InvokeVoidAsync("destroy", Id);
            await module.DisposeAsync();
        }
    }


    /// <summary>
    /// 获得/设置 错误回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnError { get; set; }

    /// <summary>
    /// JS回调方法
    /// </summary>
    /// <param name="init"></param>
    /// <returns></returns>
    [JSInvokable]
    public void GetInit(bool init) => this.IsInitialized = init;

    /// <summary>
    /// JS回调方法
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task GetError(string error)
    {
        info = error;
        if (Debug) StateHasChanged();
        if (OnError != null) await OnError.Invoke(error);
    }
}
