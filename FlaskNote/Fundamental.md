## 创建flask应用
```
# 创建flask应用核心,传入项目总目录所在项目名，若不存在则默认为当前模块（文件）
app = Flask(__name__)

# 定义视图函数,和java好像啊！
@app.route("/")
def index():
    return "<h1>Hello,Flask!</h1>"

# 用flask内置的测试服务器运行
app.run(app.run(host="0.0.0.0",port=80,debug=True)) # host指定为"0.0.0.0"表示任何表示主机的ip地址都可以访问，port指定端口

# 像普通脚本一样运行
$ python HelloFlask.py
```
## 访问静态文件
1.创建文件static/index.html  
2.开启服务器，访问url : http://127.0.0.1:5000/static/index.html

## 路径设置
```
app = Flask(__name__,
            static_url_path="\static", # 访问静态资源的前缀默认是static
            static_folder="static", # 静态资源所在目录
            template_folder="templates" # 模板目录
            )
```
>关于静态资源前缀:当请求的url以"static"开头时认为其是访问静态资源而非路由，当访问的url为"/static/index.html"时，url被认为是访问静态资源，在静态资源文件夹中寻找文件名为"index.html"的文件。即"/static/"是一个标识而非路径的一部分。

## Flask配置
### 1.使用配置文件（和springboot中相似）
（1）创建config.cfg
```
DEBUG=True
```
（2）读取配置文件
```
app.config.from_object("config.cfg")
```
### 2.使用类属性配置（和springboot中相似）
（1）创建配置类并添加类属性
```
class config():
    DEBUG=True
```
（2）指定配置类
```
app.config.from_object(config)
```
### 4.直接操作config
```
# 把app.config当做一个字典操作（也可以通过这种方式从config中取值获取配置参数）
app.config["DEBUG"] = True
```
## 全局访问Flask应用核心
```
from flask import current_app
```
>怎么使用app，就怎么使用current_app，如current_app.config.get("DEBUG")
## 路由信息
### 1.打印所有路由
```
print(app.url_map)
```
### 2.指定请求方式
-   在装饰器上指定参数
```
@app.route("/url",method=["GET"])
def get_only():
    pass
@app.route("/url",method=["POST"])
def post_only():
    pass
```
>如果请求方式不加限制，同名的视图函数只会访问到先注册的那个
-   多个url对应一个视图函数
```
@app.route("/")
@app.route("/index")
def index():
    return "hi!"
```
-   重定向
```
from flask import Flask,redirect,url_for
@app.route("/index")
def login():
    # 通过url_for()找到视图函数"index"对应的url，不直接写url是为了解耦
    redirect(url_for("index"))
    return "hi!"
```
### 2.转换器
转换器从url中提取数据，flask实现了int、float、path等类型，如果指定提取int使用`int:good_id`这样的方式命名参数，直接写名字则默认为字符串规则（除了“/"外的任意字符，需要将变量的名字传入函数中。
```
@app.route("/good/<good_id>")
def good_detail(good_id):
    return "{} detail page".format(good_id)

@app.route("/good/<int:good_id>")
def good_detail(good_id):
    return "{} detail page".format(good_id)
```

## 获取请求参数
导入request作为全局对象,包含了所有请求信息
### 提取表单数据
通过`request.form`，它是一个类字典
```
from flask import request

@app.route("/")
def index():
    name = request.form.get("name")
    return "Hello,{}".format(name)
```
>字典的存取可以使用`[key]`和`get(key)`，但如果没有这个key使用`[key]`会报错而`get(key)`返回None，为了程序的健壮性应使用`get(key)`的方式
### 提取请求体数据
```
data = request.data
```
>如果前端发送的是一个form表单数据，通过`request.form`获取，而`request.data`为空；若前段发送的是一个JSON格式的字符串，通过`request.data`获取，而`request.form`为空
### 获取url中的参数
```
data = request.args("kw")
```
>args也是一个类字典对象。另外，参数名可能发生重复，可以通过`reqeust.form.getlist("name")`来获取同名的参数值列表
### 获取其他数据
```
# 获取请求头
headers = request.headers
# 获取请求方式
method = request.method
# 获取用户上传的文件
files = request.files
```
>同样是类字典对象

### 异常处理
```
from flask import abort

# 返回一个标准http状态码，flask会返回的错误页面
abort(403)

# 返回信息
abort(Response("Error"))

# 定义状态码返回的错误信息
@app.errorhandler(404)
def http_404_error(err):
    return "<h1>访问的页面不存在<h1><br>{}".format(err)
```
>如果要自定义响应头，可以返回`return "Test",400,[("key","value"),("Connection",Keep-Alive")]`这样的元祖（或{"Connection":"Keep-Alive",key:value}）。也可以传自定义的状态码，如"666 CustomCode";或`resp = make_response()`然后对响应体对象resp进行设置

## 返回JSON
```
from flask import jsonify

@app.route("/login",methods=["POST","GET"])
def index():
    return jsonify(key1="value1",key2="value2") #或return jsonify(dict)
```

## Cookie
### 设置Cookie
```
from flask import Flask,make_response

@app.route("/set_cookie")
def set_cookie():
    resp = make_response("success")
    resp.set_cookie("id","haha")
    resp.set_cookie("age","20",max_age=3600) # 秒
    return resp
```
>cookie有有效期，默认为至关闭浏览器。设置cookie实际是浏览器发现响应头中有Set-Cookie字段，便将其保存到本地
### 获取Cookie
```
request.cookies.get("id")
```
### 删除Cookie（通过设置有效期为创建时间）
```
resp.delete_cookie("id")
```

## Session
### 设置session
```
from flask import Flask,session

@app.route("/login")
def login():
    session["username"] = "zhangsan"
    session["age"] = 20
    return "login seccess"
```
>session也是一个全局类字典对象
### 获取session

## 模板
模板默认应该放在主目录下的templates文件夹下
在html中：
```
<p>name = {{name}}</p>
<p>age = {{age}}</p>
<p>age = {{data["id"]}}</p>
<p>age = {{data.id}}</p>
<p>age = {{data[0]}}</p>
```

脚本中：
@app.route("/index",methods=["POST","GET"])
@app.route("/",methods=["POST","GET"])
def index():
    return render_template("index.html",
    name="zhangsan",
    age=20,
    data = {"id":1000,"address":"china"},
    fav = ["cs","game"]
    )
```
>也可以传入一个`**data_dict`
### 过滤器
使用装饰器
```
<p>{{"   hello  " | trim | upper}}</p>
```
自定义装饰器
```
@app.template_filter("filter_name")
def fileter(in_str):
    out_str = in_str
    return out_str
```
