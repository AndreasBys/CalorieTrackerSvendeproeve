// importing necessary libraries
import express          from 'express';
import bodyParser       from 'body-parser';
import mongoose         from 'mongoose';

// importing route modules
import authRoutes       from './routes/authRoutes.js';
import dishRoutes       from './routes/dishRoutes.js';
import foodInDishRoutes from './routes/foodInDishRoutes.js';
import foodRoutes       from './routes/foodRoutes.js';
import macroGoalRoutes  from './routes/macroGoalRoutes.js';
import macroLogRoutes   from './routes/macroLogRoutes.js';
import userRoutes       from './routes/userRoutes.js';

// importing config
import config           from './config.js';


// creating an instance of the express application
const app = express();

// middleware to parse incoming json requests
app.use(bodyParser.json());

// middleware to parse url-encoded data
app.use(bodyParser.urlencoded({ extended: true }));

// function to connect to mongodb using mongoose
const dbConnect = async () => {
    try {
        await mongoose.connect(config.MONGODB_URL, {
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
app.listen(config.PORT, () => {
    console.log(`listening on port: http://localhost:${config.PORT}`);
});


// define routes
app.use('/api/auth', authRoutes);               // authenticator-related routes
app.use('/api/dish', dishRoutes);               // dish-related routes
app.use('/api/food', foodRoutes);               // food-related routes
app.use('/api/foodInDish', foodInDishRoutes);   // foodInDish-related routes
app.use('/api/macroGoal', macroGoalRoutes);     // macroGoal-related routes
app.use('/api/macroLog', macroLogRoutes);       // macroLog-related routes
app.use('/api/users', userRoutes);              // user-related routes



// defines a route for the root url that sends a welcome message
app.get('/', (req, res) => res.send('hello from homepage'));


