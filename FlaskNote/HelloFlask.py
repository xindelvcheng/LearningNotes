# coding:utf-8
# python2中需要指定编码

from flask import Flask

# 创建flask应用核心
# __name__表示当前模块名（本文件自己就是启动文件，所以填__main__也可以）
# flask默认以这个模块所在的目录为总目录，其下static静态文件目录，templates为模板目录
app = Flask(__name__)
app.config.from_pyfile("config.cfg")

@app.route("/")
def index():
    # 定义视图函数,和java好像啊！
    return "<h1>Hello,Flask!</h1>"

if __name__=="__main__":
    # 这里相当于spingboot中的启动，使用的是flask内置的测试服务器,整个py文件当做普通脚本运行即可
    app.run(host="0.0.0.0",port=80,debug=True)

