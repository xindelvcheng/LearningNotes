import cv2
import numpy as np
import os

src_name = input("Source image\t:")

img = cv2.imread(src_name)
src = cv2.imread(src_name)

# # 缩放图片到720p
# img = cv2.resize(img, (1280, 720))
# src = cv2.resize(src, (1280, 720))
width = img.shape[1]
height = img.shape[0]

# 切掉边框
x_cut = 0.05
y_cut = 0.07
img = img[int(width * x_cut):-int(width * x_cut), int(height * y_cut):-int(height * y_cut)]
src = src[int(width * x_cut):-int(width * x_cut), int(height * y_cut):-int(height * y_cut)]

# 转化为灰度图像
imgray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)

# 中值滤波
# imgray = cv2.medianBlur(imgray, 5)

# 双边滤波
imgray = cv2.bilateralFilter(imgray, 9, 75, 75)

# 二值化
ret, thresh = cv2.threshold(imgray, 127, 255, 0)

# Canny 边缘检测代替二值化
# thresh=cv2.Canny(img,100,200)


# 获取外轮廓，精简，改变原图
# cv2.CHAIN_APPROX_NONE 不精简
# cv2.RETR_EXTERNAL 只提取外轮廓
# cv2.RETR_LIST 提取所有
# cv2.RETR_CCOMP 表示提取所有轮廓并将组织成一个两层结构，其中顶层轮廓是外部轮廓，第二层轮廓是“洞”的轮廓
# cv2.RETR_TREE 提取所有轮廓并组织成轮廓嵌套的完整层级结构
image, contours, hierarchy = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

# 按面积排序
areas = np.zeros([len(contours)])
index = 0  # 面积的下标指针

# 创建存储轮廓点的文件夹
if not os.path.exists("contours"):
    os.mkdir("contours")
# 创建存储轮廓数据的文件夹
if not os.path.exists("info"):
    os.mkdir("info")
# 创建存储图片的文件夹
if not os.path.exists("img"):
    os.mkdir("img")

# 遍历轮廓，计算每个轮廓的面积
for contour in contours:
    areas[index] = cv2.contourArea(contour)
    index = index + 1
# 对轮廓按面积排序，sortIdx能够得到这些值在原数组中的序号
areas_s = cv2.sortIdx(areas, cv2.SORT_DESCENDING + cv2.SORT_EVERY_COLUMN)
(b8, g8, r8) = cv2.split(src)
# print(contours)
# contours是一个[
#   [   [1,1] ， [1,2]  ],
#   [   [2,1] ， [2,2]  ],
# ]这样的列表，第一维是ndarray类型轮廓的集合，第二维是点的集合

# 对每个区域进行处理
for index in areas_s:
    if areas[index] < width*height*0.001:
        continue
    m = cv2.moments(contours[index.tolist()[0]])
    cx = int(m['m10'] / m['m00'])
    cy = int(m['m01'] / m['m00'])
    with open('info/split_' + str(index) + '_info.csv', 'w') as f:
        f.write(str(cx) + ',' + str(cy) + '\n')

    # 获得轮廓的边缘点
    np.savetxt('.\\contours\\split_' + str(index) + '_contour.csv', (contours[index.tolist()[0]]).reshape((-1, 2)),
               fmt='%d',
               delimiter=',')
    # 绘制区域图像，通过将thickness设置为-1可以填充整个区域，否则只绘制边缘
    temp_img = np.zeros(imgray.shape, dtype=np.uint8)
    cv2.drawContours(temp_img, contours, index, [255, 255, 255], -1)
    # 结合灰度图掩膜
    # temp_img = temp_img & imgray  

    # 得到彩色的图像
    color_img = cv2.merge([b8 & temp_img, g8 & temp_img, r8 & temp_img])

    # cv2.imshow('temp_img\t'+str(index), color_img)
    cv2.imwrite('img/split_' + str(index) + '.png', color_img)
cv2.waitKey(0)
