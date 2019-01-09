import cv2
import numpy as np

src_name = input("Source image\t:")

img = cv2.imread(src_name)
src = cv2.imread(src_name)

# 缩放图片到720p
img = cv2.resize(img, (1280, 720))
src = cv2.resize(src, (1280, 720))

# 切掉边框
img = img[50:-50, 100:-100]
src = src[50:-50, 100:-100]

imgray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)

# 中值滤波
imgray = cv2.medianBlur(imgray, 5)

# 双边滤波
# imgray=cv2.bilateralFilter(imgray,9,75,75)

# 二值化
ret, thresh = cv2.threshold(imgray, 127, 255, 0)

# 获取外轮廓，不精简，改变原图
image, contours, hierarchy = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)

# 按面积排序
areas = np.zeros([len(contours)])
index = 0  # 面积的下标指针

for contour in contours:
    areas[index] = cv2.contourArea(contour)  # 计算每个轮廓的面积
    index = index + 1
# sortIdx能够得到这些值在原数组中的序号
areas_s = cv2.sortIdx(areas, cv2.SORT_DESCENDING + cv2.SORT_EVERY_COLUMN)
(b8, g8, r8) = cv2.split(src)

# 对每个区域进行处理
for index in areas_s:
    if areas[index] < 100:
        continue
    # 绘制区域图像，通过将thickness设置为-1可以填充整个区域，否则只绘制边缘
    temp_img = np.zeros(imgray.shape, dtype=np.uint8)
    cv2.drawContours(temp_img, contours, index, [255, 255, 255], -1)
    # 结合灰度图掩膜
    # temp_img = temp_img & imgray  

    # 得到彩色的图像
    color_img = cv2.merge([b8 & temp_img, g8 & temp_img, r8 & temp_img])

    # cv2.imshow('temp_img\t'+str(index), color_img)
    cv2.imwrite('split_' + str(index) + '.png', color_img)
cv2.waitKey(0)
