## 一. 项目目录结构
-   AndroidManifest.xml
安卓的清单文件，定义四大组件
-   src
源代码
-   gen
自动生成文件
    -   R.java
    自动生成的文件，包含id，用于索引
        ```
        //在*.java中索引
        EditText num_edit = (EditText) findViewById(R.id.editText1);
        //在*.xml中索引,@代表R文件
        @drawable/ic_launcher
        ```
-   assets
资源文件，不会打包
-   bin
打包完成的文件，包括apk文件和dex文件
-   res
资源文件，打包并生成R文件内容
    -   drawable-hdpi等
    图片资源
    -   layout
    布局资源
    -   values字符串
## 二. 入门案例：电话拨号器
>就像Qt中所有控件都是Widget，安卓中所有控件都是View
###   按钮点击事件
1.匿名内部类
```
call_button.setOnClickListener(new OnClickListener() {
    @Override
    public void onClick(View v) {
    }
}
```
2.让MainActivity实现OnclickListener重写方法，传入参数this：`call_button.setOnClickListener(this);`。当界面中有多个同功能按钮比较方便  
3.像xaml一样添加
```
//activity_main.xml中添加
android:onClick="click"
...
//MainActivity.java中添加
public void click(View v){
        ...
    }
```
>click的签名要注意。它是反射实现的（`getMethod(handlerName,View.class)`），所以参数要写View v
-   使用Intent实现拨号功能
```
Intent intent = new Intent();
intent.setAction(Intent.ACTION_CALL);
intent.setData(Uri.parse("tel:"+number));
startActivity(intent);
```
###   添加权限
在AndroidManifest.xml中添加`<uses-permission android:name="android.permission.CALL_PHONE"/>`
###   Toast  
显示少量信息的提示
    >如何看文档？1. 看类的简介 2.看类的构造方法 3. 看类的方法
    ```
    Toast.makeText(MainActivity.this, "号码不能为空", Toast.LENGTH_LONG).show();
    ```
    >mskText()为静态方法，直接调用；不能忘记show();
## 布局
在layout文件夹下新建布局文件，新建AndroidXmlFile选择RootElemnt为LinearLayout
### LinearLayout
线性布局,相当于panelstack之类的
### RelativeLayout
相对布局
默认的布局，用id指定新的组件和原有组件位置关系(在某个组件的右边、下边等)
```
<EditText
        android:id="@+id/editText1"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:layout_alignLeft="@+id/textView1"
        android:layout_below="@+id/textView1"
        android:ems="10" />

    <Button
        android:id="@+id/button1"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:layout_alignRight="@+id/textView1"
        android:layout_below="@+id/editText1"
        android:layout_marginTop="17dp"
        android:text="拨打" />
```
>这两个组件是安卓开发用的较多的
### FrameLayout
一层层显示，比如播放视频窗口的播放按钮
### TableLayout
表格布局，相当于Qt的网格布局，不过安卓开发用的少,在TableRow标签中定义组件
### AbsoluteLayout
绝对布局，Qt，WPF，安卓中都用的少
### 通用属性
-   android:layout_gravity="center"
-   android:layout_margin="10dp"
外边距
-   android:padding="10dp"
内边距
## 安卓中的单位
-  dp 常用
-  px 像素，基本不用
-  sp 字体
>AndroidStudio快捷键Ctrl+Alt+F抽取局部变量
