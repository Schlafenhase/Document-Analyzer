FROM python:3.8

WORKDIR /app

RUN pip3 install numpy
RUN pip3 install scipy
RUN pip3 install scikit-learn==0.19.2
RUN pip3 install pika
RUN pip3 install pymongo
RUN pip3 install pandas

COPY . .

CMD [ "python3", "SwearAnalyzer.py"]