import socket

address = "localhost"
port = 50000
buff = 5
size = 1024

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((address, port))
s.listen(buff)

while 1:
    client, address = s.accept()
    print "Client connected."
    client.send("connection received\n")
    while 1:
        data = client.recv(size)
        if data:
            data = data.rstrip()
            print "Data from client: {0}".format(data)
            if data == "ping":
                client.send("pong\n")
