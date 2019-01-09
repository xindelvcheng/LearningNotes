# Android中本地数据的保存和读取
## 一.使用io
### Context上下文
代表应用环境，包含google提供的各种api，如startActivity()，getFileDir()
### 写入
```
this.openFileOutput("info.txt",0)
// 相当于
FileOutputStream fos = new FileOutputStream(new File("/data/data/com.xindelvcheng.helloandroid/info.txt"));
fos.write(result.getBytes());
```
### 读取
```
FileInputStream fis = context.openFileInput("data.txt");
BufferedReader br = new BufferedReader(new InputStreamReader(fis));
String info = br.readLine();
```

## 二.使用SharedPreferences(对于简单任务很方便)
```
# 获取SharedPreferences对象
SharedPreferences settings = getSharedPreferences("profile",MODE_PRIVATE);

# 保存数据
SharedPreferences.Editor edit = settings.edit();
edit.putBoolean("save_passwd",true);

# 读取数据
cb.setChecked(settings.getBoolean("save_passwd",false));
```
## 三.数据库
### 创建数据库
```
// 继承SQLiteOpenHelper
public class MySqLiteOpenHelp extends SQLiteOpenHelper {
    public MySqLiteOpenHelp(Context context) {
        //上下文对象，数据库，null，Sqlite版本（只能升不能降）
        super(context, "test.db", null, 1);
    }

    /**
     * 当数据库被创建时调用，可以用与表结构的初始化
     * @param db
     */
    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL("create table info(_id integer primary key autoincrement,name varchar(20))");
    }

    /**
     * 当数据库版本升级时被调用，可用于表结构的更新
     * @param db
     * @param oldVersion
     * @param newVersion
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }
}

//创建或打开数据库
MySqLiteOpenHelp mySqLiteOpenHelp = new MySqLiteOpenHelp(getApplicationContext());
// getReadableDatabase()和getWritableDatabase()都创建或打开一个数据库，并非一个只读一个只写
SQLiteDatabase writableDatabase = mySqLiteOpenHelp.getWritableDatabase();
```
### 执行sql语句
```
db.execSQL("select * from tab");
```
### 查询
```
// 获取结果集
Cursor cursor = database.rawQuery("select * from info", null);
// 获取记录数
cursor.getCount()
// 获取游标当前指向记录的值
String id = cursor.getString(0);
String name = cursor.getString(1);
//将游标下移，返回boolean值
while (cursor.moveToNext()) {
    String name = cursor.getString(1);
    System.out.println(name);
}
```
### 使用adb和sqlite3
在F:\Repository\Android\Sdk\platform-tools下
```
$ adb shell
$ su root
$ cd /data/data/com.example.dbdemo/
$ cd databases/
$ sqlite3 test.db
sqlite> insert into info(name) values("zhangsan");
sqlite> select * from info;
1|zhangsan
2|lisi
3|wangwu
4|zhangsan
5|lisi
6|wangwu
7|zhangsan
8|zhangsan
9|zhangsan
```
>改变CMD编码方式
```
//改变成GBK
$ chcp 936
//改变成UTF-8
$ chcp 65001
```

### 使用Android提供的API对数据库进行CRUD
1.插入数据
```
ContentValues values = new ContentValues();
values.put("name","haha");
long row_id = database.insert("info", null, values);//底层在拼接sql语句，返回值为新行的id
```
2.删除数据
```
database.delete("info", "name=?", new String[]{"zhangsan"});//返回值为影响的行数
```
3.更新数据
```
values.put("name", "haha");
database.update("info", values, "name=?", new String[]{"zhangsan"});
```
4.查询数据
```
Cursor cursor = database.query("info", null, "name=?", new String[]{"zhangsan"}, null, null, null);
```
### 事务
```
//开启事务
database.beginTransaction();
//如果执行到这句话事务提交，否则回滚
database.setTransactionSuccessful();
//关闭事务
database.endTransaction();
```