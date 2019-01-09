-   Django需要和uWSGI配合，就像Sping需要和Tomcat配合。框架和服务器是不同的，服务器负责接受请求和解析报文，框架来分发路由，执行响应函数，服务器接到返回值之后再打包成HTTP报文通过TCP链接传回去
-   虚拟环境
```
# 安装
pip install virtualenv
pip install virtualenvwrapper

# 创建虚拟环境
mkvirtualenv flask-py2
```