from flask import Flask,request,abort

app = Flask(__name__)

@app.route("/",methods=["POST","GET"])
def index():
    file_obj = request.files.get("pic")
    if file_obj==None:
        return "未上传文件"

    file_obj.save("./pic.png")
    return "upload success"

if __name__ == "__main__":
    app.run()