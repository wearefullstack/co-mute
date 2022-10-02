const express = require('express');
const app = express();
const morgan = require('morgan');
const mongoose = require('mongoose')
const router = require('./routes/routes.js');
const cookieParser = require('cookie-parser');
const bodyParser = require('body-parser');

// register a view engine
app.set('view engine', 'ejs');

// middleware
app.use(express.static('public'))
app.use(express.urlencoded({extended: true}));
app.use(bodyParser.json())
app.use(cookieParser());

// Connect to database
const dbPassword ='2Bwxniyb9J1sitLS'
const databaseURI = `mongodb+srv://Mpumelelo:${dbPassword}@cluster0.nwbkb0p.mongodb.net/coMute?retryWrites=true&w=majority`;

// port number
const PORT = process.env.PORT || 3000;

// connect to the database
mongoose.connect(databaseURI).then(response => {app.listen(PORT);
                                                console.log('connected to database');})
                             .catch(err => console.log(err))

// link to routers (MVC)
app.use(router);