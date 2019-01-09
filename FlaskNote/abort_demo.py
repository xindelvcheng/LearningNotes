from flask import Flask,request,abort,Response

app = Flask(__name__)

@app.route("/login",methods=["POST","GET"])
def index():
    abort(Response("Hello"))
    return "upload success"

@app.errorhandler(404)
def http_404_error(err):
    return "<h1>访问的页面不存在<h1><br>{}".format(err)

if __name__ == "__main__":
    app.run()