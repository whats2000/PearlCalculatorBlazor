# PearlCalculatorBlazor
僅為珍珠計算器網站項目

![GitHub](https://img.shields.io/github/license/whats2000/PearlCalculatorBlazor)

[English](README.md) | [简体中文](README_zh-CN.md)


## 使用方式

此網站支持多個瀏覽器（Google、Edge 等）。

### 先決條件

請在繼續之前安裝 Dotnet Core 運行時。

- dotnet 6.0 (鏈接: https://dotnet.microsoft.com/download)

### PearlCalculationLib

來自 /LegendsOfSky/PearlCalculatorCore 的庫 (鏈接: https://github.com/LegendsOfSky/PearlCalculatorCore)

查看更多信息，請參見該函數庫。

## 支持的語言

- English
- Español
- 繁體中文
- 简体中文

## 如何使用

這裡是使用教程

### 設置 FTL 信息

首先，您需要設置 FTL 信息。前往 `更多基礎設定`。您也可以通過點擊 `開啟設定檔` 來導入設置。

以下是 FTL 設置。

| 名稱                   | 值類型    | 預設值                | 註釋                                                                                                         |
|----------------------|--------|--------------------|------------------------------------------------------------------------------------------------------------|
| `游戏版本`               | enum   | 1.11-1.21.1        | 該大砲設計用於 Minecraft 版本                                                                                       |
| `North West TNT (X)` | double | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `North West TNT (Y)` | double | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `North West TNT (Z)` | double | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `North East TNT (X)` | double | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `North East TNT (Y)` | double | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `North East TNT (Z)` | double | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `South West TNT (X)` | double | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `South West TNT (Y)` | double | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `South West TNT (Z)` | double | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `South East TNT (X)` | double | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `South East TNT (Y)` | double | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `South East TNT (Z)` | double | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` 或者 `/log tnt` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您 |
| `珍珠Y軸運動模式`           | enum   | 正常抛射               | 決定是否使用 Y 軸運動取消技術，網站中選擇 `正常抛射` 表示不使用 Y 軸運動取消技術，否則使用此技術                                                      |
| `珍珠 Y 座標`            | double | 170.3472263892941  | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您       |
| `珍珠 Y 動量`            | double | 0.2716278719434352 | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` 並使用 `/tick freeze` 和 `/tick step 1` 可能會幫助您       |
| `珍珠 Y 軸位置（原始）`       | double | 170.34722638929412 | 移除用來取消 Y 軸運動的活塞，並在爆炸時刻獲得珍珠的 Y 軸位置，僅在 `珍珠 Y 軸運動模式` 設為 `完美水平抛射` 時顯示                                          |
| `珍珠 Y 軸位置（調整後）`      | double | 170.0              | 放置用來取消 Y 軸運動的活塞，並在時間結束時獲得珍珠的 Y 軸位置，僅在 `珍珠 Y 軸運動模式` 設為 `完美水平抛射` 時顯示                                         |
| `預設紅色 TNT 位置`        | enum   | SouthEast          | 預設紅色陣列 TNT 位置，相對於珍珠的位置，沒有被史萊姆方塊更改                                                                          |
| `預設藍色 TNT 位置`        | enum   | NorthWest          | 預設藍色陣列 TNT 位置，相對於珍珠的位置，沒有被史萊姆方塊更改                                                                          |

### 設置珍珠與目標點信息

第二步，您需要設置珍珠與 TNT/目標點的信息。前往 `一般設定`。您也可以通過點擊 `開啟設定檔` 來導入設置。

以下是珍珠與目標點的信息設置。

| 名稱        | 值類型    | 預設值   | 註釋                                                                                                 |
|-----------|--------|-------|----------------------------------------------------------------------------------------------------|
| `珍珠X座標`   | double | 0     | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` 使用 `/tick freeze` 和 `/tick step 1` 可能幫助您 |
| `珍珠Z座標`   | double | 0     | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` 使用 `/tick freeze` 和 `/tick step 1` 可能幫助您 |
| `目標點X座標`  | double | 0     | 目標點的 X 座標                                                                                          |
| `目標點Z座標`  | double | 0     | 目標點的 Z 座標                                                                                          |
| `最大TNT數量` | int    | 0     | 每側 FTL 的最大 TNT 數量                                                                                  |
| `方向`      | enum   | North | 這將影響珍珠模擬的方向，點擊 TNT 數量輸出後會自動設置                                                                      |
| `紅色TNT陣列` | int    | 0     | 這是存儲在 FTL ROM 中的紅色 TNT 數量，點擊 TNT 數量輸出後會自動設置                                                        |
| `藍色TNT陣列` | int    | 0     | 這是存儲在 FTL ROM 中的藍色 TNT 數量，點擊 TNT 數量輸出後會自動設置                                                        |

設置目標點信息後，您可以點擊 `計算TNT數量` 來輸出結果列表。

您可以從側邊欄選擇輸出結果之一，將自動填入 `方向`、`紅色TNT陣列` 和 `藍色TNT陣列`。

### 設置輸出

順序

接下來，我們可以設置輸出順序。前往 `進階設定`。

以下是輸出順序設置。

| 名稱        | 值類型    | 預設值        | 註釋                    |
|-----------|--------|------------|-----------------------|
| `X`       | double | 0 (-1<X<1) | 珍珠 X 座標與熔岩池中心座標之間的偏移量 |
| `Z`       | double | 0 (-1<Z<1) | 珍珠 Z 座標與熔岩池中心座標之間的偏移量 |
| `TNT最大數量` | int    | 0          | 輸出順序                  |
| `結果排序控制`  | enum   | 混合加權距離     | 輸出順序，開啟圖表模式可以直觀地查看結果  |

點擊您想要的數量輸出。

最後，點擊 `珍珠模擬軌跡` 查看珍珠飛行路徑。

### 額外工具

您可以前往 `TNT編碼` 頁面將 TNT 數量編碼至 FTL ROM。

| 名稱          | 值類型    | 預設值 | 註釋                                                                    |
|-------------|--------|-----|-----------------------------------------------------------------------|
| `紅色TNT編碼配置` | string |     | 輸入框中用 `,` 分隔數字，範例：`1, 2, 4, 8, 10, 20, 40, 80, 160`，會轉換為 `int[]` 進行計算 |
| `藍色TNT編碼配置` | string |     | 輸入框中用 `,` 分隔數字，範例：`1, 2, 4, 8, 10, 20, 40, 80, 160`，會轉換為 `int[]` 進行計算 |

點擊 `計算 TNT 編碼組合` 獲取結果。

### 導出與導入

您可以通過點擊 `儲存設定檔` 和 `開啟設定檔` 進行設置的導出與導入，位於 `一般設定` 頁面下。

### 圖形輸出

您可以點擊 `圖表模式` 查看圖形輸出。

**這是仍在開發中的新功能。歡迎提出任何建議。**

## 切換炮台設置

頂部可以看到具有炮台名稱的切換圖標。點擊它可以切換炮台設置。

在編輯側邊欄，您可以輕鬆執行以下操作：

- 更改炮台名稱
- 刪除炮台
- 添加新炮台
- 重置炮台
- 複製炮台設置

**這是仍在開發中的新功能。歡迎提出任何建議。**

### SVG 圖標引用

- [Moon Dark Theme SVG Vector](https://www.svgrepo.com/svg/304625/moon-dark-theme): 授權 CC0
- [Sun Light Theme SVG Vector](https://www.svgrepo.com/svg/304624/sun-light-theme): 授權 CC0
- [Coordinates SVG Vector](https://www.svgrepo.com/svg/95272/coordinates): 授權 CC0
