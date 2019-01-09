### 服务器启动jupyter
```
jupyter notebook --no-browser --allow-root
```
>注意防火墙要开放8888端口
-   
### 防止ssh长时间不操作断开连接jupyter被kill
```
$ yum install screen
$ screen
$ Ctrl+a d
$ screen -ls
$ screen -r id 
```