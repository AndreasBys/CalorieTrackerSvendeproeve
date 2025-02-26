// importing necessary libraries
import express from 'express';
import bodyParser from 'body-parser';
import mongoose from 'mongoose';

// importing route modules
import authRoutes from './routes/authRoutes.js';
import userRoutes from './routes/userRoutes.js';
import foodRoutes from './routes/foodRoutes.js';
import dailyIntakeRoutes from './routes/dailyIntakeRoutes.js';
import goalRoutes from './routes/goalRoutes.js';

// setting environment variables for secret key, mongodb url, and port
process.env.SECRET_KEY = 'test';
process.env.MONGODB_URL = 'mongodb://0.0.0.0:27017/appDB';
process.env.PORT = 5000;

// creating an instance of the express application
const app = express();

// middleware to parse incoming json requests
app.use(bodyParser.json());
// middleware to parse url-encoded data
app.use(bodyParser.urlencoded({ extended: true }));

// function to connect to mongodb using mongoose
const dbConnect = async () => {
    try {
        await mongoose.connect(process.env.MONGODB_URL, {
            autoIndex: true,  // automatically build indexes
        });
        console.log('connected to db');  // log successful connection
    } catch (error) {
        console.log(error);  // log any connection errors
        process.exit(1);  // exit process with failure
    }
};

// establish connection to the database
dbConnect();

// start the server and listen on the specified port
app.listen(process.env.PORT, () => {
    console.log(`listening on port: http://localhost:${process.env.PORT}`);
});

// define routes for different functionalities
app.use('/api/auth', authRoutes);  // authentication routes
app.use('/api/users', userRoutes);  // user-related routes
app.use('/api/foods', foodRoutes);  // food-related routes
app.use('/api/dailyIntakes', dailyIntakeRoutes);  // daily intake routes
app.use('/api/goals', goalRoutes);  // goal-related routes

// define a route for the root url that sends a welcome message
app.get('/', (req, res) => res.send('hello from homepage'));


