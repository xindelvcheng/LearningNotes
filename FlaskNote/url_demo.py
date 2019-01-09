from flask import Flask

app = Flask(__name__)

@app.route("/")
def index():
    return "<h1>Hello,Flask!</h1>"

@app.route("/good/<good_id>")
def good_detail(good_id):
    return "{} detail page".format(good_id)

if __name__=="__main__":
    app.run(host="0.0.0.0",port=80)