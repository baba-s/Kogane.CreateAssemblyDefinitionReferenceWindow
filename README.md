# Kogane Create Assembly Definition Reference Window

Assembly Definition Reference を作成する時のコンパイル回数を抑えられるエディタ拡張

## 使い方

![2022-10-01_110505](https://user-images.githubusercontent.com/6134875/193378926-fa11d2d4-6001-40a3-8eea-06f14f5c3210.png)

Project ウィンドウを右クリックして「Kogane > Create Assembly Definition Reference」を選択して

![2022-10-01_110555](https://user-images.githubusercontent.com/6134875/193378930-831c518e-cda6-4996-a0dc-a55cb9d99566.png)

表示されたウィンドウで Assembly Definition Reference を作成することができます

通常の手順で Assembly Definition Reference を作成して設定を変更する場合

1. Assembly Definition Reference を作成
2. コンパイル
3. Assembly Definition Reference の設定を変更
4. コンパイル

と2回コンパイルが走りますが、本パッケージを使用する場合は  
最初から設定が反映された状態の Assembly Definition Reference を作成できるため  
コンパイルの回数を1回に抑えられます

## 依存しているパッケージ

* https://github.com/baba-s/Kogane.JsonAssemblyDefinitionReference