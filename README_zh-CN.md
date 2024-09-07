# PearlCalculatorBlazor

仅为珍珠计算器网站项目

![GitHub](https://img.shields.io/github/license/whats2000/PearlCalculatorBlazor)

[English](README.md) | [繁體中文](README_zh-TW.md)

## 使用方式

此网站支持多个浏览器（Google、Edge 等）。

### 先决条件

请在继续之前安装 Dotnet Core 运行时。

- dotnet 6.0 (链接: https://dotnet.microsoft.com/download)

### PearlCalculationLib

来自 /LegendsOfSky/PearlCalculatorCore 的库 (链接: https://github.com/LegendsOfSky/PearlCalculatorCore)

查看更多信息，详见该函数库。

## 支持的语言

- English
- Español
- 繁體中文
- 简体中文

## 如何使用

这里是使用教程

### 设置 FTL 信息

首先，您需要设置 FTL 信息。前往 `更多基础选项`。您也可以通过点击 `导入设置` 来导入设置。

以下是 FTL 设置。

| 名称                   | 值类型    | 默认值                | 注释                                                                                                         |
|----------------------|--------|--------------------|------------------------------------------------------------------------------------------------------------|
| `North West TNT (X)` | double | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `North West TNT (Y)` | double | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `North West TNT (Z)` | double | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `North East TNT (X)` | double | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `North East TNT (Y)` | double | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `North East TNT (Z)` | double | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `South West TNT (X)` | double | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `South West TNT (Y)` | double | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `South West TNT (Z)` | double | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `South East TNT (X)` | double | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `South East TNT (Y)` | double | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `South East TNT (Z)` | double | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您 |
| `珍珠 Y 轴运动模式`         | enum   | 正常抛射               | 决定是否使用 Y 轴运动取消技术，网站中选择 `正常抛射` 表示不使用 Y 轴运动取消技术，或者使用此技术                                                      |
| `珍珠 Y 坐标`            | double | 170.3472263892941  | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您       |
| `珍珠 Y 矢量`            | double | 0.2716278719434352 | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` 并使用 `/tick freeze` 和 `/tick step 1` 可能会帮助您       |
| `珍珠 Y 轴位置（原始）`       | double | 170.34722638929412 | 移除用于取消 Y 轴运动的活塞，并在爆炸时刻获得珍珠的 Y 轴位置，仅在 `珍珠 Y 轴运动模式` 设为 `完美水平抛射` 时显示                                          |
| `珍珠 Y 轴位置（调整后）`      | double | 170.0              | 放置用于取消 Y 轴运动的活塞，并在时间结束时获得珍珠的 Y 轴位置，仅在 `珍珠 Y 轴运动模式` 设为 `完美水平抛射` 时显示                                         |
| `默认红色 TNT 位置`        | enum   | SouthEast          | 默认红色阵列 TNT 位置，相对于珍珠的位置，未被粘液块更改                                                                             |
| `默认蓝色 TNT 位置`        | enum   | NorthWest          | 默认蓝色阵列 TNT 位置，相对于珍珠的位置，未被粘液块更改                                                                             |

### 设置珍珠与目标点信息

第二步，您需要设置珍珠与 TNT/目标点的信息。前往 `一般选项`。您也可以通过点击 `导入设置` 来导入设置。

以下是珍珠与目标点的信息设置。

| 名称        | 值类型    | 默认值 | 注释                                                                                                 |
|-----------|--------|-----|----------------------------------------------------------------------------------------------------|
| `珍珠X坐标`   | double | 0   | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` 使用 `/tick freeze` 和 `/tick step 1` 可能帮助您 |
| `珍珠Z坐标`   | double | 0   | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` 使用 `/tick freeze` 和 `/tick step 1` 可能帮助您 |
| `目标点X坐标`  | double | 0   | 目标点的 X 坐标                                                                                          |
| `目标点Z坐标`  | double | 0   | 目标点的 Z 坐标                                                                                          |
| `最大TNT当量` | int    | 0   | 每侧 FTL 的最大 TNT 数量                                                                                  |
| `方向`      | enum   | 北   | 这将影响珍珠模拟的方向，点击 TNT 数量输出后会自动设置                                                                      |
| `红色阵列`    | int    | 0   | 这是存储在 FTL ROM 中的红色 TNT 数量，点击 TNT 数量输出后会自动设置                                                        |
| `蓝色阵列`    | int    | 0   | 这是存储在 FTL ROM 中的蓝色 TNT 数量，点击 TNT 数量输出后会自动设置                                                        |

设置目标点信息后，您可以点击 `计算TNT当量` 来输出结果列表。

您可以从侧边栏选择输出结果之一，自动填入 `方向`、`红色阵列` 和 `蓝色阵列`。

### 设置输出顺序

接下来，我们可以设置输出顺序。前往 `进阶选项`。

以下是输出顺序设置。

| 名称  | 值类型    | 默认值        | 注释                    |
|-----|--------|------------|-----------------------|
| `X` | double | 0 (-1<X<1) | 珍珠 X 坐标与熔岩池中心坐标之间的偏移量 |
| `Z` | double | 0 (-1<Z<1) | 珍珠 Z 坐标与熔岩池中心坐标之间的偏移量 |

| `TNT 权重` | int | 0 | 输出顺序 |
| `结果排序控制` | enum | 混合加权距离 | 输出顺序，开启图表模式可以直观地查看结果 |

点击您想要的数量输出。

最后，点击 `珍珠模拟` 查看珍珠飞行路径。

### 额外工具

您可以前往 `TNT编码` 页面将 TNT 数量编码至 FTL ROM。

| 名称          | 值类型    | 默认值 | 注释                                                                    |
|-------------|--------|-----|-----------------------------------------------------------------------|
| `红色TNT编码配置` | string |     | 输入框中用 `,` 分隔数字，示例：`1, 2, 4, 8, 10, 20, 40, 80, 160`，会转换为 `int[]` 进行计算 |
| `蓝色TNT编码配置` | string |     | 输入框中用 `,` 分隔数字，示例：`1, 2, 4, 8, 10, 20, 40, 80, 160`，会转换为 `int[]` 进行计算 |

点击 `计算 TNT 编码组合` 获取结果。

### 导出与导入

您可以通过点击 `保存设置` 和 `导入设置` 进行设置的导出与导入，位于 `一般选项` 页面下。

### 图形输出

您可以点击 `图表模式` 查看图形输出。

**这是仍在开发中的新功能。欢迎提出任何建议。**

## 切换炮台设置

顶部可以看到具有炮台名称的切换图标。点击它可以切换炮台设置。

在编辑侧边栏，您可以轻松执行以下操作：

- 更改炮台名称
- 删除炮台
- 添加新炮台
- 重置炮台
- 复制炮台设置

**这是仍在开发中的新功能。欢迎提出任何建议。**

### SVG 图标引用

- [Moon Dark Theme SVG Vector](https://www.svgrepo.com/svg/304625/moon-dark-theme): 授权 CC0
- [Sun Light Theme SVG Vector](https://www.svgrepo.com/svg/304624/sun-light-theme): 授权 CC0
- [Coordinates SVG Vector](https://www.svgrepo.com/svg/95272/coordinates): 授权 CC0
