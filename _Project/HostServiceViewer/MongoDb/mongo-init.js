
db = db.getSiblingDB('admin');
db.auth('admin', 'Hsv20211229++')


db = db.getSiblingDB('HostServiceViewerDb');

db.createUser({
'user': "admin",
'pwd': "Hsv20211229++",
'roles':[
			{'role': 'dbAdmin','db': 'HostServiceViewerDb'},
			{'role': 'readWrite','db': 'HostServiceViewerDb'}
		]
});

db.createCollection('Test');

db.Test.insertOne({
'Name' : 'Ping',
'Description' : 'Google Ping',
'Type' : 'ping',
'Ip' : '8.8.8.8',
'Timer' : 3,
'Port' : 0,
'PortName' : '',
'Url' : '',
'Active' : true,
'Order' : 0
})

db.Test.insertOne({
'Name' : 'Page',
'Description' : 'Google Page',
'Type' : 'page',
'Ip' : '',
'Timer' : 5,
'Port' : 0,
'PortName' : '',
'Url' : 'http://google.com',
'Active' : true,
'Order' : 0
})



