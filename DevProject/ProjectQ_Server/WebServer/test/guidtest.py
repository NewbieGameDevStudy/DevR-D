import json
import csv

CSV_FILENAME = 'services.csv'
JSON_FILENAME = 'services.json'
COLUMNS = ('����', '��������', '���� ����', '��������', '��������', '�Ұ����', '�Ұ���� ����ó', '���ɺо�', '��������', '�������', '�����ڰ�', '��������', '�ߺ��Ұ� ����', '��û�ʿ俩��', '�¶��ν�û���ɿ���', 'ó������', '��û����', '���񼭷�', '��û����', '�������', '������� ����ó', 'ó�����', 'ó����� ����ó', '����ó', '������ȭ��ȣ', '������Ʈ')

def convert():
    csv_reader = open(CSV_FILENAME, 'r')
    json_writer = open(JSON_FILENAME, 'w')
    
    services = csv.DictReader(csv_reader, COLUMNS)
    json_writer.write(json.dumps([row for row in services]))


if __name__ == '__main__':
    convert()