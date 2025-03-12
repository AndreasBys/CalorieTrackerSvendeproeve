// importing necessary libraries
import express              from 'express';
import bodyParser           from 'body-parser';
import cron                 from 'node-cron';

// importing route modules
import authRoutes           from './routes/authRoutes.js';
import dishRoutes           from './routes/dishRoutes.js';
import foodRoutes           from './routes/foodRoutes.js';
import macroGoalRoutes      from './routes/macroGoalRoutes.js';
import macroLogRoutes       from './routes/macroLogRoutes.js';
import userRoutes           from './routes/userRoutes.js';

// importing files
import config               from './config.js';
import dbConnect            from './db.js';

// importing saveAllMacroLogs for cron job
import { saveAllMacroLogs } from './controllers/macroLogLegacyController.js';


// creating an instance of the express application
const app = express();

// middleware to parse incoming json requests
app.use(bodyParser.json());

// middleware to parse url-encoded data
app.use(bodyParser.urlencoded({ extended: true }));

// connects to database
dbConnect();


// start the server and listen on the specified port
app.listen(config.PORT, () => {
    console.log(`listening on port: http://localhost:${config.PORT}`);
});

// define routes
app.use('/api/auth', authRoutes);               // authenticator-related routes
app.use('/api/dish', dishRoutes);               // dish-related routes
app.use('/api/food', foodRoutes);               // food-related routes
app.use('/api/macroGoal', macroGoalRoutes);     // macroGoal-related routes
app.use('/api/macroLog', macroLogRoutes);       // macroLog-related routes
app.use('/api/users', userRoutes);              // user-related routes

// Kører hver dag kl. 23:59 UTC+1
cron.schedule("59 23 * * *", async () => {
    console.log("Running daily macro log processing...");
    await saveAllMacroLogs();
});