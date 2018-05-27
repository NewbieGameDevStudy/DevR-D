import json
import csv

CSV_FILENAME = 'services.csv'
JSON_FILENAME = 'services.json'
COLUMNS = ('제목', '지원내용', '서비스 목적', '시행일자', '종료일자', '소관기관', '소관기관 연락처', '관심분야', '지원형태', '지원대상', '수급자격', '선정기준', '중복불가 서비스', '신청필요여부', '온라인신청가능여부', '처리기한', '신청절차', '구비서류', '신청기한', '접수기관', '접수기관 연락처', '처리기관', '처리기관 연락처', '문의처', '문의전화번호', '웹사이트')

def convert():
    csv_reader = open(CSV_FILENAME, 'r')
    json_writer = open(JSON_FILENAME, 'w')
    
    services = csv.DictReader(csv_reader, COLUMNS)
    json_writer.write(json.dumps([row for row in services]))


if __name__ == '__main__':
    convert()