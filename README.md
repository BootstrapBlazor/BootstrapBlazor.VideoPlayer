# Blazor VideoPlayer 视频播放器 组件  


示例:

https://www.blazor.zone/videoPlayers

https://blazor.app1.es/videoPlayers

使用方法:

1.nuget包

```BootstrapBlazor.VideoPlayer```

2._Imports.razor 文件 或者页面添加 添加组件库引用

```@using BootstrapBlazor.Components```


3.razor页面
```
<VideoPlayer MineType="application/x-mpegURL" Url="https://test-streams.mux.dev/x36xhzz/x36xhzz.m3u8" />

<VideoPlayer MineType="video/mp4" Url="//vjs.zencdn.net/v/oceans.mp4" />

<VideoPlayer MineType="video/mp4" Url="//vjs.zencdn.net/v/oceans.mp4" Width="400" Height="300" Autoplay="false" Poster="//vjs.zencdn.net/v/oceans.png" />

```

4.参数说明

|  类型   |  参数   | 说明  | 默认值  | 
|  ----  |  ----  | ----  | ----  | 
| string | Url  | 资源地址 | null | 
| string | MineType  | 资源类型,video/mp4, application/x-mpegURL, video/ogg .. 更多参考 EnumVideoType | application/x-mpegURL | 
| int | Width  | 宽度 | 300 | 
| int | Height  | 高度 | 200 | 
| bool | Controls  | 显示控制条 | true | 
| bool | Autoplay  | 自动播放 | true | 
| string | Poster  | 设置封面资源,相对或者绝对路径 |  | 
| string | Language  | 界面语言,默认 获取当前文化, 例如 zh-CN / en-US,如果语言包不存在,回退到 zh-CN | 当前文化 | 
| VideoPlayerOption | Option  | 播放器选项, 不为空则优先使用播放器选项,否则使用参数构建 | null | 
| async Task |  Reload(string? url, string? type) | 切换播放资源 | |
| async Task |  SetPoster(string? poster) | 设置封面 | |
| Func&lt;string, Task&gt;? |  OnError | 错误回调 |

#### 更新历史

v 8.0.4
- 优化组件

v 8.0.1
- 修复播放器某些特殊浏览器无法播放的问题, 升级内置播放器版本到 8.6.1
- 添加 VideoJsPath : 自定义video.js路径,默认为null,使用内置video.js
- 添加 LanguagePath : 自定义语言包路径,默认为null,使用内置语言包

v7.0.2 
- 添加 Language : 界面语言,默认 获取当前文化, 例如 zh-CN / en-US,如果语言包不存在,回退到 zh-CN 

v7.0.3 
- 如果语言代码与子代码（例如en-us）不匹配，则使用主代码（例如en）的匹配项（如果可用）, 如果语言包不存在,回退到 zh-CN 

---
#### Blazor 组件

[条码扫描 ZXingBlazor](https://www.nuget.org/packages/ZXingBlazor#readme-body-tab)
[![nuget](https://img.shields.io/nuget/v/ZXingBlazor.svg?style=flat-square)](https://www.nuget.org/packages/ZXingBlazor) 
[![stats](https://img.shields.io/nuget/dt/ZXingBlazor.svg?style=flat-square)](https://www.nuget.org/stats/packages/ZXingBlazor?groupby=Version)

[图片浏览器 Viewer](https://www.nuget.org/packages/BootstrapBlazor.Viewer#readme-body-tab)
  
[条码扫描 BarcodeScanner](Densen.Component.Blazor/BarcodeScanner.md)
   
[手写签名 Handwritten](Densen.Component.Blazor/Handwritten.md)

[手写签名 SignaturePad](https://www.nuget.org/packages/BootstrapBlazor.SignaturePad#readme-body-tab)

[定位/持续定位 Geolocation](https://www.nuget.org/packages/BootstrapBlazor.Geolocation#readme-body-tab)

[屏幕键盘 OnScreenKeyboard](https://www.nuget.org/packages/BootstrapBlazor.OnScreenKeyboard#readme-body-tab)

[百度地图 BaiduMap](https://www.nuget.org/packages/BootstrapBlazor.BaiduMap#readme-body-tab)

[谷歌地图 GoogleMap](https://www.nuget.org/packages/BootstrapBlazor.Maps#readme-body-tab)

[蓝牙和打印 Bluetooth](https://www.nuget.org/packages/BootstrapBlazor.Bluetooth#readme-body-tab)

[PDF阅读器 PdfReader](https://www.nuget.org/packages/BootstrapBlazor.PdfReader#readme-body-tab)

[文件系统访问 FileSystem](https://www.nuget.org/packages/BootstrapBlazor.FileSystem#readme-body-tab)

[光学字符识别 OCR](https://www.nuget.org/packages/BootstrapBlazor.OCR#readme-body-tab)

[电池信息/网络信息 WebAPI](https://www.nuget.org/packages/BootstrapBlazor.WebAPI#readme-body-tab)

[视频播放器 VideoPlayer](https://www.nuget.org/packages/BootstrapBlazor.VideoPlayer#readme-body-tab)

#### AlexChow

[今日头条](https://www.toutiao.com/c/user/token/MS4wLjABAAAAGMBzlmgJx0rytwH08AEEY8F0wIVXB2soJXXdUP3ohAE/?) | [博客园](https://www.cnblogs.com/densen2014) | [知乎](https://www.zhihu.com/people/alex-chow-54) | [Gitee](https://gitee.com/densen2014) | [GitHub](https://github.com/densen2014)

