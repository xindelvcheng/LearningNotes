import requests

sess = requests.session()
login_url = "https://www.pixiv.net/"
data = {
    "pixiv_id": "2604627950@qq.com",
    "password": "123456789"
}
headers = {
    "user-agent": "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Mobile Safari/537.36"
}
proxies = {
    "http": "localhost:1080",
    "https": "localhost:1080"
}
r = sess.get(login_url,headers=headers, proxies=proxies)
with open("pixiv ranking.html", "w", encoding="utf8") as f:
    f.write(r.content.decode())
