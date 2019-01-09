from flask import Flask,request

app = Flask(__name__)

@app.route("/",methods=["POST","GET"])
def index():
    name = request.form.get("name")
    return "Hello,{}".format(name)

if __name__ == "__main__":
    app.run()