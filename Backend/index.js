var app = require('express')();
var http = require('http').Server(app);
var io = require('socket.io')(http);
var port = process.env.PORT || 3000;
var sql = require('sqlite3').verbose();
var bodyParser = require('body-parser');
var db = new sql.Database('abcd2');

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended:true}));

db.serialize(function(){
    db.run("CREATE TABLE IF NOT EXISTS User (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL , username TEXT , money INT , gems INT  )");
    db.run("CREATE TABLE IF NOT EXISTS Units (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL , user_id INT , UnitName TEXT  , Level INT , FOREIGN KEY(user_id) REFERENCES User(id))");
})


app.get('/', function(req, res){
    res.sendFile(__dirname + '/index.html');
    // res.send("okay");
});

app.get('/test', function(req, res){
    res.send("okay from server");
});

app.get('/load/:id', function(req, res){

    //console.log(req.params.name);
    //var obj = {any:req.params.name};
    //res.json(obj);
    var obj = {
        primary_key :req.params.id,
        name:"weasly chan",
        money:0,
        gems:0,
        status:'404',
        unit_list : []
    };
    // add user
    if(req.params.id == -1){
        console.log("User added");
        obj.status = "User added";
        db.run('INSERT INTO User(username,money,gems) VALUES($username , $money , $gems) ',{$username:obj.name , $money:obj.money , $gems:obj.gems}, function(err , rows){
            console.log(this.lastID);
            if(err){
                return console.log(err.message);
            }
            obj.primary_key = this.lastID;
            res.json(obj);
        })
    }else{
        db.all("SELECT * FROM User WHERE id = $id " , {$id: req.params.id}  , function(err , rows){
            if(err){
                console.log(err);
                throw err;
                res.json(obj);
            }
            if(rows.length > 0 ){
                console.log("User Found");

                var row = rows[0];
                console.log(row.username);
                obj.primary_key = row.id;
                obj.name = row.username;
                obj.money = row.money;
                obj.gems = row.gems;
                obj.status = 'found';

                db.all("SELECT * FROM Units WHERE user_id = $id " , {$id : row.id} , function(err , units){
                    if(err){
                        console.log(err);
                        throw err;
                        res.json(obj);
                    }
                    console.log(units);
                    units.forEach(function (u) {
                        obj.unit_list.push({
                            name:u.UnitName,
                            level:u.Level
                        });
                    })

                    res.json(obj);
                });
            }
            else{
                res.json(obj);
            }
    } )
}
});



app.post('/add', function(req, res){
    var jsonData = req.body.JsonData;
    var obj = JSON.parse(jsonData);
    console.log(obj)
    db.run('INSERT INTO User(username,money,gems) VALUES($username , $money , $gems) ',{$username:obj.name , $money:obj.money , $gems:obj.gems}, function(err){
        if(err){
            res.send("Player already exist");
            return console.log(err.message);
        }
        else{
            res.send("player added");
        }
    })
});

app.post('/updateUser/' , function(req,res){
    var jsonData = req.body.JsonData;
    var obj = JSON.parse(jsonData);
    const sql = 'UPDATE User SET money = ? , gems = ?  WHERE username = ?';
    const parms = [obj.money, obj.gems , obj.name];

    db.run(sql, parms, function(err) {
        if (err) {
            res.json(obj);
            return console.error(err.message);
        }
        //res.json(obj);
    });

    (async () => {
        await updateUser(obj);
    })();

});




//first promise
async function deleteUnits(){
    return new Promise(
        (resolve, reject) => {
            db.run("DELETE FROM Units WHERE user_id = 1 ", { },function(err){
                console.log("first");
                if(err){
                    console.log(err);
                    reject();
                }
                else resolve("unit deleted");
            });
        }
    );
}

// 2nd promise
async function updateunits(obj) {
    return new Promise(
        (resolve, reject) => {

            obj.unit_list.forEach(function(u){
                console.log("second");
                db.run('INSERT INTO Units(user_id,UnitName,Level) VALUES($user_id , $UnitName , $Level) ',{$user_id:obj.primary_key , $UnitName:u.name , $Level:u.level}, function(err , t){
                    console.log("inserted");
                    if(err){
                        console.log(err);
                    }
                })
        
            })
            resolve("DONE");
        }
    );
};

// call our promise
async function updateUser(obj) {
    try {
        let ready = await  deleteUnits(obj);
        let message = await updateunits(obj);
        console.log(message);
    }
    catch (error) {
        console.log(error.message);
    }
}





var roomno = 1;
var playerno = 0;
var roominfo = [];

io.on('connection', function(socket){
    console.log("some one connected");

    socket.on('testSocket', function(msg){
        console.log("some one opened");
        var res = {
            good:5
        };
        io.emit('testSocket', res);
    });
    socket.on('connectToRoom', function(msg){
        console.log("some one connect to room");
        //Increase roomno 2 clients are present in a room.
        if(io.nsps['/'].adapter.rooms["room-"+roomno]){
            if(io.nsps['/'].adapter.rooms["room-"+roomno].length === 1){
                playerno = 1;
            }
            if(io.nsps['/'].adapter.rooms["room-"+roomno].length > 1) {
                playerno = 0;
                roomno++;
            }
        }
        
        console.log("room counter in server " + roomno);
        socket.join("room-"+roomno);

        if(!roominfo[roomno])
            roominfo[roomno] =[];

        for(var i=0; i < msg.selected_monster.length; ++i) {
            roominfo[roomno].push(msg.selected_monster[i])
        }

        console.log(roominfo[roomno]);

        var response = {
            roomid : roomno,
            playerid : playerno,
            message : "you are in room no." + roomno,
            selected_monster : roominfo[roomno]
        };

        io.sockets.in("room-" + roomno).emit('connectToRoom', response);

    });

    socket.on('startMatch' , function(msg){
        var roomno = msg.owner.roomid;
        //socket.join("match-"+roomno);


    });

    socket.on('sendAction', function(msg){
        var roomno = msg.owner.roomid;
        console.log(msg);
        console.log(roomno);

        var response = {
          ok : 1
        };
        io.sockets.in("room-"+roomno).emit('sendAction', msg);

    });
    socket.on('endTurn', function(msg){
        var romid = msg.roomid;
        console.log("turn ended");
        io.sockets.in("room-"+romid).emit('endTurn' , msg);

    });
    
    socket.on('sendMessage', function(msg){
        console.log("some one connect to room");
        var roomid = msg.roomid;
        console.log("roomid is " + roomid);
        var response = {
            message : "message received",
        };

        io.sockets.in("room-"+roomno).emit('connectToRoom', response);

    });


    socket.on('leaveRoom' , function(msg){
        var roomid = msg.roomid;
        console.log("some one disconnected");
        socket.leave("room-"+roomno);
    })


});


http.listen(port, '0.0.0.0',function(){
  console.log('listening on *:' + port);
});
