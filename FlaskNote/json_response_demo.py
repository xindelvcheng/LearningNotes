from flask import Flask,session

app = Flask(__name__)

@app.route("/login")
def login():
    session["username"] = "zhangsan"
    session["age"] = 20
    return "login seccess"

@app.route("/index")
def index():
    username = session.get("username")
    age = session.get("age")
    return "Hello,{},age:{}".format(username,age)

if __name__ == "__main__":
    app.run()