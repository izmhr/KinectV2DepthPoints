## これは

Unity で Kinect V2 の Depth 画像を、点群として見るソフトです

Unity 2017.3 から、Mesh が 32bit index buffer をサポートしました。 [(リリースノート)](https://unity3d.com/jp/unity/beta/unity2017.3.0b10) これにより、65536頂点以上の頂点を持つ Mesh オブジェクトを扱えるようになっています。

### できること

- Kinect V2 の Depth 画像を、点群で表示します

### できないこと

- CameraSpace に展開していません(Depth の画像に馬鹿正直に奥行きをつけています)

### リファレンス

先に keijiro さんがこの機能を利用して、PCLのデータをインポートするエディタ拡張を作られていて、これが出来るなら Kinect V2 のデプスデータも似たような感じで扱えるのでは、と思って、参考にしながら作ってみました。

- [keijiro/Pcx](https://github.com/keijiro/Pcx)

## 実行

### 環境

- Windows 10 x64
- Unity 2017.3b10 (2017/12/1現在)
- Visual Studio Community 2017 (これは依存してないと思う)
- KinectSDK-v2.0_1409

### 必要なアセット

- [Kinect for Windows SDK 2.0 Unity Pro Add-in](http://go.microsoft.com/fwlink/?LinkID=513177)
  - 公式の配布ページがわからないけど直リンク。

### 手順

- あらかじめ KinectSDK を入れて、標準のビューワでデータが見られることを確認しておく
- 本リポジトリをDLして、Unityで開く
- Kinect for Windows SDK 2.0 Unity Pro Add-in の .unitypackage を展開する
- Main.unity シーンを開く
- 実行する