# MapAssist 使用指南

2022.6.15早8点，确认开过全图会被封号，所以此地图停止开发，目前仅适合老版单机或者非正版D2R使用（源码）


## 视频

基础教程

https://www.bilibili.com/video/BV1BR4y1A7Uc?share_source=copy_web

高级教程

https://www.bilibili.com/video/BV1jZ4y147Yx?share_source=copy_web

## 下载

![image-20220430022530873](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202204300225954.png)

第一次务必下载731MB的完整包，之后使用最新更新的包覆盖。

-  下载速度过慢，请将文件转发给自己下载，可以满速


## 启动

打开MapAssist.exe

![image-20220416121308331](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202204161213461.png)

- **更新后如果报错，请删除`Config.yaml`**



## 修改配置

会出现在右下角

![image-20220507164324862](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205071643898.png)

点击设置

![image-20220507164421283](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205071644319.png)



默认已经切换成了中文，字体可以随时切换，可配置选项比较多，自己研究一下吧

- 路径已经默认选好了，不需要你再选了



物品过滤功能请使用`MF_Filter`，看群内使用文档或者以下链接

>https://github.com/elmagnificogi/MA_Filter
>
>http://elmagnifico.tech/2022/05/07/Diablo-MA-Filter/



#### 掉落声音

修改掉落声音，声音文件放在同目录下就行了

![image-20220608134553868](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202206081345894.png)

声音文件是wav格式，增加或者替换都可以

![image-20220608134600691](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202206081346716.png)

![image-20220608134512712](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202206081345745.png)







#### 地图偏移

通过修改x和y调整地图偏移

- Center模式下无法调整
- 无法把地图移出窗口

![image-20220519123238802](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205191232838.png)



#### DC追踪

DC追踪功能，请根据需要自行选择

![image-20220508000508689](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205080005823.png)



#### 服务器屏蔽

![img](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205172332003.png)

由于亚服多日卡顿，不见好转，所以增加一个屏蔽功能，选择要屏蔽的服务器，然后应用即可。**建议只保留新加坡，其他全屏蔽**

- 屏蔽以后无法进入到对应的服务器，可能会造成不能进入好友房间
- 有任何问题，可以取消屏蔽，会解除所有屏蔽策略
- 屏蔽需要管理员权限，并且需要Windows防火墙可以正常启动，策略组可修改



#### 房间记录

防止意外掉线，房间不见了，增加了房间记录功能，多开也能区分清楚

![image-20220517232238264](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205172322359.png)

![image-20220517232251086](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205172322122.png)



#### 仓库同步

首先登录下面网址，注册一个账号

> https://resurrected.trade/overview

仓库同步中输入刚才注册的账号和密码

![image-20220517232405130](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205172324173.png)

- 只需要登录一次就行了

只要在游戏内，就能自动同步你的仓库和物品到Web端，然后就可以出售了

![image-20220517232525739](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205172325921.png)

![image-20220517232751978](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205172327180.png)

- 右上角切换成中文
- 首次同步可能需要一点时间，数据量比较大
- 功能还不完善，还在开发中，有问题欢迎提
- 使用的人太少了，集市还太小了
- Web的那个同步软件可以不用下载，用我的MapAssist就行

**不使用此功能，请关闭，否则会有额外CPU占用**

![image-20220519123522946](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205191235982.png)



#### HC功能

快捷键退出到选择人物

如果开启血量监控，会自动退出到选择人物

![image-20220524210126089](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205242101175.png)

同理蓝量

#### 快捷键设置

**不要和已有游戏快捷键冲突或者是某些其他软件的全局快捷键冲突**



F8可以直接复制粘贴当前房间名和密码，在外面只需要`ctrl-v`粘贴就行了

- 不需要的快捷键可以按下`Backspace`删除

![image-20220520212737853](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205202127949.png)

F11房间名自动+1

```
牛场[0]一起来快活啊
```

按下快捷键以后会自动复制并粘贴以下内容，数字会随着按下次数增加

```
牛场1一起来快活啊
```

![image-20220519160944506](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205191609558.png)

F12仅复制当前房间名



## CPU占用过高

![image-20220525212725241](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205252127336.png)

建议适当调节地图刷新频率，过高会导致CPU占用高得离谱



## 物品过滤

![image-20220522170044735](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205221700766.png)

![image-20220522170217797](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205221702843.png)

点击后自动打开我的过滤器，导入同目录下的`ItemFilter.yaml`就可以进行修改了

![image-20220522170203980](http://img.elmagnifico.tech:9514/static/upload/elmagnifico/202205221702010.png)

具体使用细节请看同目录下的`MA_Filter使用指南.pdf`



## 游戏更新地图失效

请在频道内等待新版，**替换老客户端的方法已经失效了**
