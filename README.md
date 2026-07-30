## FlappyBird

[English](#english) | [中文](#中文)

---

# English

### Resource Method

Developed with the AssetBundle approach.

### About

A reinterpretation of the FlappyBird mini-game built on OxGFrame. Everything is developed within a single scene, which keeps the logic and the game phases distinct and easy to follow.

### Framework

[OxGFrame](https://github.com/michael811125/OxGFrame)

### How It Works

The core of the main program is CoreSystem. Everything is driven by CoreSystem, which in turn drives GSIManager through the MonoBehaviour characteristics of CoreSystem.

**This demo is a mini-game that does not involve much control (apart from fetching leaderboard data over the network), so it is fairly simple to structure. Scoring is therefore handed directly to CoreSystem to mediate. In a larger commercial game, each controller would be split out so that CoreSystem stays clean and acts only as the core driver and mediator.**

### Setup

Set the Game view to W: 1080 * H: 1920, then open the Main.unity scene and hit Play.

---

### Unity Version

Unity 2022.3.59f1 (LTS) or higher is recommended - [Unity Download](https://unity3d.com/get-unity/download/archive)

---

### Reference

[LordZed400/Flappy-Bird-Unity](https://github.com/LordZed400/Flappy-Bird-Unity)

---

# 中文

### 資源方式

使用 AssetBundle 方式進行開發。

### 專案說明

基於 OxGFrame 重新詮釋 FlappyBird 小遊戲，使用單一場景進行開發，讓邏輯與遊戲階段更為鮮明。

### 專案框架

[OxGFrame](https://github.com/michael811125/OxGFrame)

### 運作說明

主程序核心為 CoreSystem，皆由 CoreSystem 進行核心驅動，藉由 CoreSystem MonoBehaviour 特性驅動 GSIManager。

**此 Demo 是小遊戲在控制上不多 (撇除用到排行榜撈連線數據)，在建構上會比較簡便，所以直接將計分交由 CoreSystem 進行中介控制，實際上在更大規模的商業遊戲開發上，就會再把各控制拆分出去，盡量讓 CoreSystem 保持乾淨僅作為核心驅動與中介而已。**

### 配置說明

設定 Game 視窗為 W: 1080 * H: 1920，再開啟 Main.unity 場景直接 Play。

---

### Unity 版本

建議使用 Unity 2022.3.59f1(LTS) or higher 版本 - [Unity Download](https://unity3d.com/get-unity/download/archive)

---

### 參考

[LordZed400/Flappy-Bird-Unity](https://github.com/LordZed400/Flappy-Bird-Unity)
