# 2018年10月9日19:47:19

# 抓取猫眼电影排行

###### 基本步骤 ######
# 1、安装requests库
# 2、确定抓取目标URL    http://maoyan.com/board/4
# 3、确定URL中分页参数及其偏移量    offset = n 时，显示的电影序号是 n + 1 ~ n + 10
# 4、抓取首页源码
# 5、正则提取所需信息   从network选项卡下处理源码。此处的源码经过JavaScript处理，可能与原始请求会有所不同
# 6、处理提取到的信息
# 7、写入本地文件
# 8、重构代码，完成分页爬取
######################

# 导入模块
import requests
import json
import re
import time
from requests.exceptions import RequestException

# 抓取首页源码
def get_one_page(url):
    try:
        # 伪造请求头
        headers = {'User-Agent':'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36'}
        # 发送请求
        reponse = requests.get(url, headers = headers)
        # 判断请求是否成功
        if reponse.status_code == 200:
            # 返回源代码
            return reponse.text
        return None
    except RequestException:
        return None

# 正则提取首页信息
def parse_one_page(html):
    # 定义正则匹配规则
    pattern = re.compile(r'<dd>.*?board-index.*?>(.*?)</i>.*?data-src="(.*?)".*?name.*?a.*?>(.*?)</a>.*?star.*?>(.*?)</p>.*?releasetime.*?>(.*?)</p>.*?integer.*?>(.*?)</i>.*?fraction.*?>(.*?)</i>.*?</dd>', re.S)
    # 匹配所有
    items = re.findall(pattern, html)
    # print(items)
    # 整理匹配结果格式 --> 字典
    for item in items:
        yield{
            'index':item[0],
            'image':item[1],
            'title':item[2].strip(),
            'actor':item[3].strip()[3:] if len(item[3]) > 3 else '',
            'time':item[4].strip()[5:] if len(item[4]) > 5 else '',
            'score':item[5].strip() + item[6].strip()
        }

# 写入本地文件
def write_to_file(content):
    with open('{}.txt'.format(time.strftime(r'%Y%m%d', time.localtime())), 'a', encoding='utf-8') as f:
        print(type(json.dumps(content)))
        f.write(json.dumps(content, ensure_ascii=False) + '\n')


# 分页爬取 获取多页内容
def main(offset):
    url = 'http://maoyan.com/board/4?offset=' + str(offset)
    html = get_one_page(url)
    for i in parse_one_page(html):
        write_to_file(i)

# 临时方法 用于测试执行
def test():
    # 指定URL
    url = 'http://maoyan.com/board/4'
    html = get_one_page(url)
    # print(html)
    for i in parse_one_page(html):
        # print(i)
        write_to_file(i)

# 程序主方法
if __name__ == '__main__':
    for i in range(10):
        main(offset = i * 10)
        # 防止请求次数过快被反爬机制拒绝
        time.sleep(1)