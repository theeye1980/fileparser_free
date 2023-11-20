import urllib.parse
import urllib.request

url = "https://ms.fandeco.ru/rest/bitrix24/chat/send"
params = {"user": "chat53651", "msg": "https://ms.fandeco.ru/rest/bitrix24/chat/send?user=chat53651&msg=FileParser обновлен. Его можно скачать из папки КМ на 5.188.156.224"}
encoded_params = urllib.parse.urlencode(params)
full_url = f"{url}?{encoded_params}"
response = urllib.request.urlopen(full_url)
