from flask import Flask,make_response,request

app = Flask(__name__)

@app.route("/set_cookie")
def set_cookie():
    resp = make_response("success")
    resp.set_cookie("id","haha")
    resp.set_cookie("age","20",max_age=3600) # 秒
    return resp
    
@app.route("/get_cookie")
def get_cookie():
    return request.cookies.get("id")

if __name__ == "__main__":
    app.run()