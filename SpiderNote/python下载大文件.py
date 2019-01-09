import requests

url = 'http://download.tensorflow.org/models/image/imagenet/inception-2015-12-05.tgz'
headers = {"User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Safari/537.36"}
proxies = {"http":"localhost:1080","https":"localhost:1080"}
r = requests.get(url,headers=headers,stream = True,proxies=proxies)
print("Start download...")
with open("./test","wb") as f:
    for chunk in r.iter_content(chunk_size=1024):
        if chunk:
            f.write(chunk)
print("Finish")