const express = require('express');
const app = express();
const port = 8080;
const user = require('./routes/userRouter');
const babysitters = require('./routes/babySitterRouter');
const manager = require('./routes/managerRouter')
var bodyParser = require('body-parser');//?


var path = require('path-posix')
path.resolve(__dirname, 'foo')



const cors = require('cors');
const corsOptions = {
  origin: '*',
  credentials: true,            //access-control-allow-credentials:true
  optionSuccessStatus: 200
}
app.use(cors(corsOptions));

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

app.use(express.json());
app.use(
  express.urlencoded({
    extended: true,
  })
);

app.use('/api/babySitter', babysitters);
app.use('/api/user', user);
app.use('/api/manager', manager);

// app.use('/api',logIn);

app.listen(port, () => {
  console.log(`app listening at http://localhost:${port}`);
});