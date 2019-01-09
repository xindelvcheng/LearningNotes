from flask import Flask,render_template

app = Flask(__name__)

@app.route("/index",methods=["POST","GET"])
@app.route("/",methods=["POST","GET"])
def index():
    return render_template("index.html",
    name="zhangsan",
    age=20,
    data = {"id":1000,"address":"china"},
    fav = ["cs","game"]
    )



if __name__ == "__main__":
    app.run()